using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainStation
{
    class Train : ISelectable
    {
        public const int WIDTH = 12;
        public const float MOVE_STEP = 1f;

        public delegate void StateChangedHandler(EState state);

        public event StateChangedHandler StateChanged;

        private EState _state;
        public EState State
        {
            get {return _state; }
            set
            {
                _state = value;
                StateChanged?.Invoke(value);
            }
        }

        public Guid Guid { get; } = Guid.NewGuid();
        public Point Location { get; private set; }
        
        private Node _goal;
        public Node Goal
        {
            get => _goal;
            set
            {
                if(_started) return;
                _goal = value;
            }
        }

        public float Speed { get; set; }
        public string Name { get; set; }


        private Rail _currentRail = null;
        private Node _lastNode = null;

        private bool _started = false;

        private float _railDistance = 0;
        private EventWaitHandle _signalHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private Stack<Rail> _path;


        public Train(string name,Node node, float speed)
        {
            Name = name;
            _lastNode = node;
            Speed = speed;
            Location = _lastNode.Location;
        }
        

        //returns false if already in goal destination or true if start was successful
        public bool Start(CancellationToken token)
        {
            if (_started)
            {
                throw new Exception("Train is already on it's way");
            }

            if (Goal == null)
            {
                throw new Exception("Goal was not set");
            }
            if (_lastNode.Guid == Goal.Guid)
            {
                return false;
            }

            List<Stack<Rail>> options = _lastNode.Find(Goal);
            
            _path = options.OrderBy(x => x.Sum(rail => rail.Distance)).FirstOrDefault();
            if (_path == null)
            {
                throw new Exception("Path to goal could not be found");
            }

            
            ThreadPool.QueueUserWorkItem(Move, token);
            
            return true;
        }

        public void Signal(Rail rail)
        {
            if (rail.Guid == _currentRail.Guid)
            {
                _signalHandle.Set();
            }
        }
        
        public void Move(object obj)
        {
            if (_path == null || obj is not CancellationToken) return;
            CancellationToken token = (CancellationToken) obj;
            _started = true;
            
            State = EState.Waiting;
            _currentRail = _path.Pop(); 
            _currentRail.Register(this);
            _signalHandle.WaitOne();
            State = EState.Running;
            
            while (!token.IsCancellationRequested)
            {
                Node a = _lastNode;
                Node b = _currentRail.GetOther(a);
                
                int x = a.Location.X;
                int y = a.Location.Y;
                
                float xDiff = a.Location.X - b.Location.X;
                float yDiff = a.Location.Y - b.Location.Y;
                float xSlope = xDiff / _currentRail.Distance;
                float ySlope = yDiff / _currentRail.Distance;
                
                //check for end of rail
                while (!token.IsCancellationRequested && _railDistance < _currentRail.Distance)
                {
                    _railDistance = Math.Min(_currentRail.Distance, _railDistance + MOVE_STEP);
                    Thread.Sleep((int) (500f/Speed));
                    Location = new Point(x - (int) (xSlope * _railDistance), y - (int) (ySlope * _railDistance));
                }
                _currentRail.Leave(this);
                _lastNode = b;
                _railDistance = 0;
                if (_path.Count == 0 || token.IsCancellationRequested)
                {
                    break;
                }

                State = EState.Waiting;
                _currentRail = _path.Pop();
                _currentRail.Register(this);
                _signalHandle.WaitOne();
                State = EState.Running;
            }

            State = EState.Idle;
            _started = false;
            _path = null;
            Goal = null;
        }

        private static Brush _normalBrush = Brushes.Blue;
        private static Brush _nearBrush = Brushes.Orange;
        private static Brush _selectedBrush = Brushes.Yellow;

        public void Render(Graphics g)
        {
            g.FillEllipse(_normalBrush, Location.X - WIDTH / 2, Location.Y - WIDTH / 2, WIDTH, WIDTH);
        }

        public void RenderNear(Graphics g)
        {
            int size = WIDTH + 2;
            g.FillEllipse(_nearBrush, Location.X - size / 2, Location.Y - size / 2, size, size);
            Render(g);
        }

        public void RenderSelected(Graphics g)
        {
            int size = WIDTH + 2;
            g.FillEllipse(_selectedBrush, Location.X - size / 2, Location.Y - size / 2, size, size);
            Render(g);
        }
        public enum EState
        {
            Idle,
            Running,
            Waiting,
        }
    }
}
