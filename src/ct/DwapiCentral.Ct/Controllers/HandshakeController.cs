using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace DwapiCentral.Ct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HandshakeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HandshakeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }




    }
}
