using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    interface IConnect
    {
        void connectDB();
        void closeDB();
        void SQLquery(string query);
        void SQLToXml(string fileName);
    }
}
