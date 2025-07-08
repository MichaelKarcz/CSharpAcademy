using Phonebook.Controllers;
using Phonebook.Models;

namespace Phonebook;

public class Program()
{
    public static void Main(string[] args)
    {
        using (ContactContext db = new ContactContext())
        {

            MenuController.RunMenuLoop();

            /*
            db.Contacts.Add(new Contact
            {
                Name = "Michael Karcz",
                Email = "MichaelAKarcz@gmail.com",
                Phone = "585-590-9534"
            });
            
            Console.WriteLine("Calling save changes...");
            db.SaveChanges();
            Console.WriteLine("Save changes complete");
            

            Contact myContact = db.Contacts.First();

            Console.WriteLine(myContact.Name);

            
            db.Remove(myContact);
            Console.WriteLine("Calling save changes...");
            db.SaveChanges();
            Console.WriteLine("Save changes complete");
            */
        }
    }
}