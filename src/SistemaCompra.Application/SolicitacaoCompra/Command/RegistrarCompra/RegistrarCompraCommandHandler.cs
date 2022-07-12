using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SistemaCompra.Infra.Data.UoW;
using SistemaCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SistemaCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository produtoRepository;
        public RegistrarCompraCommandHandler(
            SistemaCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository,
            ProdutoAgg.IProdutoRepository produtoRepository,
            IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
            this.produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacao =
                new SistemaCompraAgg.SolicitacaoCompra(request.NomeUsuarioSolicitante, request.NomeFornecedor);
            solicitacao.RegistrarCompra(PopulaItem(request.Items));
            solicitacaoCompraRepository.RegistrarCompra(solicitacao);
            Commit();
            return Task.FromResult(true);
        }

        public List<SistemaCompraAgg.Item> PopulaItem(List<Item> items)
        {
            var itemsAgg = new List<SistemaCompraAgg.Item>();
            foreach (var item in items)
            {
                var produto = produtoRepository.Obter(item.Id);
                itemsAgg.Add(new SistemaCompraAgg.Item(produto, item.Qtde));
            }
            return itemsAgg;
        }
    }
}
