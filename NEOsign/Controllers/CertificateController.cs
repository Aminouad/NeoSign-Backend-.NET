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
                String imageName = null;
                String imagePath = null;

                if (certificateDto.Image!= null)
                {
                     imageName = Path.GetFileName(certificateDto.Image.FileName);
                     imagePath = Path.Combine(@"C:\Users\ao\Documents\ApiSignatureNeoSign\ApiSignature\src\main\resources\", "CertificateUser" + owner.Id, imageName);
                }            
                string dir = Path.Combine(baseURL, @"C:\Users\ao\Documents\ApiSignatureNeoSign\ApiSignature\src\main\resources\", "CertificateUser" + owner.Id);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await certificateDto.File.CopyToAsync(fileSteam);
                }
                if(imagePath != null && imagePath!= null)
                {
                    using (var fileSteamImg = new FileStream(imagePath, FileMode.Create))
                    {
                        await certificateDto.Image.CopyToAsync(fileSteamImg);
                    }
                }               
                Certificate certificate = new Certificate();
                certificate.Type = certificateDto.type;
                certificate.User = owner;
                certificate.UserId = owner.Id;
                certificate.Path = filePath;
                certificate.PathImage = imagePath;
                await _certificateService.AddCertificate(certificate, owner);
                string json = JsonConvert.SerializeObject(certificate, Formatting.Indented, new JsonSerializerSettings
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
