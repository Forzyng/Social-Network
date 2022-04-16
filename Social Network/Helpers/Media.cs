using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ImageProcessor.Imaging;
using System.Drawing;

namespace Social_Network.Helpers
{
    public class Media
    {
        public static List<string> AExtensions =  new List<string> { ".jpg", ".jpeg", ".bmp", ".png" };
    
        public static string WebRootStoragePath = "";

        public static String CreateDirectory(String directoryPath)
        {
            DateTime date = DateTime.Now;
            if (!Directory.Exists(WebRootStoragePath + directoryPath + "\\" + date.Year))
                Directory.CreateDirectory(WebRootStoragePath + directoryPath + "\\" + date.Year);

            if (!Directory.Exists(WebRootStoragePath + directoryPath + "\\" + date.Year + "\\" + date.Month))
                Directory.CreateDirectory(WebRootStoragePath + directoryPath + "\\" + date.Year + "\\" + date.Month);

            return directoryPath + "/" + date.Year + "/" + date.Month;
        }

        /// <summary>
        /// На основе данных по операционной системы про Content Type выбирает расширение файла
        /// TODO: было бы здорово уйти от винды - и решитьв опрос для любой ОС
        /// </summary>
        /// <param name="mimeType">Тип содержимого</param>
        /// <returns>Расширение файла</returns>
        public static string GetDefaultExtension(string mimeType)
        {
            string result;
            RegistryKey key;
            object value;

            key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        
        /// <summary>
        /// Принимает файловый поток и сохраняет его в указанное место в Storage
        /// </summary>
        /// <param name="fileToStorage">Файл для сохранения</param>
        /// <param name="path">Путь второй папки</param>
        /// <returns>Url</returns>
        public  static string UploadImages(IFormFile fileToStorage, string path = "tmp")
        {
            if (fileToStorage != null && fileToStorage.Length > 0)
            {
                string currentExtension = Path.GetExtension(fileToStorage.FileName).ToLower();

                if(!AExtensions.Contains(currentExtension))
                {
                    return string.Empty;
                }


                string webPFileName = Guid.NewGuid().ToString() + ".webp";
                string webPFilePath = CreateDirectory(path) + "/" + webPFileName;

        
                using (var webPFileStream = new FileStream(path, FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        imageFactory.Load(fileToStorage.OpenReadStream())
                                    .Format(new WebPFormat())
                                    .Quality(100)
                                    .Save(WebRootStoragePath + webPFilePath);
                    }
                }



              
                return "/storage/" + webPFilePath;
            }
            return null;
        }

        public static string UploadProfilePictures(IFormFile fileToStorage, string path = "tmp")
        {
            if (fileToStorage != null && fileToStorage.Length > 0)
            {
                string currentExtension = Path.GetExtension(fileToStorage.FileName).ToLower();

                if (!AExtensions.Contains(currentExtension))
                {
                    return string.Empty;
                }


                string webPFileName = Guid.NewGuid().ToString() + ".webp";
                string webPFilePath = CreateDirectory(path) + "/" + webPFileName;


                using (var webPFileStream = new FileStream(path, FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        imageFactory.Load(fileToStorage.OpenReadStream())
                             .Format(new WebPFormat())
                             .Resize(new ResizeLayer(new Size(200, 200), ResizeMode.Crop))
                             .Quality(85)
                             .Save(WebRootStoragePath + webPFilePath);
                    }
                }




                return "/storage/" + webPFilePath;
            }
            return null;
        }



    }
}
