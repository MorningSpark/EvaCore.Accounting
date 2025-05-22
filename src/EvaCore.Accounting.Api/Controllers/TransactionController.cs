
using EvaCore.Accounting.Application.Commands.AccountingTransactions.CreateAccountingTransaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaCore.Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new accounting entry
        /// </summary>
        /// <param name="command">The command containing the details of the accounting entry to create</param>
        /// <returns>The ID of the created accounting entry</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountingTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
