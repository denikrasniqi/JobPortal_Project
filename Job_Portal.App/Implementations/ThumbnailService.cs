using Job_Portal.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Job_Portal.App.Implementations
{
    public class ThumbnailService : IThumbnailService
    {
        public void GenerateThumbnail(int size, string inputPath, string outputPath)
        {
            try
            {
                using (Image image = Image.Load(inputPath))
                {
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = size;
                        height = size; /*Convert.ToInt32(image.Height * size / (double)image.Width);*/
                    }
                    else
                    {
                        width = size; /*Convert.ToInt32(image.Width * size / (double)image.Height);*/
                        height = size;
                    }
                    image.Mutate(x => x.Resize(width, height));
                    image.Save(outputPath); // automatic encoder selected based on extension.
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
