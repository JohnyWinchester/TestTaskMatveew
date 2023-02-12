using Microsoft.EntityFrameworkCore;
using TestTaskMatveew.Domain;

namespace TestTaskMatveew.DAL.Context
{
    public class TestTaskMatveewDB : DbContext
    {
        public DbSet<Offer> Offers { get; set; }
        public TestTaskMatveewDB(DbContextOptions<TestTaskMatveewDB> options) : base(options)
        {

        }
    }
}
