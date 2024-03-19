using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public abstract class Player
    {
        public bool IsWhite { get; }
        public string Name { get; }
        
        public Color Color { get; }

        protected Player(bool isWhite, string name)
        {
            this.IsWhite = isWhite;
            this.Name = name;
            this.Color = isWhite ? Colors.White : Colors.Black;
        }
    }
}
