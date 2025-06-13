using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosikoRefApp.CORE
{
    public static class CD
    {
       public static int CoD(decimal tp)
        {
            return tp switch
            {
                < 1000 => 0,
                >= 1000 and < 5000 => 5,
                >= 5000 => 10
            };
        }
    }
}
