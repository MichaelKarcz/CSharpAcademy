using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure;

namespace Phonebook.Database
{
    internal class ContactDbConfiguration : DbConfiguration
    {
        internal ContactDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
        }
    }
}
