#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Samples.FileStorage.Models;
using Samples.FileStorage.Services;
#endregion

namespace Samples.FileStorage.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IService _service;

        public ApiController(ILogger<ApiController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("", Name = "GetContainerList")]
        public async Task<IActionResult> GetContainerList()
        {
            ContainerListResponse response = new ContainerListResponse();

            return Ok(response);
        }

        //[HttpGet]
        //[Route("{source}", Name = "GetShareList")]
        //public async Task<IActionResult> GetShareList(string source)
        //{
        //    ShareListResponse response = new ShareListResponse();

        //    return Ok(response);
        //}

        [HttpGet]
        [Route("{source}/{share}", Name = "GetFolderList")]
        public async Task<IActionResult> GetFolderList(string source, string share)
        {
            if(string.IsNullOrEmpty(source) || string.IsNullOrEmpty(share))
            {
                return BadRequest();
            }

            FolderListResponse response = await _service.ListFolders(source, share);
            return Ok(response);
        }

        [HttpGet]
        [Route("{source}/{share}/{folder}", Name = "GetFileList")]
        public async Task<IActionResult> GetFileList(string source, string share, string folder)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(share) || string.IsNullOrEmpty(folder))
            {
                return BadRequest();
            }
            FileListResponse response = await _service.ListFiles(source, share, folder);
            return Ok(response);
        }

        [HttpGet]
        [Route("{source}/{share}/{folder}/{filename}", Name = "GetFile")]
        public async Task<IActionResult> GetFile(string source, string share, string folder, string filename)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(share) || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(filename))
            {
                return BadRequest();
            }
            FileDownloadResponse response = await _service.DownloadFile(source, share, folder, filename);
            return Ok(response);
        }

        [HttpPost]
        [Route("{source}/{share}/{folder}")]
        public async Task<IActionResult> UploadFile(string source, string share, string folder,  [FromBody] FileUploadRequest request)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(share) || string.IsNullOrEmpty(folder))
            {
                return BadRequest();
            }
            FileUploadResponse response = await _service.UploadFile(source, share, folder, request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{source}/{share}/{folder}/{filename}")]
        public async Task<IActionResult> DeleteFile(string source, string share, string folder, string filename)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(share) || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(filename))
            {
                return BadRequest();
            }
            await _service.DeleteFile(source, share, folder, filename);
            return Ok();
        }

    }
}
