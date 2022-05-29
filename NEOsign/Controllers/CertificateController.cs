using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly ICertificateService _certificateService;
        private readonly IUserService _userService;

        public CertificateController(ICertificateService certificateService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;

            _certificateService = certificateService;
            _userService = userService;

        }
        [HttpPost]
        public async Task<IActionResult> AddCertificate([FromForm] CertificateDto certificateDto)
        {
            if (certificateDto.File != null)
            {
                var baseURL = _hostingEnvironment.ContentRootPath;

                var owner = _userService.GetUserByEmail(certificateDto.Owner);

                var fileName = Path.GetFileName(certificateDto.File.FileName);

                var filePath = Path.Combine(@"C:\Users\ao\Documents\ApiSignatureNeoSign\ApiSignature\src\main\resources\", "CertificateUser" + owner.Id, fileName);
                string dir = Path.Combine(baseURL, @"C:\Users\ao\Documents\ApiSignatureNeoSign\ApiSignature\src\main\resources\", "CertificateUser" + owner.Id);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await certificateDto.File.CopyToAsync(fileSteam);
                }
                Certificate cert = new Certificate();

                


                cert.Type = certificateDto.type;

                cert.User = owner;
                cert.UserId = owner.Id;
                cert.Path = filePath;
                await _certificateService.AddCertificate(cert);


                string json = JsonConvert.SerializeObject(cert, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Ok(json);

            }
            else
            {
                return Ok("erreur");

            }


        }
    }
}
