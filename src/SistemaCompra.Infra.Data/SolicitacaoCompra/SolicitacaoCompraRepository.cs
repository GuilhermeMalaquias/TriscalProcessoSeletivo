using System;
using System.Collections.Generic;
using System.Text;
using SistemaCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SistemaCompraAgg.ISolicitacaoCompraRepository
    {
        public readonly SistemaCompraContext contexto;
        public SolicitacaoCompraRepository(SistemaCompraContext contexto)
        {
            this.contexto = contexto;
        }
        public void RegistrarCompra(SistemaCompraAgg.SolicitacaoCompra solicitacaoCompra)
        {
            contexto.Set<SistemaCompraAgg.SolicitacaoCompra>().Add(solicitacaoCompra);
        }
    }
}
