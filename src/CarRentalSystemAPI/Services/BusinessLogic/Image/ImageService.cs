namespace WebAPI.Services.BusinessLogic.Image
{
    using System.IO;
    using System.Threading.Tasks;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    public class ImageService : IImageService
    {
        public async Task<Stream> ProcessAsync(Stream image, int wantedHeight)
        {
            using var imageResult = Image.Load(image);

            var width = imageResult.Width;
            var height = imageResult.Height;

            if (height > wantedHeight)
            {
                width = (int)((double)wantedHeight / height * width);
            }

            height = wantedHeight;

            imageResult
                .Mutate(i => i
                    .Resize(new Size(width, height)));

            imageResult.Metadata.ExifProfile = null;

            var output = new MemoryStream();

            await imageResult.SaveAsync(output, new JpegEncoder()
            {
                Quality = 85,
            });

            output.Position = 0;

            return output;
        }
    }
}
