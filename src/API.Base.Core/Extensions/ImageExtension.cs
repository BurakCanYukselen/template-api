using API.Base.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API.Base.Core.Extensions
{
    public static class ImageExtensions
    {
        public static KeyValuePair<string, List<string>> JPG => new KeyValuePair<string, List<string>>("jpg", new List<string> {"FF", "D8"});
        public static KeyValuePair<string, List<string>> JPEG => new KeyValuePair<string, List<string>>("jpeg", new List<string> {"FF", "D8"});
        public static KeyValuePair<string, List<string>> BMP => new KeyValuePair<string, List<string>>("bmp", new List<string> {"42", "4D"});
        public static KeyValuePair<string, List<string>> GIF => new KeyValuePair<string, List<string>>("gif", new List<string> {"47", "49", "46"});
        public static KeyValuePair<string, List<string>> PNG => new KeyValuePair<string, List<string>>("png", new List<string> {"89", "50", "4E", "47", "0D", "0A", "1A", "0A"});

        public static string GetExtension(this byte[] image)
        {
            using (var stream = new MemoryStream(image))
            {
                stream.Seek(0, SeekOrigin.Begin);
                var signature = new List<string>();
                for (int i = 0; i < 10; i++)
                    signature.Add(stream.ReadByte().ToString("X2"));

                if (!JPEG.Value.Except(signature).Any())
                    return JPEG.Key;
                else if (!BMP.Value.Except(signature).Any())
                    return BMP.Key;
                else if (!GIF.Value.Except(signature).Any())
                    return GIF.Key;
                else if (!PNG.Value.Except(signature).Any())
                    return PNG.Key;
            }

            throw new FormatException("Not an allowed image data");
        }

        public static bool ValidateImageExtension(this byte[] image, params KeyValuePair<string, List<string>>[] extensions)
        {
            using (var stream = new MemoryStream(image))
            {
                stream.Seek(0, SeekOrigin.Begin);
                var signature = new List<string>();
                for (int i = 0; i < 10; i++)
                    signature.Add(stream.ReadByte().ToString("X2"));

                foreach (var extension in extensions)
                {
                    if (!extension.Value.Except(signature).Any())
                        return true;
                }

                return false;
            }
        }

        // TODO: Add nuget package...
        // public static bool ValidateImageSize(this byte[] image, int width, int height)
        // {
        //     using (var stream = new MemoryStream(image))
        //     using (var imageFromStream = System.Drawing.Image.FromStream(stream))
        //     {
        //         if (imageFromStream.Height <= height && imageFromStream.Width <= width)
        //             return true;
        //         return false;
        //     }
        // }
    }
}
