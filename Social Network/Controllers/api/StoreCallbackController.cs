using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Network.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Social_Network.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCallbackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoreCallbackController(ApplicationDbContext context)
        {
            _context = context;
        }

       

        // POST: api/StoreCallback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Callback>> PostCallback(Callback callback)
        {
            //callback.Photo = await Helpers.Media.UploadImage(fileToStorage, "callback_photo");
            _context.Callbacks.Add(callback);
            await _context.SaveChangesAsync();

            Helpers.Notifications.Telegram.SendMessage("Обрабатывайте парня => " + callback.Name + "\nНомер телефона => " + callback.Phone + "\nИдентификатор => " + callback.Id + "\n\nУ вас вечность");
            //

            //string url = Helpers.GetUrl.GetPageUri(this.HttpContext, "Home/Help");

            string myHostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            string subject = "Help";
            string message = $"<p>We get your message and soon we will contact with you. If you in 30 minites won`t get call, please check your data or try to write in our center one more time. Also check</p>";
            string button_Url = myHostUrl + "Home/Help"; 


            await Helpers.Notifications.Email.SendEmailAsync(callback.Email, subject, message, myHostUrl, button_Url);


            return CreatedAtAction("GetCallback", new { id = callback.Id }, callback);
        }



        private bool CallbackExists(Guid id)
        {
            return _context.Callbacks.Any(e => e.Id == id);
        }
    }
}
