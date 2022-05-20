using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestioneSpese.Core.Entities;

namespace GestioneSpeseEF.RepositoryEF
{
    internal class CategorieConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.NomeCategoria).IsRequired();

            //relazione cat-spese
            builder.HasMany(c => c.Spese).WithOne(s => s.Categoria).HasForeignKey(s => s.CategoriaId);
        }
    }
}