using GestioneSpese.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestioneSpeseEF.RepositoryEF
{
    public class Context : DbContext
    {
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Spesa> Spese { get; set; }

        public Context() : base() { }      


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpeseEF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Categoria>(new CategorieConfiguration());
            modelBuilder.ApplyConfiguration<Spesa>(new SpeseConfiguration());

        }
    }
}
