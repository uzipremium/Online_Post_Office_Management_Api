using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeWithOfficeDto>>> GetAllEmployees()
        {
            var employees = await _mediator.Send(new EmployeeGetAll());
            return Ok(employees);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWithOfficeDto>> GetEmployeeById(string id)
        {
            var employee = await _mediator.Send(new EmployeeGetOne(id));
            return employee is not null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeWithOfficeDto>> CreateEmployeeWithAccount([FromBody] CreateEmployeeAndAccount command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "The command field is required." });
            }

            var employee = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeWithOfficeDto>> UpdateEmployee(string id, [FromBody] UpdateEmployee command)
        {
            command.Id = id;

            var updatedEmployee = await _mediator.Send(command);

            if (updatedEmployee != null)
            {
                return Ok(updatedEmployee); // Trả về đối tượng đã cập nhật
            }

            return NotFound("Employee not found."); // Nếu không tìm thấy nhân viên
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            var result = await _mediator.Send(new DeleteEmployee_Account(id));

            if (result)
            {
                return Ok("Employee and associated account deleted successfully.");
            }

            return NotFound("Employee not found.");
        }
    }
}
