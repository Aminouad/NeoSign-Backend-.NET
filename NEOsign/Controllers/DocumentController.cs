
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using io=System.IO ;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;


        public DocumentController( DataContext context, IHostingEnvironment hostingEnvironment, IDocumentService documentService,IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _userService = userService;

        }
        [HttpPost, Authorize]
        public async Task<IActionResult> AddDocument([FromForm] DocumentDto documentDto)
        {
            if (documentDto.File != null)
            {
                var baseURL = _hostingEnvironment.ContentRootPath;
                var owner = _userService.GetUserByEmail(documentDto.Owner);
                var fileName = Path.GetFileName(documentDto.File.FileName);
                var filePath = Path.Combine("NEOsignDocument", "User"+owner.Id, fileName);
                string dir = Path.Combine(baseURL, "NEOsignDocument", "User" + owner.Id);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await documentDto.File.CopyToAsync(fileSteam);
                }
                Document doc = new Document();
                doc.Name = documentDto.Name;
                doc.Path = filePath;
                byte[] fileByte = io.File.ReadAllBytes(doc.Path);
                var document= new Document();
                document.Name = documentDto.Name;
                document.Type = documentDto.Type;
                document.Etat = documentDto.Etat;
                document.Date = documentDto.Date;
                document.Description = documentDto.Description;
                document.Path = filePath;
                document.Content = fileByte;
                document.User = owner;
                await _documentService.AddDocument(document);
                string json = JsonConvert.SerializeObject(document, Formatting.Indented, new JsonSerializerSettings
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

       
        [HttpGet("{userId}"), Authorize]
        public async Task<IActionResult> GetDocument(string userId)
        {

            string json = JsonConvert.SerializeObject(
                _documentService.GetDocumentsByUser(Int16.Parse(userId))
                , Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return  Ok(json);

        }
        [HttpDelete("{documentId}"), Authorize]
        public async Task<string> DeleteDocument(string documentId)
        {
            if (documentId == null) return null;
            var id=Int16.Parse(documentId);          
            return await _documentService.DeleteDocument(id); 

        }
        [HttpPut, Authorize]
        public async Task<IActionResult> ModifyDocument(EditDocumentDto editDocumentDto )
        {
            if (editDocumentDto != null)
            {
                var modifiedDocument = await _documentService.ModifyDocument(editDocumentDto.Id, editDocumentDto.Name, editDocumentDto.Description);

               
                string json = JsonConvert.SerializeObject(modifiedDocument, Formatting.Indented, new JsonSerializerSettings
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
