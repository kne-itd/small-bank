using System;
using System.Collections.Generic;
using SmallBank.Model;

namespace SmallBank
{
    public static class Test
    {

        public static List<Customer> Init()
        {
            List<Customer> customers = new List<Customer>();
            string[] names =
            {
                "Anders Andersen", "Bob Bobson", "Chris Christofferson", "Dick Dickson", "Erik Eriksen"
            };
            foreach (string n in names)
            {
                customers.Add(CreateCustomers(n));
            }
            return customers;
        }


        static Customer CreateCustomers(string name)
        {
            Customer a = new Customer();
            a.Name = name;
            a.Email = CreatePhoneyEmail(a.Name);
            a.Telephone = CreatePhoneyPhoneNumber();
            a.Address = CreatePhoneyAddress(a.Name);
            Loan l = (Loan)CreateAccount("loan", 5, "billån");
            a.Accounts.Add(l);
            Deposit d = (Deposit)CreateAccount("deposit", 2, "lønkonto");
            a.Accounts.Add(d);
            return a;
        }

        static Account CreateAccount(string type, double interest, string name)
        {
            switch (type)
            {
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
            for (int i = 0; i < 8; i++)
            {
                output += rnd.Next(0, 10);
            }
            return output;
        }

    }
}
