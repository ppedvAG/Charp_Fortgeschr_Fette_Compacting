using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerContracts
{
    public interface IRechner
    {
        double Rechne(IFormel formel); 
    }
}
