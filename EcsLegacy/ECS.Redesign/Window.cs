using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Redesign
{
    public class Window : IWindow
    {
        public void Open()
        {
            Console.WriteLine("Window is open");
        }

        public void Close()
        {
            Console.WriteLine("Window is closed");
        }
    }
}
