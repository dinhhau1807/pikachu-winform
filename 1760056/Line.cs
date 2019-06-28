using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760056
{
    public enum Type
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Line
    {
        public List<PointF> Points { get; set; }
        public Type Point1 { get; set; }
        public Type Point2 { get; set; }
    }
}
