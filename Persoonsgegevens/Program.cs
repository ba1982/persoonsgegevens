using System;
using Gtk;

namespace Persoonsgegevens
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}

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
		}

		public string FirstName {
			get { return firstname; }
		}

		public string LastName {
			get { return lastname; }
		}

		public string Address {
			get { return address; }
		}

		public string Zipcode {
			get { return zipcode; }
		}

		public string City {
			get { return city; }
		}

		public string Email {
			get { return email; }
		}

		public string CellPhoneNumber {
			get { return cellphonenumber; }
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
