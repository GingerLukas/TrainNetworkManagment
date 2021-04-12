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
        
        private EControlMode __controlMode = EControlMode.Simulation;
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
            
            _seletedDetail.ActionClicked += SeletedDetailOnActionClicked;
            
            UpdateStatus();
            
            _renderTimer.Start();
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
            _controlMode = EControlMode.Simulation;
        }

        private void MiNodesConnectOnClick(object? sender, EventArgs e)
        {
            _controlMode = EControlMode.Rail;
        }

        private void MiTrainsRemoveAllOnClick(object? sender, EventArgs e)
        {
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
                case EControlMode.Simulation:
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
            Simulation,
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
                case EControlMode.Simulation:
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
                        _controlMode = EControlMode.Simulation;
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

    enum EClickableItem
    {
        None,
        Node,
        Train
    }
}
