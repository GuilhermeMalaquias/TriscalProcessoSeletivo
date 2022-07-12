using Microsoft.AspNetCore.Mvc;
using MediatR;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;

namespace SistemaCompra.API.SolicitacaoCompra
{
    [ApiController]
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost, Route("solicitacao-compra/registrar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Registrar([FromBody] RegistrarCompraCommand command)
        {
            _mediator.Send(command);
            return StatusCode(201);
        }
    }
}
