using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinFormsApp
{    
    public class BorderIventArg
    {
        public BorderSide Side;

        public BorderIventArg(BorderSide side)
        {
            this.Side = side;   
        }
    }
}
