using System;
using System.Collections.Generic;

namespace SmallBank.Model
{
    public class Customer
    {
        private int customerID;
        private string name;
        private string email;
        private string telephone;
        private Address address;

        public int CustomerID { get => customerID; set => customerID = value; }
        public string Name { get => name; set => name = value; }
        public Address Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public List<Account> Accounts = new List<Account>();

        public Customer()
        {
        }
    }
}
