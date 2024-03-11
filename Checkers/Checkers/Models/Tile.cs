using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Tile : ObservableObject
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsSelected { get; set; }

        [ObservableProperty] 
        public Color _color;

        public Tile()
        {
            this.IsSelected = false;
        }
    }
}
