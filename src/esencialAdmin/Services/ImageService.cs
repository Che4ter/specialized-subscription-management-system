using esencialAdmin.Data.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Processing;

namespace esencialAdmin.Services
{
    public class ImageService : IImageService
    {
        protected readonly esencialAdminContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ImageService(esencialAdminContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }


        public FileStreamResult loadSubscriptionImage(String URL)
        {
            try
            {
                String[] parts = URL.Split("/");
                if (parts.Length != 2)
                {
                    return null;
                }
                String originalName = parts[1].Replace("_thumb", "");
                String originalWithoutThumb = parts[1];
                int subID = int.Parse(parts[0]);
                var file = this._context.SubscriptionPhotos.Where(x => x.FkSubscriptionId == subID && x.FkFile.OriginalName == originalName).Select(x => x.FkFile).FirstOrDefault();
                if (file == null)
                {
                    return null;
                }

                string userID = this._context.Subscription.Where(x => x.Id == subID).Select(x => x.FkCustomerId).FirstOrDefault().ToString();

                String path = _hostingEnvironment.WebRootPath + file.Path + file.FileName;
                using (Image<SixLabors.ImageSharp.Rgba32> image = Image.Load(path)) //open the file and detect the file type and decode it
                {
                    if (originalWithoutThumb.Contains("_thumb"))
                    {
                        var tmp = new ResizeOptions();
                        
                        tmp.Mode = ResizeMode.Max;
                        tmp.Size = new SixLabors.Primitives.Size(250,180);

                       image.Mutate(x => x.AutoOrient().Resize(tmp));
                    }
                    Stream outputStream = new MemoryStream();

                    switch (file.ContentType)
                    {
                        case "image/jpeg":
                        case "image/jpg":
                            image.Save(outputStream, new JpegEncoder());
                            break;
                        case "image/png":
                            image.Save(outputStream, new PngEncoder());
                            break;
                        case "image/gif":
                            image.Save(outputStream, new GifEncoder());
                            break;
                    }
                    outputStream.Seek(0, SeekOrigin.Begin);
                    FileStreamResult result = new FileStreamResult(outputStream, file.ContentType);
                    result.FileDownloadName = file.OriginalName;
                    return result;
                }
            }
            catch (Exception ex)
            {

            }

            return null;

        }
        public bool deleteSubscriptionPhoto(int fileID)
        {
            try
            {
                var file = this._context.Files.Where(x => x.Id == fileID).FirstOrDefault();
                if (file == null)
                {
                    return false;
                }

                String path = _hostingEnvironment.WebRootPath + file.Path + file.FileName;


                if (File.Exists(path))
                {
                    File.Delete(path);

                }

                this._context.SubscriptionPhotos.Remove(this._context.SubscriptionPhotos.Where(x => x.FkFileId == fileID).FirstOrDefault());
                this._context.Files.Remove(file);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;

        }
    }
}
