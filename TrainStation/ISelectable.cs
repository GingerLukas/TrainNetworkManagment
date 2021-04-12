using System;
using System.Drawing;

namespace TrainStation
{
    public interface ISelectable
    {
        public Guid Guid { get; }
        public void Render(Graphics g);
        public void RenderNear(Graphics g);
        public void RenderSelected(Graphics g);
    }
}