using Microsoft.AspNetCore.Mvc;
using Minio;
using JWTAuthTemplate.Extensions;
using System.IO;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace JWTAuthTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinioController : ControllerBase
    {
        private readonly MinioService _minioService;

        public MinioController(MinioService minioService)
        {
            _minioService = minioService;
        }

        [HttpPost("create-bucket")]
        public async Task<IActionResult> CreateBucket(string bucketName)
        {
            await _minioService.CreateBucketAsync(bucketName);
            return Ok($"Bucket {bucketName} created.");
        }

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile(string bucketName, string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File {Path.GetFileName(filePath)} not found.");
            }
            await _minioService.UploadFileAsync(bucketName, Path.GetFileName(filePath), filePath);
            return Ok($"File {Path.GetFileName(filePath)} uploaded to bucket {bucketName}.");
        }

        [HttpGet("get-file")]
        public async Task<IActionResult> GetFile(string bucketName, string objectName)
        {
            var fileUrl = await _minioService.GetFileAsync(bucketName, objectName);
            return Ok(new { Url = fileUrl });
        }
    }
}
