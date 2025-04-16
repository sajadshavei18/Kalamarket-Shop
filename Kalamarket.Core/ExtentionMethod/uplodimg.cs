using Kalamarket.Core.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kalamarket.Core.ExtentionMethod
{
    public static class uplodimg
    {
        public static string CreateImage(IFormFile file)
        {
            try
            {
                string imgname = GeneratCode.GuidCode() + Path.GetExtension(file.FileName);
                string Pathimg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CssSite/ImageSite", imgname);
                string Pathimgthumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/thumb", imgname);

                string imagesecurity = file.ImageSecurity();

                if (imagesecurity == "false")
                    return "false";

                    using (var stream = new FileStream(Pathimg, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                ConvertImage convert = new ConvertImage();
                convert.Image_resize(Pathimg, Pathimgthumb, 400, 400);

                return imgname;
            }
            catch (Exception)
            {
                return "false";
            }

        }

        public static bool DeleteImg(string path, string imagename)
        {
            try
            {
                string FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CssSite/" + path + "/" + imagename);
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
