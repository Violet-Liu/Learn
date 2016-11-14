using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility.Dyn
{
    public class Person
    {
        private int _id = 1;
        private string _name = "jj";
        private string _firstName = "s";
        private string _field1, _field2, _field3;

        public Person()
        {
            //this.Addresses = new List<Address>();
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private Address _address = new Address();

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private List<Address> _addresses = new List<Address>() { new Address() };

        public List<Address> Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }

    }

    public class Address
    {
        public Address()
        {
            this.AddressID = -1;
        }

        public Address(int aid)
        {
            this.AddressID = aid;
        }

        private int _addressID;

        public int AddressID
        {
            get { return _addressID; }
            set { _addressID = value; }
        }

        private string _street = "houhai";

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _city = "shenzhen";

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

    }
}
