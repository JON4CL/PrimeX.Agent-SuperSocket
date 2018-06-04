using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prime.Backend.Data;
using Prime.Base.Networking;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Prime.Backend.Controllers
{
    [Route("api/BinaryController")]
    public class BinaryMessageController : Controller
    {
        private readonly ILogger _logger;
        private readonly MessageRecordContext _db;

        public BinaryMessageController(ILogger<BinaryMessageController> logger, MessageRecordContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public async Task<Boolean> PrimeMessageFormatter([FromBody] byte[] bodyData)
        {
            MessageRecord msg = new MessageRecord(new CommMessage(bodyData));
            //_logger.LogInformation( "SOURCE: {0}, COMMAND: {1}, COMPONENT:{2} , DATA:{3}", 
            //                        msg.SourceIP, 
            //                        msg.MessageCommand, 
            //                        msg.ComponentName, 
            //                        (msg.IsBinary ? Encoding.Unicode.GetString(msg.DataBinary) : msg.DataString) );
           // _db.MessageRecords.Add(msg);
            //_db.SaveChanges();
            //_logger.LogInformation("Messages Processed: {0}", _db.MessageRecords.Local.Count);
            return true;
        }

        //FileStreamResult
        //HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=form.pdf");
    }
}
