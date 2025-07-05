using Phonebook.Models;

namespace Phonebook;

public class Program()
{
    public static void Main(string[] args)
    {
        using (ContactContext db = new ContactContext())
        {
            db.Contacts.Add(new Contact
            {
                Name = "Michael Karcz",
                Email = "MichaelAKarcz@gmail.com",
                Phone = "585-590-9534"
            });

            Console.WriteLine("Calling save changes...");
            db.SaveChanges();
            Console.WriteLine("Save changes complete");
        }
    }
}