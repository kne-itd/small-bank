using System;
namespace SmallBank.Model
{
    public class Address
    {
        private string streetAddress;
        private string houseNumber;
        private string zip;
        private string city;

        public string StreetAddress { get => streetAddress; set => streetAddress = value; }
        public string HouseNumber { get => houseNumber; set => houseNumber = value; }
        public string Zip { get => zip; set => zip = value; }
        public string City { get => city; set => city = value; }
    }
}
