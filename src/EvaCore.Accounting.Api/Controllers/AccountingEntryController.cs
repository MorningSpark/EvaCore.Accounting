
using EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.CreateMassiveAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.DeleteAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.FetchAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.ResumeAccountingEntry;
using MediatR;
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
        [HttpGet]
        public async Task<IActionResult> FetchAccountingEntryRange([FromQuery] DateTime? initialDate,
                                                                   [FromQuery] DateTime? finalDate
                                                                   )
        {
            var command = new FetchAccountingEntryRangeCommand
            {
                InitialDate = initialDate,
                FinalDate = finalDate
            };
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

        /// <summary>
        /// Create a new accounting entry
        /// </summary>
        /// <param name="command">The command containing the details of the accounting entry to create</param>
        /// <returns>The ID of the created accounting entry</returns>
        [HttpPost]
        [Route("GeneralLedger")]
        public async Task<IActionResult> GeneralLedger([FromBody] ResumeAccountingEntryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccountingEntry([FromQuery] int? accountingEntryId)
        {
            var command = new DeleteAccountingEntryCommand
            {
                AccountingEntryId = accountingEntryId ?? 0
            }; 
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
