
using Newtonsoft.Json;
using RestSharp;
using io = System.IO;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {

        private readonly DataContext _context;

        public SignatureController(DataContext context)
        {
            _context = context;
        }

        


        [HttpPost]
        public async Task<IActionResult> SignPdf(DocumentToSign doc)
        {
            var owner = _context.Users.SingleOrDefault(a => a.Email == doc.userEmail);
            var cert = _context.Certificates.SingleOrDefault(a => a.UserId == owner.Id);
            var document = _context.Documents.Where(d => d.User == owner)
                .Where(d => d.Id == doc.idDocument).SingleOrDefault();
            var options = new RestClientOptions("http://localhost:8081/")
            {
                Timeout = 5000,

                ThrowOnAnyError = true
            };
            string s = @"\" + owner.Id;

            var client = new RestClient(options);
            var request = new RestRequest("api/sign-pdf?document&certificate&pawssord", Method.Post);
            request.AlwaysMultipartFormData = true;


            request.AddFile("document", document.Path);


            request.AddParameter("pathCertificate",cert.Path);
            request.AddParameter("password", doc.passwordCertificate);
            request.AddHeader("Content-Type", "multipart/form-data");

            //RestResponse response = await client.ExecuteAsync(request);
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
            _context.Documents.Add(documentSigned);
            await _context.SaveChangesAsync();

            io.File.WriteAllBytes(documentSigned.Path, documentSigned.Content);




            return Ok(response.IsSuccessful);
        }
    }
}
