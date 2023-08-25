using Mithril.FileSystem.Exceptions;
using SkiaSharp;

namespace Mithril.FileSystem.Services
{
    /// <summary>
    /// Image class
    /// </summary>
    public class Image : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        public Image(FileCurator.Interfaces.IFile? fileInfo)
        {
            if (fileInfo?.Exists != true)
                return;
            using var Stream = new MemoryStream(fileInfo.ReadBinary());
            Bitmap = SKBitmap.Decode(Stream);
            Height = Bitmap.Height;
            Width = Bitmap.Width;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        /// <value>The bitmap.</value>
        private SKBitmap? Bitmap { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Resizes the image to the specified maximum width.
        /// </summary>
        /// <param name="maxWidth">The maximum width.</param>
        /// <returns>This.</returns>
        public Image Resize(int maxWidth)
        {
            Bitmap = ResizeBitmap(maxWidth, Bitmap);
            Height = Bitmap?.Height ?? 0;
            Width = Bitmap?.Width ?? 0;
            return this;
        }

        /// <summary>
        /// Saves the image to the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>This.</returns>
        /// <exception cref="ImageSaveException">Data could not be encoded</exception>
        public Image Save(FileCurator.Interfaces.IFile? location, int quality)
        {
            if (Bitmap is null || location is null)
                return this;
            using var MemoryStream = new MemoryStream();
            using var WStream = new SKManagedWStream(MemoryStream);
            _ = Bitmap.Encode(WStream, SKEncodedImageFormat.Jpeg, quality);
            var Data = MemoryStream.ToArray();
            if (Data is null || Data.Length == 0)
                throw new ImageSaveException("Data could not be encoded");
            _ = location.Write(Data);
            return this;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            Bitmap?.Dispose();
            Bitmap = null;
        }

        /// <summary>
        /// Resizes the bitmap
        /// </summary>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>Final bitmap.</returns>
        private static SKBitmap? ResizeBitmap(int maxWidth, SKBitmap? bitmap)
        {
            if (bitmap is null)
                return bitmap;
            if (maxWidth >= bitmap.Width)
                return bitmap;
            var Height = (int)Math.Round(bitmap.Height * (maxWidth / (double)bitmap.Width), MidpointRounding.AwayFromZero);
            var FinalBitmap = new SKBitmap(maxWidth, Height, bitmap.ColorType, bitmap.AlphaType);
            var Factor = Height / (float)bitmap.Height;
            using (var Canvas = new SKCanvas(FinalBitmap))
            {
                Canvas.SetMatrix(SKMatrix.CreateScale(Factor, Factor));
                Canvas.DrawBitmap(bitmap, 0, 0);
                Canvas.ResetMatrix();
            }
            bitmap.Dispose();
            return FinalBitmap;
        }
    }
}