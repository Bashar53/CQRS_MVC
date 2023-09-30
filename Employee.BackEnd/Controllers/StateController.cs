using Employee.BackEnd.Controllers;
using Employee.Core.CQRS.Employee.Command;
using Employee.Core.CQRS.Employee.Query;
using Employee.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Backend.Controllers
{
    public class StateController : APIControllerBase
    {

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VMState>> GetById(int id)
        {
            return await HandleQueryAsync(new GetAllStateByIdQuery(id));
        }
        [HttpGet]
        public async Task<ActionResult<VMState>> GetAllState()
        {
            return await HandleQueryAsync(new GetAllStateQuery());
        }
        [HttpPost]

        public async Task<ActionResult<VMState>> CreateState(VMState command)
        {
            return await HandleCommandAsync(new CreateStateCommand(command));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<VMState>> UpdateState(int id, VMState state)
        {
            return await HandleCommandAsync(new UpdateStateCommand(id, state));
              }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<VMCountry>> DeleteState(int id)
        {
            return await HandleCommandAsync(new DeleteStateCommand(id));
        }
    }
}
