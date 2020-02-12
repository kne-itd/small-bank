using System;
namespace SmallBank.Model
{
    public class Account
    {
        private int accountID;
        private static int accountCount;

        private double balance;
        private double interest;
        private string name;
        private enum AccountNames
        {
            løn,
            opsparing,
            budget,
            billån,
            forbrugslån
        }

        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public double Interest
        {
            get
            {
                return interest;
            }
            set
            {
                interest = value;
            }
        }
        public Account()
        {
            accountCount++;
            accountID = accountCount;
            balance = 0;
        }
    }
}
