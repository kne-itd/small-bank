using System;
namespace SmallBank.Model
{
    public class Account
    {
        private int accountID;
        private int balance;
        private decimal interest;
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

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public decimal Interest
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
            balance = 0;
        }
    }
}
