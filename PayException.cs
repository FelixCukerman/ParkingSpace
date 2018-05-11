using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpace
{
    class PayException : Exception
    {
        readonly private string message = "Оплатите штраф!";
        public override string Message
        {
            get { return message; }
        }
    }
}
