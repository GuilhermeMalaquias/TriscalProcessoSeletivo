using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string NomeUsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public List<Item> Items { get; set; }
    }
    public class Item
    {
        public Guid Id { get; set; }
        public int Qtde { get; set; }
    }
}
