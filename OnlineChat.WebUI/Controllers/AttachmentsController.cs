using Application.CQRS.Commands.Attachment;
using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly ISender _sender;

        public AttachmentsController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost]
        public async Task<ActionResult> UploadFileToDirectMessageByMessageIdAsync([FromBody] UploadFileToDirectMessageByMessageIdRequest request)
        {
            return Ok(await _sender.Send(new UploadFileToDirectMessageByMessageIdCommand(request)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetFileFromAttachmentById(int id)
        {
            var downloadInfo = await _sender.Send(new GetFileFromAttachmentByIdCommand(id));
            return File(downloadInfo.Content, downloadInfo.ContentType);
        }
    }
}
