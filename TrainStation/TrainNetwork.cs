using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainStation
{
    class TrainNetwork
    {
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Dictionary<Guid, Node> Nodes = new Dictionary<Guid, Node>();
        public Dictionary<Guid, Rail> Rails = new Dictionary<Guid, Rail>();
        public Dictionary<Guid, Train> Trains = new Dictionary<Guid, Train>();
        public Dictionary<Guid, ISelectable> Selectables = new Dictionary<Guid, ISelectable>();

        public Node AddNode(string name, Point location)
        {
            Node node = new Node(name, location);
            Nodes.Add(node.Guid, node);
            Selectables.Add(node.Guid, node);
            return node;
        }

        public Train AddTrain(string name, Node start, float speed)
        {
            Train train = new Train(name, start, speed);
            Trains.Add(train.Guid, train);
            Selectables.Add(train.Guid, train);
            return train;
        }

        public void Connect(Node a, Node b)
        {
            Rail rail = a.Connect(b);
            Rails.Add(rail.Guid, rail);
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
            _tokenSource = new CancellationTokenSource();
            Trains.Clear();
        }

        public void Render(Graphics g)
        {
            foreach (Rail rail in Rails.Values)
            {
                rail.Render(g);
            }
            foreach (Node node in Nodes.Values)
            {
                node.Render(g);
            }
            foreach (Train train in Trains.Values)
            {
                train.Render(g);
            }
        }

        public void Start(Train train)
        {
            train.Start(_tokenSource.Token);
        }

        public ISelectable GetItemNear(Point location, out EClickableItem type, float allowance = 5)
        {

            float maxDist = allowance + Train.WIDTH;
            Train t = Trains.Values.OrderBy(x => x.Location.Distance(location)).FirstOrDefault();
            if (t != null && t.Location.Distance(location) <= maxDist)
            {
                type = EClickableItem.Train;
                return t;
            }

            maxDist = (allowance + Node.WIDTH) * 3;
            Node n = Nodes.Values.OrderBy(x => x.Location.Distance(location)).FirstOrDefault();
            if (n != null && n.Location.Distance(location) <= maxDist)
            {
                type = EClickableItem.Node;
                return n;
            }


            type = EClickableItem.None;
            return null;
        }

        public void Clear()
        {
            Cancel();
            Trains.Clear();
            Nodes.Clear();
            Rails.Clear();
        }
    }


    enum EClickableItem
    {
        None,
        Node,
        Train
    }
}
