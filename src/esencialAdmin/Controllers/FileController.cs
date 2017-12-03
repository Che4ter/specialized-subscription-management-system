using esencialAdmin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Controllers
{
    public class FileController : BaseController
    {
        private IImageService _iService;
        public FileController(IImageService sService)
        {
            _iService = sService;
        }

        [ResponseCache(Duration = 256,Location = ResponseCacheLocation.Client)]
        public IActionResult DisplayImage(string name)
        {
            if(name != "")
            {
                var result = _iService.loadSubscriptionImage(name);
                if(result != null)
                {
                    return result;
                }
            }

            return this.NotFound();

        }

        public IActionResult DeleteImage(int fileID)
        {

            var result = _iService.deleteSubscriptionPhoto(fileID);
            if (result )
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult downloadImage(String filePath)
        {
            filePath = filePath.Replace("/file/img/", "").Replace("_thumb", "");
            if (filePath != "")
            {
                var result = _iService.loadSubscriptionImage(filePath);
                if (result != null)
                {
                    this.HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=" + result.FileDownloadName);
                    return result;
                }
            }

            return this.NotFound();

        }
        
    }
}
