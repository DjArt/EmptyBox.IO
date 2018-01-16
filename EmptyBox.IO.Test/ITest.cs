using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Test
{
    public interface ITest
    {
        string Description { get; }
        Task<string> Run();
    }
}
