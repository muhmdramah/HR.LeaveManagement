using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocation);

        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await _mediator
                .Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
        {
            var command = new CreateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // PUT api/<LeaveAllocationController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocation)
        {
            var command = new UpdateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await _mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
