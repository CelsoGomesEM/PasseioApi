using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Passeio.Negocio.Models;

namespace Passeio.Data.Mappings
{
    public class LugarMapping : IEntityTypeConfiguration<Lugar>
    {
        public void Configure(EntityTypeBuilder<Lugar> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(l => l.Localizacao)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(l => l.UrlFoto)
                .HasColumnType("varchar(250)");

            builder.Property(l => l.Avaliacao)
                .IsRequired();

            builder.Property(l => l.CategoriaId)
                .IsRequired();

            // Relacionamento 1:N
            builder.HasOne(l => l.Categoria)
                .WithMany() // <== permite múltiplos lugares por categoria
                .HasForeignKey(l => l.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
