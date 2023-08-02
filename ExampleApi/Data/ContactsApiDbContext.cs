using ExampleApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Data
{
    public class ContactsApiDbContext : DbContext
    {
        public ContactsApiDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
