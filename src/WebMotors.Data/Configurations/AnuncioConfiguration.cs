using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMotors.Domain.Entities;

namespace WebMotors.Data.Configuaration
{
    public class AnuncioConfiguration : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Marca)
                .HasMaxLength(45)
                .IsRequired();

            builder
                .Property(p => p.Modelo)
                .HasMaxLength(45)
                .IsRequired();

            builder
                .Property(p => p.Versao)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Ano)
                .IsRequired();

            builder
                .Property(p => p.Quilometragem)
                .IsRequired();

            builder
                .Property(p => p.Observacao)
                .IsRequired();
        }
    }
}
