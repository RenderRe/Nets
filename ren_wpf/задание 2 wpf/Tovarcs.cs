using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace задание_2_wpf
{
        public partial class Tovar
    {
        public override string ToString()
        {
            //return base.ToString();
            return nazvanie;
        }
    }
    public partial class VidTovara
    {
        public override string ToString()
        {
            //return base.ToString();
            return vid;
        }
    }
}
