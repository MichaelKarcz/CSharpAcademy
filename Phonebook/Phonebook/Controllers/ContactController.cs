using Phonebook.Database;
using Phonebook.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Controllers
{
    internal static class ContactController
    {
        internal static void AddContact()
        {
            AnsiConsole.Clear();
            string name = AnsiConsole.Prompt(new TextPrompt<string>("Name: "));
            string phone = AnsiConsole.Prompt(new TextPrompt<string>("Phone (xxx-xxx-xxxx): "));
            phone = ValidationHelper.ValidatePhone(phone);
            string email = AnsiConsole.Prompt(new TextPrompt<string>("Email (a@b.cc): "));
            email = ValidationHelper.ValidateEmail(email);

            Contact contact = new Contact
            {
                Name = name,
                Phone = phone,
                Email = email
            };

            ContactDBHelper.AddContact(contact);
        }

        internal static void DeleteContact()
        {
            List<Contact> contacts = ContactDBHelper.GetAllContacts();

            Contact contactToDelete = AnsiConsole.Prompt(
                new SelectionPrompt<Contact>()
                .Title("Select a contact to delete")
                .PageSize(15)
                .MoreChoicesText("[grey]Use the up and down arrow keys to view more contacts[/]")
                .AddChoices(contacts.ToArray()));

            AnsiConsole.WriteLine();

            bool confirmDelete = AnsiConsole.Prompt(
                new TextPrompt<bool>("Are you sure you'd like to delete this contact?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(false)
                .WithConverter(choice => choice ? "yes" : "no"));

            if (!confirmDelete) return;
            else ContactDBHelper.DeleteContact(contactToDelete);

            AnsiConsole.WriteLine("\nThe contact has been deleted.\n");
        }

        internal static void EditContact()
        {
            
        }

        internal static void ViewContacts()
        {
            
        }
    }
}
