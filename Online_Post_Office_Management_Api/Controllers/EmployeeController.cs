using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _mediator.Send(new EmployeeGetAll());
            return Ok(employees);
        }


        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(string id)
        {
            var employee = await _mediator.Send(new EmployeeGetOne(id));
            return employee is not null ? Ok(employee) : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployeeWithAccount([FromBody] CreateEmployeeAndAccount command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "The command field is required." });
            }

            var employee = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployee command)
        {
            if (id != command.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result > 0)
            {
                return Ok("Employee updated successfully.");
            }

            return NotFound("Employee not found.");
        }

       
        [Authorize(Roles = "Admin")]
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
