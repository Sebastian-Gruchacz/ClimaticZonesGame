using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var layout = new Layout(new LayoutParameters());
            layout.Calculations();
        }
    }
}
