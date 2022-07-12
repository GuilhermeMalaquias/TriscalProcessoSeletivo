using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public List<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private int _diasCondicaoPagamento = 30;
        private int _valorCondicaoPagamento = 50000;

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens = new List<Item>();
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            PopulaItens(itens);
            SumaTotalGeral();
            TrocaCondicaoPagamentoDeAcordoComValor();
            if (!IsValidItensCondicao()) throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
        }

        private bool IsValidItensCondicao()
        {
            return Itens.Count() > 0;
        }

        private void SumaTotalGeral()
        {
            TotalGeral = new Money(Itens.Sum(val => val.Subtotal.Value));
        }

        private void PopulaItens(IEnumerable<Item> itens) 
        {
            Itens.AddRange(itens);
        }

        private void TrocaCondicaoPagamentoDeAcordoComValor() 
        {
            if (TotalGeral.Value > _valorCondicaoPagamento) 
                CondicaoPagamento = new CondicaoPagamento(_diasCondicaoPagamento);

        }
    }
}
