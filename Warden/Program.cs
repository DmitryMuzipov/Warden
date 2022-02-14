using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectCheck connect = new ConnectCheck();

            connect.ConnectViev("91.109.200.200");
        }
    }
}
