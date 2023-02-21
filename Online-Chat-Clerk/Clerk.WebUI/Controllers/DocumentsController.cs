using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Application.CQRS.Queries.Documents;
using Contracts.Requests.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clerk.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly ISender _sender;

    public DocumentsController(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpGet]
    public async Task<ActionResult> GeneratePdfByUsageStatisticsAsync([FromQuery] GenerateDocumentByUsageStatisticsRequest request)
    {
        var steam = await _sender.Send(new GenerateDocumentByUsageStatisticsQuery(request));

        return File(steam, "application/pdf", "statistics");
    }
}