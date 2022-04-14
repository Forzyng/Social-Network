using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Social_Network.Helpers.Notifications
{
    public class Telegram
    {
        public static string apiTelegram = "5003956566:AAEXjIy9WBIzJZYOoxXbSIZ2ym298MLupHY";
        public static string chatId_my = "1000709329";

        public static bool SendMessage(string msg)
        {


            //string url
            //= $"https://api.telegram.org/bot" + apiTelegram
            //+ "/sendMessage?chat_id=" + chatId_my
            //+ "&text=" + msg;
            string url = "https://api.telegram.org/bot" + apiTelegram + "/sendMessage?chat_id=" + chatId_my + "&text=" + msg;

            var request = WebRequest.Create(url);
            request.Method = "GET";

            var response = request.GetResponse();

            return true;


        }
    }
}
