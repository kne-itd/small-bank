using System;
namespace SmallBank.Model
{
    public class Loan : Account
    {
        public double InitialBalance { get; set; }
        public Loan():base()
        {
            InitialBalance = -1000;
        }
        public Loan(double amount) : base()
        {
            if (amount > 0)
            {
                amount *= -1;
            }
            InitialBalance = amount;
            balance = InitialBalance;
        }
    }
}
