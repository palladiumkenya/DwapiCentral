using DwapiCentral.Ct.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Ct.Controllers
{
    public class HandshakeController : Controller
    {
        private readonly IMediator _mediator;


        public HandshakeController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        [Route("api/Handshake")]
        public async Task<IActionResult> Post(Guid session)
        {
            try
            {
                var responce = await _mediator.Send(new UpdateHandshakeCommand(session));

                if (responce.IsSuccess)
                {
                    var message = new
                    {
                        session

                    };

                    return Ok(message);
                }
                else return BadRequest(responce);
            }
            catch (Exception e)
            {
                Log.Error("Handshake error", e);
                return BadRequest(e.Message);
            }
        }
    }
}
