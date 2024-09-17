using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Queries.OfficeQuery;
using Online_Post_Office_Management_Api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Office
        [HttpGet]
        public async Task<ActionResult<List<Office>>> GetAllOffices()
        {
            var offices = await _mediator.Send(new OfficeGetAll());
            return Ok(offices);
        }

        // GET: api/Office/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Office>> GetOfficeById(string id)
        {
            var office = await _mediator.Send(new OfficeGetOne(id));
            return office is not null ? Ok(office) : NotFound();
        }
    }
}
