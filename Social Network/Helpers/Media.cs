using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.Helpers
{
    public class Media
    {
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
        public async static Task<string> UploadImage(IFormFile fileToStorage, string path = "tmp")
        {
            if (fileToStorage != null)
            {
                //TODO: Определить - картинку ли передал пользователь
                // Если нет - прервать ввод новой доски

                path = Media.CreateDirectory(path); // Добавили год месяц
                path += "/" + Guid.NewGuid().ToString()
                    + GetDefaultExtension(fileToStorage.ContentType);

                using (var fileStream = new FileStream(WebRootStoragePath + path, FileMode.Create))
                {
                    await fileToStorage.CopyToAsync(fileStream);
                }
                return "/storage/" + path;
            }
            return null;
        }
    }
}
