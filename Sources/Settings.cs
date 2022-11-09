using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Snake
{
    public enum Direction
    {
        NoDirection,
        Up,
        Down,
        Left,
        Right,
    };

    public class Settings
    {
        public static Direction direction { get; set; }
    }
}
