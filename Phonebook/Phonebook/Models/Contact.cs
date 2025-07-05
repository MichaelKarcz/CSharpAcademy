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
    internal class Contact
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
    }

    internal class ContactContext : DbContext
    {
        internal DbSet<Contact> Contacts { get; set; }
        internal string DbPath { get; }

        internal ContactContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "phonebook.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConfigurationManager.AppSettings.Get("phonebookConnectionString"));
    }
}
