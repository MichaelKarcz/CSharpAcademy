using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Phonebook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }

        public Contact()
        {
            Name = "";
            Email = "";
            Phone = "";
        }

        public Contact(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        

        public ContactContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConfigurationManager.AppSettings.Get("phonebookConnectionString"));
    }
}
