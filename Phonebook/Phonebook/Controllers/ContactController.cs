using Microsoft.Identity.Client;
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

        internal static void ViewContacts()
        {
            List<Contact> contacts = ContactDBHelper.GetAllContacts();

            if (contacts.Count == 0)
            {
                AnsiConsole.WriteLine("\nThere are no contacts.\n");
                return;
            }

            AnsiConsole.WriteLine($"\n~Contacts~");
            Table table = new Table();
            table.AddColumn(new TableColumn("Name").Centered().NoWrap());
            table.AddColumn(new TableColumn("Phone").Centered().NoWrap());
            table.AddColumn(new TableColumn("Email").Centered().NoWrap());
            foreach (Contact contact in contacts)
            {
                table.AddRow(contact.Name, contact.Phone, contact.Email ?? "");
            }
            table.Border(TableBorder.Heavy);
            table.ShowRowSeparators();
            AnsiConsole.Write(table);

            AnsiConsole.WriteLine();
        }

        internal static void EditContact()
        {
            List<Contact> contacts = ContactDBHelper.GetAllContacts();
            Contact contact = SelectAContact(contacts);

            contact.Name = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter the new name: ")
                .DefaultValue(contact.Name));

            contact.Phone = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the new phone number: ")
                .DefaultValue(contact.Phone));
            contact.Phone = ValidationHelper.ValidatePhone(contact.Phone);

            contact.Email = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the new email: ")
                .DefaultValue(contact.Email));
            contact.Email = ValidationHelper.ValidateEmail(contact.Email);

            ContactDBHelper.UpdateContact(contact);

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

        internal static Contact SelectAContact(List<Contact> contacts)
        {
            Contact contact = AnsiConsole.Prompt(
                new SelectionPrompt<Contact>()
                .Title("Select a contact")
                .PageSize(15)
                .MoreChoicesText("[grey]Use the up and down arrow keys to view more contacts[/]")
                .AddChoices(contacts.ToArray()));

            return contact;
        }
    }
}
