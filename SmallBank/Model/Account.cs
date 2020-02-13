using System;
namespace SmallBank.Model
{
    public class Account
    {
        private int accountID;
        private static int accountCount;

        protected double balance;
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
            set
            {
                if (this is Loan)
                {
                    if (value < balance)
                    {
                        throw new Exception("Der kan ikke hæves penge på en udlånskonto.");
                    }
                }
                else if( this is Deposit)
                {
                    if (value < ((Deposit)this).CreditLimit)
                    {
                        throw new Exception("Der kan ikke hæves over kreditgrænsen");
                    }
                }
                balance = value;
                
            }
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
