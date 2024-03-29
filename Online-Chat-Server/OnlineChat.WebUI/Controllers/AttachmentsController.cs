﻿using Application.CQRS.Commands.Attachments;
using Application.CQRS.Queries.Attachments;
using Contracts.Requests.Attachment;
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
        public async Task<ActionResult> UploadFilesToDirectMessageByMessageIdAsync([FromForm] UploadFilesToDirectMessageByMessageIdRequest request)
        {
            return Ok(await _sender.Send(new UploadFilesToDirectMessageByMessageIdCommand(request)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetFileFromAttachmentById(int id)
        {
            var downloadInfo = await _sender.Send(new GetFileFromAttachmentByIdQuery(id));
            return File(downloadInfo.Content, downloadInfo.ContentType);
        }
    }
}
