using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Social_Network.Helpers.Notifications
{
    public class Email
    {
        public static string ukrNet_pswd = "cH5X3QlTKjOtgsZL";

        public static async Task SendEmailAsync(string email = "fozzynice@gmail.com", string subject = "Запрос", string message = "text", string myHostUrl = "localhost:49131", string Button_Url = "/Home")
        {
            // get template
            string path = Directory.GetCurrentDirectory();
            string FilePath = $"{path}\\Helpers\\Notifications\\EmailTemplate.html";

            // local strings to change
            string Home_Url = "HOMEURL";
            string Privacy_Url = "PRIVACYURL";
            string Help_Url = "HELPURL";

            string Button_text_ToChange = "BUTTONTEXT";
            string Button_Url_ToChange = "BUTTONURL";
            string Main_Text_ToChange = "THEMAINTEXTTOCHANGE";

            string MailText = string.Empty;

            // creating links
            string Home = myHostUrl + "/Home";
            string Privacy = myHostUrl + "/Home/Privacy";
            string Help = myHostUrl + "/Home/Help";

            using (StreamReader SourceReader = System.IO.File.OpenText(FilePath))
            {

                MailText = SourceReader.ReadToEnd();

            }
            //links
            MailText = MailText.Replace(Home_Url, Home);
            MailText = MailText.Replace(Privacy_Url, Privacy);
            MailText = MailText.Replace(Help_Url, Help);

            //Main
            MailText = MailText.Replace(Main_Text_ToChange, message);
            MailText = MailText.Replace(Button_text_ToChange, subject);
            MailText = MailText.Replace(Button_Url_ToChange, Button_Url);
            // sending email
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Triangle", "forzyng@ukr.net"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = MailText
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.ukr.net", 465, true);
                await client.AuthenticateAsync("forzyng@ukr.net", ukrNet_pswd);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
