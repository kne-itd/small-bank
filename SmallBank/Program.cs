using System;
using System.Collections.Generic;
using SmallBank.Model;

namespace SmallBank
{
    class Program
    {
        private static Customer currentCustomer;
        private static Account currentAccount;
        static List<Customer> customers = new List<Customer>();
        static void Main(string[] args)
        {
            customers = Test.Init();
            SeeCustomers();

            string cMenu = string.Empty;
            foreach( Customer c in customers)
            {
                cMenu += "(" + c.CustomerID + ") ";
            }
            do
            {
                Console.WriteLine("Ønsker du at se bankens samlede kapital? ja(y) - nej(n)");
                if (Console.ReadLine() == "y")
                {
                    double cap = CalculateCapital();
                    Console.WriteLine($"Bankens samlede kapital er: DKK {cap}");
                }
                else
                {
                    Console.WriteLine($"Hvilken kunde? {cMenu}");
                    int currentCustomerID = 0;
                    int.TryParse(Console.ReadLine(), out currentCustomerID);
                    currentCustomer = FindCustomerById(currentCustomerID);
                    SeeCustomer(currentCustomer);
                    Console.WriteLine($"Ønsker du at: ");
                    Console.WriteLine($"Oprette konto (1) ");
                    Console.WriteLine($"Indsætte beløb (2)");
                    Console.WriteLine($"Hæve beløb (3)");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Account a = NewAccountDialog();
                            currentCustomer.Accounts.Add(a);
                            SeeCustomer(currentCustomer);
                            break;
                        case "2":
                            TransactionDialog("indsætte");
                            SeeCustomer(currentCustomer);
                            break;
                        case "3":
                            TransactionDialog("hæve");
                            SeeCustomer(currentCustomer);
                            break;
                    }
                }
            } while (!string.IsNullOrEmpty(Console.ReadLine()));

        }

        private static void TransactionDialog(string action)
        {
            string aMenu = string.Empty;
            foreach (Account acc in currentCustomer.Accounts)
            {
                aMenu += $"{acc.Name} ({acc.AccountID}) ";
            }
            Console.WriteLine("På hvilken konto? (angiv konti-id");
            Console.WriteLine(aMenu);
            int.TryParse(Console.ReadLine(), out int currentAccountID);
            currentAccount = FindAccountById(currentAccountID);
            Console.WriteLine($"Hvilket beløb ønsker du at {action}? ");
            double.TryParse(Console.ReadLine(), out double amount);
            if (action == "hæve")
            {
                try
                {
                    currentAccount.Balance -= amount;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    currentAccount.Balance += amount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        private static Account NewAccountDialog()
        {
            string name = string.Empty;
            string type = string.Empty;

            double interest = 0;
            Console.WriteLine("Udlån(1) eller indlån(2)");
            int.TryParse(Console.ReadLine(), out int typeID);
            Console.WriteLine("Kontoens navn:");
            name = Console.ReadLine();
            Console.WriteLine("Kontoens rentefod:");
            double.TryParse(Console.ReadLine(), out interest);

            switch (typeID)
            {
                case 1:
                    return new Loan
                    {
                        Name = name,
                        Interest = interest
                    };
                case 2:
                default:
                    return new Deposit
                    {
                        Name = name,
                        Interest = interest
                    };

            }
        }

        private static double CalculateCapital()
        {
            double output = 0;
            foreach (Customer c in customers)
            {
                foreach (Account a in c.Accounts)
                {
                    output += a.Balance;
                }
            }
            return output;
        }

        private static Customer FindCustomerById(int id)
        {
            foreach(Customer c in customers)
            {
                if ( c.CustomerID == id)
                {
                    return c;
                }
            }
            return null;
        }

        private static Account FindAccountById(int currentAccountID)
        {
            foreach (Account a in currentCustomer.Accounts)
            {
                if (a.AccountID == currentAccountID)
                {
                    return a;
                }
            }
            return null;
        }

        static void SeeCustomers()
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"{c.CustomerID}, {c.Name}, {c.Email}, {c.Telephone}");
                Console.WriteLine($"{c.Address.StreetAddress} {c.Address.HouseNumber}, {c.Address.Zip} {c.Address.City}");
                foreach (Account account in c.Accounts)
                {
                    Console.WriteLine($"   {account.AccountID}: {account.Name} interest: {account.Interest} {account.Balance}");
                    if (account is Loan)
                    {
                        Console.WriteLine($"   Startbeløb: {((Loan)account).InitialBalance}");
                    }
                    else if ( account is Deposit )
                    {
                        Console.WriteLine($"   Limit: {((Deposit)account).CreditLimit}");
                    }
                }
            }
        }

        static void SeeCustomer(Customer c)
        {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"{c.CustomerID}, {c.Name}, {c.Email}, {c.Telephone}");
                Console.WriteLine($"{c.Address.StreetAddress} {c.Address.HouseNumber}, {c.Address.Zip} {c.Address.City}");
                foreach (Account account in c.Accounts)
                {
                    Console.WriteLine($"   {account.AccountID}: {account.Name} interest: {account.Interest}" +
                        $" {account.Balance}");
                }
        }
    }
}
