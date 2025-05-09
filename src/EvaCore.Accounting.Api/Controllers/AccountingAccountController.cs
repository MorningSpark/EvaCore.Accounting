using EvaCore.Accounting.Application.Commands.AccountingAccounts.CreateAccountingAccount;
using EvaCore.Accounting.Application.Commands.AccountingAccounts.FetchAccountingAccount;
using EvaCore.Accounting.Application.Commands.AccountingAccounts.ResumeAccountingAccount;
using EvaCore.Accounting.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EvaCore.Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountingAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new accounting account
        /// </summary>
        /// <param name="command">The command containing the details of the accounting account to create</param>
        /// <returns>The ID of the created accounting account</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountingAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountingAccount>>> ObtenerCuentasFiltradas(
        [FromQuery] int? id,
        [FromQuery] int? parentId,
        [FromQuery] string? referenceCode,
        [FromQuery] string? reference,
        [FromQuery] string? name,
        [FromQuery] string? type,
        [FromQuery] int? level,
        [FromQuery] DateTime? creationDate)
        {
            FetchAccountingAccountCommand query = new FetchAccountingAccountCommand
            {
                Id = id ?? 0,
                ParentId = parentId ?? 0,
                ReferenceCode = referenceCode,
                Reference = reference,
                Name = name,
                Type = type,
                Level = level ?? 0,
            };


            var cuentas = await _mediator.Send(query);
            return Ok(cuentas);
        }

        /// <summary>
        /// Create a new accounting entry
        /// </summary>
        /// <param name="command">The command containing the details of the accounting entry to create</param>
        /// <returns>The ID of the created accounting entry</returns>
        [HttpPost]
        [Route("resume")]
        public async Task<IActionResult> CreateMasive([FromBody] ResumeAccountingAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
