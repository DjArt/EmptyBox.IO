using EmptyBox.IO.Serializator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;

namespace EmptyBox.IO.Desktop.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            BinarySerializer a = new BinarySerializer(Encoding.UTF32);
            byte[] test = a.Serialize(new TimeSpan(70000));
            ReadKey();
        }
    }
}
