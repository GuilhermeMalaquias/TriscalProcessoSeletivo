using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.OwnsOne(v => v.TotalGeral, p => p.Property("Value").HasColumnName("TotalGeral"));
            builder.OwnsOne(v => v.UsuarioSolicitante, p => p.Property("Nome").HasColumnName("UsuarioSolicitante"));
            builder.OwnsOne(v => v.NomeFornecedor, p => p.Property("Nome").HasColumnName("NomeFornecedor"));
            builder.OwnsOne(v => v.CondicaoPagamento, p => p.Property("Valor").HasColumnName("CondicaoPagamento"));
        }
    }
}
