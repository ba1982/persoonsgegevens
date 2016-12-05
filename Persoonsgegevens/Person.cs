using System;

namespace Persoonsgegevens
{
	public class Person
	{
		//constructor
		public Person(string id, string firstName, string lastName, string address, string zipCode, string city, string email, string cellPhoneNumber)
		{			
			this.id = id;
			this.firstname = firstName;
			this.lastname = lastName;
			this.address = address;
			this.zipcode = zipCode;
			this.city = city;
			this.email = email;
			this.cellphonenumber = cellPhoneNumber;
		}

		//properties
		public string ID {
			get { return id; }
			set { this.id = value; }
		}

		public string FirstName {
			get { return firstname; }
			set { this.firstname = value; }
		}

		public string LastName {
			get { return lastname; }
			set { this.lastname = value; }
		}

		public string Address {
			get { return address; }
			set { this.address = value; }
		}

		public string Zipcode {
			get { return zipcode; }
			set { this.zipcode = value; }
		}

		public string City {
			get { return city; }
			set { this.city = value; }
		}

		public string Email {
			get { return email; }
			set { this.email = value; }
		}

		public string CellPhoneNumber {
			get { return cellphonenumber; }
			set { this.cellphonenumber = value; }
		}

		private string id;
		private string firstname;
		private string lastname;
		private string address;
		private string zipcode;
		private string city;
		private string email;
		private string cellphonenumber;
	}
}

