
using Newtonsoft.Json;
using RestSharp;
using io = System.IO;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ICertificateService _certificateService;
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        public SignatureController(ICertificateService certificateService, IUserService userService, IDocumentService documentService)
        {
            _certificateService = certificateService;
            _userService= userService;
            _documentService = documentService;
        }      
        [HttpPost]
        public async Task<IActionResult> SignPdf(DocumentToSign documentToSign)
        {
            var owner = _userService.GetUserByEmail(documentToSign.userEmail);
            var cert = _certificateService.GetCertificate(owner.Id);          
            var document = _documentService.GetDocumentById(documentToSign.idDocument);
            var options = new RestClientOptions("http://localhost:8081/")
            {
                Timeout = -1,
                ThrowOnAnyError = true
            };
            string s = @"\" + owner.Id;
            var client = new RestClient(options);
            var request = new RestRequest("api/sign-pdf?document&certificatePath&pawssord&typeOfSignature", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddFile("document", document.Path);
            request.AddParameter("pathCertificate",cert.Path);
            request.AddParameter("nature", documentToSign.nature);
            request.AddParameter("label", documentToSign.label);
            request.AddParameter("pathImageCertificate", cert.PathImage);
            request.AddParameter("password", documentToSign.passwordCertificate);
            request.AddParameter("typeOfSignature", documentToSign.typeOfSignature);
            request.AddHeader("Content-Type", "multipart/form-data");
            var response = await client.ExecuteAsync(request);
            var fileByte = response.RawBytes;
            Document documentSigned = new Document();
            documentSigned.Name = document.Name + "-Signed";
            documentSigned.Type = document.Type;
            documentSigned.Date = document.Date;
            documentSigned.Description = document.Description;
            documentSigned.Etat = "signé";
            var filePath = Path.Combine("NEOsignDocument", "User" + owner.Id, "DOC-" + document.Id + "SIGNED.PDF");
            documentSigned.Path = filePath;
            documentSigned.User = owner;
            documentSigned.User = owner;
            documentSigned.Content = fileByte;         
            await  _documentService.AddDocument(documentSigned);
            io.File.WriteAllBytes(documentSigned.Path, documentSigned.Content);
            return Ok(response.IsSuccessful);
        }
    }
}
