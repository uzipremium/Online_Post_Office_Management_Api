using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Queries.EmployeeQuery;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Handlers.EmployeeHandler;

namespace Online_Post_Office_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeWithOfficeDto>>> GetAllEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var employees = await _mediator.Send(new EmployeeGetAll(pageNumber, pageSize));
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWithOfficeDto>> GetEmployeeById(string id)
        {
            try
            {
                var employee = await _mediator.Send(new EmployeeGetOne(id));
                return employee is not null ? Ok(employee) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeWithOfficeDto>> CreateEmployeeWithAccount([FromBody] CreateEmployeeAndAccount command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "The command field is required." });
            }

            try
            {
                var employee = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        // Cập nhật nhân viên theo ID
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeWithOfficeDto>> UpdateEmployee(string id, [FromBody] UpdateEmployee command)
        {
            command.Id = id;

            try
            {
                var updatedEmployee = await _mediator.Send(command);

                if (updatedEmployee != null)
                {
                    return Ok(new { message = "Employee and associated account updated successfully." }); 
                }

                return NotFound(new { message = "Employee not found." }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEmployee_Account(id));

                if (result)
                {
                    return Ok(new { message = "Employee and associated account deleted successfully." });
                }

                return NotFound(new { message = "Employee not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<EmployeeWithOfficeDto>>> SearchEmployees([FromQuery] string? name = null, [FromQuery] string? officeId = null, [FromQuery] string? phone = null, [FromQuery] string? officeName = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
       
                var searchCriteria = new EmployeeSearch(name, officeId, phone, officeName, pageNumber, pageSize);

                var employees = await _mediator.Send(new SearchEmployeeQuery(searchCriteria));

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }
}
