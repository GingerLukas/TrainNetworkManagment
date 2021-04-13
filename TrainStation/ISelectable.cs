using System;
using System.Drawing;

namespace TrainStation
{
    public interface ISelectable
    {
        public string Name { get; set; }
        public Guid Guid { get; }
        public void Render(Graphics g);
        public void RenderNear(Graphics g);
        public void RenderSelected(Graphics g);
    }
}