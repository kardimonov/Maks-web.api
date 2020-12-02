using AutoMapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Images;
using GolovinskyAPI.Logic.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GolovinskyAPI.Logic.Services
{
    public class PictureService : IPictureService
    {
        public float biggestside;
        public float prop;
        public int needside;
        private readonly IPictureRepository repo;
        private readonly IMapper mapper;

        public PictureService(IPictureRepository repository, IMapper map)
        {
            repo = repository;
            mapper = map;
        }

        public bool UploadPicture(NewUploadImageInput input)
        {
            try
            {
                var image = Image.FromStream(input.Img.OpenReadStream(), true, true);
                var s = new Size(image.Width, image.Height);
                var bmp = new Bitmap(image, s);
                byte[] fileBytes;

                if (image.Width > image.Height)
                {
                    biggestside = image.Width;
                    prop = biggestside / image.Height;
                    var newheight = 720 / prop;
                    needside = Convert.ToInt32(newheight);

                    if (biggestside > 720)
                    {
                        var ts = DateTime.Now;
                        Console.WriteLine("Image compressing started at " + ts.ToString());
                        var bmp2 = new Bitmap(image, new Size(720, needside));
                        var rectangle = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                        var bitmapData = bmp2.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        bmp2.UnlockBits(bitmapData);

                        using (var ms = new MemoryStream())
                        {
                            bmp2.Save(ms, ImageFormat.Jpeg);
                            fileBytes = ms.ToArray();
                        }
                        var result = mapper.Map<NewUploadImageInputByte>(input, opt => opt.Items["Img"] = fileBytes);
                        //var result = new NewUploadImageInputByte
                        //{
                        //    AppCode = input.AppCode,
                        //    TImageprev = input.TImageprev,
                        //    Img = fileBytes
                        //};
                        
                        Console.WriteLine("Image compressing done/ elapsed " + (DateTime.Now - ts).TotalMilliseconds);
                        ts = DateTime.Now;
                        Console.WriteLine("uploading image done. elapsed " + (DateTime.Now - ts).TotalMilliseconds);

                        var resObj = repo.UploadPicture(result);
                        return (resObj == "1");
                    }

                    else
                    {
                        using (var ms = new MemoryStream())
                        {
                            bmp.Save(ms, ImageFormat.Png);
                            fileBytes = ms.ToArray();
                        }
                        var result = mapper.Map<NewUploadImageInputByte>(input, opt => opt.Items["Img"] = fileBytes);
                        //var result = new NewUploadImageInputByte
                        //{
                        //    AppCode = input.AppCode,
                        //    TImageprev = input.TImageprev,
                        //    Img = fileBytes
                        //};
                        
                        var resObj = repo.UploadPicture(result);                        
                        return (resObj == "1");
                    }
                }

                else if (image.Height > image.Width)
                {
                    biggestside = image.Height;
                    prop = biggestside / image.Width;
                    var newweight = 720 / prop;
                    needside = Convert.ToInt32(newweight);

                    if (biggestside > 720)
                    {
                        var bmp3 = new Bitmap(image, new Size(needside, 720));
                        var rectangle = new Rectangle(0, 0, bmp3.Width, bmp3.Height);
                        var bitmapData = bmp3.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        bmp3.UnlockBits(bitmapData);
                        using (var ms = new MemoryStream())
                        {
                            bmp3.Save(ms, ImageFormat.Jpeg);
                            fileBytes = ms.ToArray();
                        }
                        var result = mapper.Map<NewUploadImageInputByte>(input, opt => opt.Items["Img"] = fileBytes);
                        //var result = new NewUploadImageInputByte
                        //{
                        //    AppCode = input.AppCode,
                        //    TImageprev = input.TImageprev,
                        //    Img = fileBytes
                        //};
                        
                        var resObj = repo.UploadPicture(result);                       
                        return (resObj == "1");
                    }

                    else
                    {
                        using (var ms = new MemoryStream())
                        {
                            bmp.Save(ms, ImageFormat.Jpeg);
                            fileBytes = ms.ToArray();
                        }
                        var result = mapper.Map<NewUploadImageInputByte>(input, opt => opt.Items["Img"] = fileBytes);
                        //var result = new NewUploadImageInputByte
                        //{
                        //    AppCode = input.AppCode,
                        //    TImageprev = input.TImageprev,
                        //    Img = fileBytes
                        //};
                        
                        var resObj = repo.UploadPicture(result);                        
                        return (resObj == "1");
                    }
                }

                else
                {
                    using (var ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);
                        fileBytes = ms.ToArray();
                    }
                    var result = mapper.Map<NewUploadImageInputByte>(input, opt => opt.Items["Img"] = fileBytes);
                    //var result = new NewUploadImageInputByte
                    //{
                    //    AppCode = input.AppCode,
                    //    TImageprev = input.TImageprev,
                    //    Img = fileBytes
                    //};
                    
                    var resObj = repo.UploadPicture(result);                    
                    return (resObj == "1");
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        public SearchPictureInfoOutputModel SearchPictureInfo(SearchPictureInfoInputModel input)
        {
            var res = repo.SearchPictureInfo(input);
            if (res != null)
            {
                res.AdditionalImages = repo.GetAllAdditionalPictures(input);
                res.Prc_ID = input.Prc_ID;
                int difference = (DateTime.Now - Convert.ToDateTime(res.gdate)).Days;
                res.isActive = difference > 30 ? false : true;
                return res;
            }
            return null;
        }
    }
}