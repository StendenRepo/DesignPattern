using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class Tile
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsWhite { get; set; }
        public bool IsSelected { get; set; }
    }
}
