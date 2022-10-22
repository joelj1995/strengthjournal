using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Contexts
{
    public class StrengthJournalContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=StrengthJournal1;Trusted_Connection=True");
        }
    }
}
