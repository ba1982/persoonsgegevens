using System;
using Gtk;
using System.IO; //for FileStream, File classes
using System.Linq; //let us try LINQ :-)
using Persoonsgegevens;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{
	TreeModelFilter filter;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		lblSearch.Hide ();
		filterEntry.Hide ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnOpenActionActivated (object sender, EventArgs e)
	{

		//create FileChooserDialog
		FileChooserDialog fcd = new FileChooserDialog (
			"Kies CSV bestand", 
			this, 
			FileChooserAction.Open,
			"Annuleren", ResponseType.Cancel,
			"Open", ResponseType.Accept
		);

		//set filter for CSV extension
		fcd.Filter = new FileFilter ();
		fcd.Filter.AddPattern ("*.csv");

		//run dialog
		ResponseType responseType = (ResponseType)fcd.Run();

		//process response
		if (responseType == ResponseType.Cancel) {

			//close FileChooserDialog
			fcd.Destroy ();
		} else {
			lblSearch.Show ();
			filterEntry.Show ();
			//read CSV-file
			if (responseType == ResponseType.Accept) {				
				string[] allLines = File.ReadAllLines(fcd.Filename);

				//List with Person objects
				List<Person> persons = new List<Person> ();

				//LING to SQL -- read data, skip headers (first line)
				var query = from line in allLines.Skip (1)
				            let data = line.Split (';')
							select new {
								id = data[0],
								firstname = data[1],
								lastname = data[2],
								address = data[3],
								zipcode = data[4],
								city = data[5],
								email = data[6],
								cellphonenumber = data[7]
							};
						
												
				foreach (var p in query) {
					Person person = new Person (
						p.id, 
						p.firstname, 
						p.lastname, 
						p.address, 
						p.zipcode, 
						p.city, 
						p.email, 
						p.cellphonenumber
					);
					persons.Add (person);
				}
				fcd.Hide ();
				TreeView (persons);
			}
		}
	}

	private void TreeView(List<Person> persons)
	{
		filterEntry.Changed += OnFilterEntryChanged;

		//MVC
		//View
		TreeViewColumn idColumn = new TreeViewColumn ();
		TreeViewColumn firstNameColumn = new TreeViewColumn ();
		TreeViewColumn lastNameColumn = new TreeViewColumn ();
		TreeViewColumn addressColumn = new TreeViewColumn ();
		TreeViewColumn zipcodeColumn = new TreeViewColumn ();
		TreeViewColumn cityColumn = new TreeViewColumn();
		TreeViewColumn emailColumn = new TreeViewColumn ();
		TreeViewColumn cellphonenumberColumn = new TreeViewColumn();

		//Set Title attribute
		idColumn.Title = "ID";
		firstNameColumn.Title = "Voornaam";
		lastNameColumn.Title = "Familienaam";
		addressColumn.Title = "Adres";
		zipcodeColumn.Title = "Postcode";
		cityColumn.Title = "Gemeente";
		emailColumn.Title = "E-mail adres";
		cellphonenumberColumn.Title = "GSM-nummer";

		//RendererText
		CellRendererText idCell = new CellRendererText ();
		CellRendererText firstNameCell = new CellRendererText ();
		CellRendererText lastNameCell = new CellRendererText ();
		CellRendererText addressCell = new CellRendererText ();
		CellRendererText zipcodeCell = new CellRendererText ();
		CellRendererText cityCell = new CellRendererText ();
		CellRendererText emailCell = new CellRendererText ();
		CellRendererText cellphonenumberCell = new CellRendererText ();

		idColumn.PackStart(idCell,true);
		firstNameColumn.PackStart (firstNameCell, true);
		lastNameColumn.PackStart (lastNameCell, true);
		addressColumn.PackStart (addressCell, true);
		zipcodeColumn.PackStart(zipcodeCell, true);
		cityColumn.PackStart (cityCell, true);
		emailColumn.PackStart (emailCell, true);
		cellphonenumberColumn.PackStart(cellphonenumberCell,true);

		treeView.AppendColumn (idColumn);	
		treeView.AppendColumn (firstNameColumn);
		treeView.AppendColumn (lastNameColumn);	
		treeView.AppendColumn (addressColumn);		
		treeView.AppendColumn (zipcodeColumn);
		treeView.AppendColumn (cityColumn);	
		treeView.AppendColumn (emailColumn);
		treeView.AppendColumn (cellphonenumberColumn);

		idColumn.AddAttribute(idCell, "text",0);
		firstNameColumn.AddAttribute (firstNameCell, "text", 1);
		lastNameColumn.AddAttribute (lastNameCell, "text", 2);
		addressColumn.AddAttribute (addressCell, "text", 3);
		zipcodeColumn.AddAttribute (zipcodeCell, "text", 4);
		cityColumn.AddAttribute (cityCell, "text", 5);
		emailColumn.AddAttribute (emailCell, "text", 6);
		cellphonenumberColumn.AddAttribute (cellphonenumberCell, "text", 7);

		//Model
		ListStore personsListStore = new ListStore(
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		);

		foreach (Person person in persons) {
			personsListStore.AppendValues (
				person.ID,
				person.FirstName,
				person.LastName,
				person.Address,
				person.Zipcode,
				person.City,
				person.Email,
				person.CellPhoneNumber
			);
		}

		//Controller
		filter = new TreeModelFilter(personsListStore,null);
		filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterTree);
		treeView.Model = filter;

		//show all data
		//treeView.Model = personsListStore;
	}

	private bool FilterTree (TreeModel model, TreeIter iter)
	{
		//GetValue returns the value at column at the path pointed to by iter
		string id = model.GetValue (iter,1).ToString ();

		if (filterEntry.Text == "")
		return true;

		if (id.IndexOf (filterEntry.Text) > -1)
			return true;
		else
			return false;	
	}

	protected void OnFilterEntryChanged (object sender, EventArgs e)
	{
		filter.Refilter();
	}
}
