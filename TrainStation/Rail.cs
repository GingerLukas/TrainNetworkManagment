using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainStation
{
    class Rail
    {
        public const int WIDTH = 4;
        
        public Guid Guid { get; } = Guid.NewGuid();
        public Node A { get; set; }
        public Node B { get; set; }
        public float Distance { get; set; }
        public Train CurrentTrain { get; private set; } = null;
        private Queue<Train> _trains = new Queue<Train>();

        private object _signalLock = new object();

        public Rail(Node a, Node b, float distance)
        {
            A = a;
            B = b;
            Distance = distance;
        }

        //Must call leave after train exits registered rail
        public void Register(Train train)
        {
            lock (_signalLock)
            {
                if (CurrentTrain == null)
                {
                    CurrentTrain = train;
                    CurrentTrain.Signal(this);
                }
                else
                {
                    _trains.Enqueue(train);
                }
            }
        }

        //Signals that no train is currently on rail
        public void Leave(Train train)
        {
            lock (_signalLock)
            {
                if (CurrentTrain == null || train.Guid != CurrentTrain.Guid)
                {
                    return;
                }

                if (_trains.Count > 0)
                {
                    CurrentTrain = _trains.Dequeue();
                    CurrentTrain.Signal(this);
                }
                else
                {
                    CurrentTrain = null;
                }
            }
        }
        
        public Node GetOther(Node node)
        {
            if (node.Guid == A.Guid)
            {
                return B;
            }

            return A;
        }

        public void Render(Graphics g)
        {
            g.DrawLine(new Pen(CurrentTrain != null ? Color.Green : Color.Black, WIDTH), A.Location, B.Location);
        }

        public override string ToString()
        {
            return $"{A.Name} - {B.Name}";
        }
    }
}
