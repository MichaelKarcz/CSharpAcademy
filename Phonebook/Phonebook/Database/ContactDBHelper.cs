using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Database
{
    internal class ContactDBHelper
    {

        internal static List<Contact> GetAllContacts()
        {
            using (ContactContext db = new ContactContext())
            {
                List<Contact> allContacts = db.Contacts
                    .OrderBy(c => c.Name)
                    .ToList();

                return allContacts;
            }
        }

        internal static void AddContact(Contact contact)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Add(contact);
                Console.WriteLine("Saving the contact...");
                db.SaveChanges();
                Console.WriteLine("Save changes complete\n");
                
            }
        }

        internal static void UpdateContact(Contact contact)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Add(contact);
                Console.WriteLine("Saving the contact...");
                db.SaveChanges();
                Console.WriteLine("Save changes complete\n");

            }
        }

        internal static void DeleteContact(Contact contactToDelete)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Remove(contactToDelete);
                Console.WriteLine("Deleting the contact...");
                db.SaveChanges();
                Console.WriteLine("Delete complete\n");
            }
        }
        
    }
}
