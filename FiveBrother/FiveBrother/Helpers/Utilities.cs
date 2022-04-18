using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FiveBrother.Helpers
{
    public class Utilities
    {
        public static int PAGE_SIZE = 20;
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            var result = url.ToLower().Trim();
            url = Regex.Replace(url, @"[áàạảãâấầẩậẫăắằạẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẻẽẹêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòỏõọốồổỗộ]", "o");
            url = Regex.Replace(url, @"[íìịỉì]", "i");
            url = Regex.Replace(url, @"[ýỳỷỹ]", "y");
            url = Regex.Replace(url, @"[úùụủũứừửữự]", "u");
            url = Regex.Replace(url, @"[đ]", "d");

            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            url = Regex.Replace(url.Trim(), @"\s+", "-").Trim();
            url = Regex.Replace(url.Trim(), @"\s", "-").Trim();
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        public static string GetRandomKey(int lenght = 5)
        {
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lenght; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
                string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                if (!System.IO.Directory.Exists(path2))
                {
                    System.IO.Directory.CreateDirectory(path2);
                }
                var supportedTypes = new[] { "ipg", "ipeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
