using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpace
{
    class Transaction
    {
        readonly DateTime date;
        public DateTime Date
        {
            get { return date; }
        }

        readonly uint id;
        public uint Id
        {
            get { return id; }
        }

        readonly uint payment;
        public uint Payment
        {
            get { return payment; }
        }

        public Transaction(uint id, uint payment)
        {
            this.date = DateTime.Now;
            this.id = Id;
            this.payment = Payment;
        }
    }
}