using System;
using System.Collections.Generic;
using System.Text;

namespace Kupid
{
    public interface IRecenzija
    {
        string DajUtisak();
    }
    public class Recenzija : IRecenzija
    {
        public string DajUtisak()
        {
            throw new NotImplementedException();
        }
    }
}
