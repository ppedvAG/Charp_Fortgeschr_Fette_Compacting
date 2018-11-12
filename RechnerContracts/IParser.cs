using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerContracts
{
    public interface IParser
    {
       IFormel  Parse(string eingabe);
    }
}
