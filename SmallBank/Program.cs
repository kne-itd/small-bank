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
            Init();
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
            Console.WriteLine($"Hvilket beløb øsker du at {action}? ");
            double.TryParse(Console.ReadLine(), out double amount);
            if (action == "hæve")
            {
                currentAccount.Balance -= amount;
            }
            else
            {
                currentAccount.Balance += amount;
            }
            
        }

        private static Account FindAccountById(int currentAccountID)
        {
            foreach( Account a in currentCustomer.Accounts)
            {
                if (a.AccountID == currentAccountID)
                {
                    return a;
                }
            }
            return null;
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

            return CreateAccount(type, interest, name);
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

        private static void Init()
        {
            string[] names =
            {
                "Anders Andersen", "Bob Bobson", "Chris Christofferson", "Dick Dickson", "Erik Eriksen"
            };
            foreach (string n in names)
            {
                customers.Add(CreateCustomers(n));
            }
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

        static void SeeCustomers()
        {
            foreach (Customer c in customers)
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

        static Customer CreateCustomers(string name)
        {
            Customer a = new Customer();
            a.Name = name;
            a.Email = CreatePhoneyEmail(a.Name);
            a.Telephone = CreatePhoneyPhoneNumber();
            a.Address = CreatePhoneyAddress(a.Name);
            Loan l = (Loan) CreateAccount("loan", 5, "billån");
            a.Accounts.Add(l);
            Deposit d = (Deposit)CreateAccount("deposit", 2, "lønkonto");
            a.Accounts.Add(d);
            return a;
        }

        static Account CreateAccount( string type, double interest, string name)
        {
            switch (type) {
                case "loan":
                    Loan a = new Loan
                    {
                        Interest = interest,
                        Name = name
                    };
                    return a;
                case "deposit":
                default:
                    Deposit b = new Deposit
                    {
                        Interest = interest,
                        Name = name
                    };
                    return b;
            }
        }

        private static string CreatePhoneyEmail(string name)
        {
            string[] a = name.Split(" ");

            return a[0].ToLower() + "@" + a[1].ToLower() + ".dk";
        }
        private static Address CreatePhoneyAddress(string name)
        {
            string[] a = name.Split(" ");
            Random rnd = new Random();
            Address b = new Address
            {
                StreetAddress = a[1] + "svej",
                HouseNumber = rnd.Next(1, 100).ToString(),
                Zip = "5000",
                City = "Odense"
            };
            return b;
        }

        static string CreatePhoneyPhoneNumber()
        {
            Random rnd = new Random();
            string output = string.Empty;
            for (int i= 0; i <8; i++)
            {
                output += rnd.Next(0, 10);
            }
            return output;
        }
    }
}
