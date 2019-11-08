using EmptyBox.IO.Serializator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmptyBox.Collections.Generic;
using static System.Console;

namespace EmptyBox.IO.Desktop.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            BinarySearchTreeNode<int> a = new BinarySearchTreeNode<int>() { 123, 33, 22, 43, 13 };
            ReadKey();
        }
    }
}
