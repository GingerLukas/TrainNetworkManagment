using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainStation
{
    public partial class Form1 : Form
    {
        private Random _random = new Random();
        private TrainNetwork _network = new TrainNetwork();
        
        private Point _mouse;
        private ISelectable _nearObj = null;
        private EClickableItem _nearType = EClickableItem.None;

        public ISelectable Selected
        {
            get => _selected;
            private set
            {
                _selected = value;
                _seletedDetail.Item = value;
            }
        }
        
        private EControlMode __controlMode = EControlMode.Select;
        private ISelectable _selected = null;

        private EControlMode _controlMode
        {
            get { return __controlMode;}
            set
            {
                __controlMode = value;
                UpdateStatus();
            }
        }
        
        public Form1()
        {
            InitializeComponent();
            
            DoubleBuffered = true;
            
            Init();
        }

        private void Init()
        {
            _miNodesBuild.Click += MiNodesBuildOnClick;
            _miNodesConnect.Click += MiNodesConnectOnClick;
            _miTrainsNew.Click += MiTrainsNewOnClick;
            _miTrainsRemoveAll.Click += MiTrainsRemoveAllOnClick;
            _miSimulation.Click += MiSimulationOnClick;
            _miDemo.Click += MiDemoOnClick;
            
            _seletedDetail.ActionClicked += SeletedDetailOnActionClicked;
            
            UpdateStatus();
            
            _renderTimer.Start();
        }

        private void MiDemoOnClick(object? sender, EventArgs e)
        {
            GenerateDemo();
        }

        private void SeletedDetailOnActionClicked(object? sender, EventArgs e)
        {
            switch (Selected)
            {
                case Node node:
                    
                    break;
                case Train train:
                    _controlMode = EControlMode.TrainGoalSelect;
                    break;
            }
        }

        private void MiSimulationOnClick(object? sender, EventArgs e)
        {
            _controlMode = EControlMode.Select;
        }

        private void MiNodesConnectOnClick(object? sender, EventArgs e)
        {
            _controlMode = EControlMode.Rail;
        }

        private void MiTrainsRemoveAllOnClick(object? sender, EventArgs e)
        {
            Selected = null;
            _network.Cancel();
            Invalidate();
        }

        private void MiTrainsNewOnClick(object? sender, EventArgs e)
        {
            _controlMode = EControlMode.Train;
            
            Invalidate();
        }

        private void MiNodesBuildOnClick(object? sender, EventArgs e)
        {
            _controlMode = EControlMode.Node;
        }

        private void GenerateDemo()
        {
            _network.Clear();
            Selected = null;

            Node a = _network.AddNode("A", new Point(50, 100));
            Node aConnector = _network.AddNode("A connector", new Point(100, 100));

            Node b = _network.AddNode("B", new Point(50, 150));
            Node bConnector = _network.AddNode("B connector", new Point(100, 150));

            Node c = _network.AddNode("C", new Point(50, 200));
            Node cConnector = _network.AddNode("C connector", new Point(100, 200));

            Node abcJunction = _network.AddNode("ABC junction", new Point(150, 150));


            Node mid = _network.AddNode("MID", new Point(150 + (500 - 150)/2, 150));
            
            
            Node defJunction = _network.AddNode("DEF junction", new Point(500, 150));

            Node d = _network.AddNode("D", new Point(600,100));
            Node dConnector = _network.AddNode("D connector", new Point(550,100));
            
            Node e = _network.AddNode("E", new Point(600,150));
            Node eConnector = _network.AddNode("E connector", new Point(550, 150));
            
            Node f = _network.AddNode("F", new Point(600,200));
            Node fConnector = _network.AddNode("F connector", new Point(550, 200));


            _network.Connect(a,aConnector);
            _network.Connect(b,bConnector);
            _network.Connect(c,cConnector);
            
            _network.Connect(aConnector,abcJunction);
            _network.Connect(bConnector,abcJunction);
            _network.Connect(cConnector,abcJunction);
            
            _network.Connect(abcJunction,mid);
            _network.Connect(mid,defJunction);
            
            _network.Connect(defJunction,dConnector);
            _network.Connect(defJunction,eConnector);
            _network.Connect(defJunction,fConnector);
            
            _network.Connect(dConnector,d);
            _network.Connect(eConnector,e);
            _network.Connect(fConnector,f);
            

            Train aTrain = _network.AddTrain("Train A", a, 25);
            Train bTrain =_network.AddTrain("Train B", b, 50);
            Train cTrain = _network.AddTrain("Train C", c, 100);

            Train aaTrain = _network.AddTrain("Train AA", aConnector, 25);
            Train bbTrain = _network.AddTrain("Train BB", bConnector, 50);
            Train ccTrain = _network.AddTrain("Train CC", cConnector, 100);

            aTrain.Goal = f;
            aaTrain.Goal = eConnector;
            bTrain.Goal = d;
            bbTrain.Goal = fConnector;
            cTrain.Goal = e;
            ccTrain.Goal = dConnector;
            
            _network.Start(aTrain);
            _network.Start(aaTrain);
            _network.Start(bTrain);
            _network.Start(bbTrain);
            _network.Start(cTrain);
            _network.Start(ccTrain);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            
            _network.Render(g);
            
            _nearObj?.RenderNear(g);
                
            Selected?.RenderSelected(g);
            

            switch (__controlMode)
            {
                case EControlMode.Select:
                    break;
                case EControlMode.Node:
                    if (_nearType != EClickableItem.Node)
                    {
                        g.FillEllipse(new SolidBrush(Color.FromArgb(128, 27, 27, 27)),
                            _mouse.X - Node.WIDTH / 2, _mouse.Y - Node.WIDTH / 2,
                            Node.WIDTH, Node.WIDTH);
                    }
                    break;
                case EControlMode.Rail:
                    break;
                case EControlMode.Train:
                    break;
            }
        }

        enum EControlMode
        {
            Select,
            Node,
            Rail,
            Train,
            TrainGoalSelect
        }

        private void UpdateStatus()
        {
            _statusLabel.Text = _controlMode.ToString();
        }

        private void _renderTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _network.Cancel();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Point location = ((MouseEventArgs) e).Location;
            EClickableItem type;
            switch (_controlMode)
            {
                case EControlMode.Select:
                    if (Selected != null && _nearObj != null && Selected.Guid == _nearObj.Guid)
                    {
                        Selected = null;
                        break;
                    }
                    Selected = _nearObj;
                    break;
                case EControlMode.Node:
                    if (_network.GetItemNear(location,out type) != null)
                    {
                        break;
                    }
                    _network.AddNode("new node "+_network.Nodes.Count,location);
                    break;
                case EControlMode.Rail:
                    if (_nearType == EClickableItem.Node)
                    {
                        Node node = (Node) _nearObj;
                        if (Selected != null && Selected.Guid == _nearObj.Guid)
                        {
                            Selected = null;
                        }
                        else if (Selected is Node && Selected.Guid != node.Guid)
                        {
                            _network.Connect(node, (Node)Selected);
                            Selected = null;

                        }
                        else
                        {
                            Selected = node;
                        }
                    }
                    break;
                case EControlMode.Train:
                    if (_nearType == EClickableItem.Node)
                    {
                        _network.AddTrain("new train " + _network.Trains.Count, (Node) _nearObj, 50);
                    }

                    break;
                case EControlMode.TrainGoalSelect:
                    if (_nearObj is Node goal && Selected is Train)
                    {
                        Train t = (Train)Selected;
                        t.Goal = goal;
                        _network.Start(t);
                        _controlMode = EControlMode.Select;
                    }
                    break;
            }
            
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
            _nearObj = _network.GetItemNear(e.Location, out _nearType);
        }
    }

}
