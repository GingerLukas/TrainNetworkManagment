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

        private void TrainOnStateChanged(Train.EState state)
        {
            _lblState.Invoke(() => _lblState.Text = state.ToString());
        }

        public SelectableDetail()
        {
            InitializeComponent();
        }

        private void UpdateUi()
        {
            switch (_item)
            {
                case Node node:
                    _lblName.Text = node.Name;
                    _lblType.Text = "Node";
                    break;
                case Train train:
                    _lblName.Text = train.Name;
                    _lblType.Text = "Train";
                    break;
            }
        }
    }
}
