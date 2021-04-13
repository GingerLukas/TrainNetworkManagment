using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainStation
{
    public partial class SelectableDetail : UserControl
    {
        public event EventHandler ActionClicked
        {
            add => _btnAction.Click += value;
            remove => _btnAction.Click -= value;
        }

        private ISelectable _item;

        public ISelectable Item
        {
            get { return _item; }
            set
            {
                if (_item is Train t)
                {
                    t.StateChanged -= TrainOnStateChanged;
                }
                _item = value;
                if (_item is Train tt)
                {
                    tt.StateChanged += TrainOnStateChanged;
                }
                UpdateUi();
            }
        }

        public SelectableDetail()
        {
            InitializeComponent();

            DoubleBuffered = true;
            _lbMain.SetProperty(nameof(DoubleBuffered), true);
        }

        private void TrainOnStateChanged(Train.EState state)
        {
            UpdateTrain();
        }

        private void UpdateTrain()
        {
            if (Item is Train train)
            {
                _lblState.Invoke(() => _lblState.Text = train.State.ToString());
                _lbMain.Invoke(() =>
                {
                    _lbMain.Items.Clear();
                    if (train.Path != null)
                    {
                        if (train.CurrentRail != null)
                        {
                            _lbMain.Items.Add(train.CurrentRail);
                            _lbMain.SetSelected(0, true);
                        }
                        foreach (Rail rail in train.Path)
                        {
                            _lbMain.Items.Add(rail);
                        }
                    }
                });
                _btnAction.Invoke(() => _btnAction.Enabled = train.State == Train.EState.Idle);
            }
        }

        private void UpdateNode()
        {
            if (Item is Node node)
            {
                _lbMain.Invoke(() =>
                {
                    _lbMain.Items.Clear();
                    foreach (Node child in node.nodes)
                    {
                        _lbMain.Items.Add(child);
                    }
                });
                _btnAction.Invoke(() => _btnAction.Enabled = false);
            }
            
        }

        private void UpdateUi()
        {
            Enabled = _item != null;
            switch (_item)
            {
                case Node node:
                    _txtName.Text = node.Name;
                    _lblType.Text = "Node";
                    UpdateNode();
                    break;
                case Train train:
                    _txtName.Text = train.Name;
                    _lblType.Text = "Train";
                    UpdateTrain();
                    break;
            }
        }

        private void _txtName_TextChanged(object sender, EventArgs e)
        {
            Item.Name = _txtName.Text;
        }
    }
}
