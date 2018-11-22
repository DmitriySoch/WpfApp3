using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    class Programm
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            var main = new MainWidow_1(0);
            app.Run(main);
        }
    }
}
