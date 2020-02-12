using System;
namespace SmallBank.Model
{
    public class Transaction
    {
        private int iD;
        private DateTime date;
        private double amount;

        public int ID { get => iD; set => iD = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Amount { get => amount; set => amount = value; }
        public Transaction()
        {
        }
    }
}
