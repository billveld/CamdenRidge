using CamdenRidge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CamdenRidge.DAL
{
    public class CamdenRidgeContext : DbContext
    {
        public CamdenRidgeContext() : base("CamdenRidgeContext")
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}