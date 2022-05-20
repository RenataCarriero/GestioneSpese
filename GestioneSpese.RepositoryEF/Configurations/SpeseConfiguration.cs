using GestioneSpese.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GestioneSpeseEF.RepositoryEF
{
    internal class SpeseConfiguration : IEntityTypeConfiguration<Spesa>
    {
        public void Configure(EntityTypeBuilder<Spesa> builder)
        {
            builder.ToTable("Spesa");
            builder.HasKey(x => x.Id);
            builder.Property(s=>s.Data).IsRequired();
            builder.Property(s=>s.CategoriaId).IsRequired();
            builder.Property(s=>s.Descrizione).IsRequired();
            builder.Property(s=>s.Utente).IsRequired();
            builder.Property(s=>s.Importo).IsRequired();
            builder.Property(s=>s.Approvato).IsRequired();

            //relazione
            builder.HasOne(s=>s.Categoria).WithMany(c=>c.Spese).HasForeignKey(s=>s.CategoriaId);
        }
    }
}