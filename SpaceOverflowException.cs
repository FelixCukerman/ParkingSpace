using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpace
{
    class SpaceOverflowException : Exception
    {
        readonly private string message = "Недостаточно места на парковке!";
        public override string Message
        {
            get { return message; }
        }
    }
}
