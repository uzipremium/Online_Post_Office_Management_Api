using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Commands.ServiceCommand;
using Online_Post_Office_Management_Api.Queries.ServiceQuery;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // API để lấy thông tin một dịch vụ dựa trên ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(string id)
        {
            var service = await _mediator.Send(new GetServiceQuery(id));
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // API để lấy danh sách tất cả các dịch vụ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _mediator.Send(new GetAllServicesQuery());
            return Ok(services);
        }

        // API để tạo một dịch vụ mới - Chỉ Admin được phép
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateService([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest("Service is required.");
            }

            await _mediator.Send(new CreateServiceCommand(service));
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        // API để cập nhật một dịch vụ dựa trên ID - Chỉ Admin được phép
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService(string id, [FromBody] Service service)
        {
            if (id != service.Id)
            {
                return BadRequest("Service ID mismatch.");
            }

            var result = await _mediator.Send(new UpdateServiceCommand(service));
            if (result)
            {
                return Ok("Service updated successfully.");
            }

            return NotFound("Service not found.");
        }

        // API để xóa một dịch vụ dựa trên ID - Chỉ Admin được phép
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            var result = await _mediator.Send(new DeleteServiceCommand(id));
            if (result)
            {
                return Ok("Service deleted successfully.");
            }

            return NotFound("Service not found.");
        }
    }
}
