
using EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.CreateMassiveAccountingEntry;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaCore.Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountingEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new accounting entry
        /// </summary>
        /// <param name="command">The command containing the details of the accounting entry to create</param>
        /// <returns>The ID of the created accounting entry</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountingEntryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Create a new accounting entry
        /// </summary>
        /// <param name="command">The command containing the details of the accounting entry to create</param>
        /// <returns>The ID of the created accounting entry</returns>
        [HttpPost]
        [Route("massive")]
        public async Task<IActionResult> CreateMasive([FromBody] CreateMassiveAccountingEntryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
