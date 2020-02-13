using System;
namespace SmallBank.Model
{
    public class Deposit : Account
    {
        public double CreditLimit { get; set; }
        public Deposit():base()
        {
            CreditLimit = 0;
        }
    }
}
