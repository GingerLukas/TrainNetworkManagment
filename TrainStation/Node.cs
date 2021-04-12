using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainStation
{
    class Node : ISelectable
    {
        public const int WIDTH = 16;
        
        public string Name { get; set; }
        public Guid Guid { get; } = Guid.NewGuid();
        public Point Location { get; set; }
        internal List<Rail> rails = new List<Rail>();
        internal List<Node> nodes = new List<Node>();

        public Node(string name, Point location)
        {
            Name = name;
            Location = location;
        }

        public List<Stack<Rail>> Find(Node goal, List<Stack<Rail>> results = null, Stack<Rail> path = null, HashSet<Guid> visited = null)
        {
            //prevent null exception on new path
            path ??= new Stack<Rail>();
            visited ??= new HashSet<Guid>();
            results ??= new List<Stack<Rail>>();

            if (visited.Contains(Guid))
            {
                return results;
            }

            visited.Add(Guid);
            
            //check if goal reached
            if (goal.Guid == Guid)
            {
                //copy current path
                int len = path.Count;
                Rail[] _path = new Rail[len];
                Array.Copy(path.ToArray(), _path, len);
                //save current path as solution
                results.Add(new Stack<Rail>(_path));
            }
            else
            {
                //recursive search on all connected rails
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (visited.Contains(rails[i].Guid)) continue;
                    visited.Add(rails[i].Guid);
                    path.Push(rails[i]);
                    nodes[i].Find(goal, results, path, visited);
                    visited.Remove(rails[i].Guid);
                    path.Pop();
                }
            }

            visited.Remove(Guid);
            return results;
        }

        public Rail Connect(Node other)
        {
            float x = Location.X - other.Location.X;
            x *= x;
            float y = Location.Y - other.Location.Y;
            y *= y;
            Rail rail = new Rail(this, other, (float) Math.Sqrt(x + y));
            rails.Add(rail);
            other.rails.Add(rail);
            nodes.Add(other);
            other.nodes.Add(this);

            return rail;
        }

        private static Brush _normalBrush = Brushes.Red;
        private static Brush _nearBrush = Brushes.Pink;
        private static Brush _selectedBrush = Brushes.Purple;
        
        public void Render(Graphics g)
        {
            g.FillEllipse(_normalBrush, Location.X - WIDTH / 2, Location.Y - WIDTH / 2, WIDTH, WIDTH);
        }

        public void RenderNear(Graphics g)
        {
            int size = WIDTH + 4;
            g.FillEllipse(_nearBrush, Location.X - size / 2, Location.Y - size / 2, size, size);
            Render(g);
        }

        public void RenderSelected(Graphics g)
        {
            int size = WIDTH + 4;
            g.FillEllipse(_selectedBrush, Location.X - size / 2, Location.Y - size / 2, size, size);
            Render(g);
        }
    }
}
