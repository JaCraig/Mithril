using BigBook;
using BigBook.ExtensionMethods;
using FileCurator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mithril.Core.Abstractions.Mvc.Attributes;
using Mithril.Data.Abstractions.ExtensionMethods;
using Mithril.FileSystem.Abstractions.Services;
using Mithril.FileSystem.Services;
using Mithril.FileSystem.ViewModels;
using System.Globalization;

namespace Mithril.FileSystem.Controllers
{
    /// <summary>
    /// File browser controller
    /// </summary>
    /// <seealso cref="Controller"/>
    /// <remarks>Initializes a new instance of the <see cref="FileBrowserController"/> class.</remarks>
    /// <param name="fileManager">The file manager.</param>
    [Area("Services")]
    [Authorize]
    [Route("/Services/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AddHeader("X-Frame-Options", "SAMEORIGIN")]
    public class FileBrowserController(IFileSystemService? fileManager) : Controller
    {
        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>The file manager.</value>
        public IFileSystemService? FileManager { get; } = fileManager;

        /// <summary>
        /// Returns the file browser based on the file type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The view.</returns>
        [HttpGet("{type}")]
        public IActionResult Browser(string type)
        {
            type = type.Keep(StringFilter.Alpha);
            IDirectory? Directory = FileManager?.Directory($"mithril://{type}s/uploads/{User.GetName()}");
            _ = Directory?.Create();
            return View(new FileBrowserDirectoryVM(Directory, type, null, FileManager));
        }

        /// <summary>
        /// Uploads files to the browser.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="file">The file.</param>
        /// <returns>The result.</returns>
        [HttpPost("{type}")]
        public IActionResult Browser(string type, IFormFile file)
        {
            type = type.Keep(StringFilter.Alpha);
            return GetMediaView(type, file);
        }

        /// <summary>
        /// Gets the media view.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private IActionResult GetMediaView(string type, IFormFile? file)
        {
            type = type.Keep(StringFilter.Alpha);
            var FileName = file?.FileName;
            FileName = $"mithril://{type}s/uploads/{User.GetName()}/{DateTime.UtcNow.ToString("hhmmss", CultureInfo.InvariantCulture)}-{FileName}";

            IFile? FinalFile = FileManager?.File(FileName);
            IDirectory? RootDirectory = FileManager?.Directory($"mithril://{type}s/uploads/{User.GetName()}/");
            if (RootDirectory is null || FinalFile is null)
                return BadRequest();
            if (!FinalFile.FullName.Contains(RootDirectory.FullName, StringComparison.OrdinalIgnoreCase))
                return BadRequest();

            _ = FinalFile.Write(file?.OpenReadStream().ReadAllBinary() ?? []);

            if (string.Equals(type, "image", StringComparison.OrdinalIgnoreCase))
                MipMap(FinalFile);

            IDirectory? Directory = FileManager?.Directory($"mithril://{type}s/uploads/{User.GetName()}/");
            _ = Directory?.Create();
            return View(new FileBrowserDirectoryVM(Directory, type, FinalFile, FileManager));
        }

        /// <summary>
        /// Mips the map.
        /// </summary>
        /// <param name="finalFile">The final file.</param>
        private void MipMap(IFile finalFile)
        {
            ResizeImage(finalFile, 2048);
            ResizeImage(finalFile, 1024);
            ResizeImage(finalFile, 512);
            ResizeImage(finalFile, 256);
            ResizeImage(finalFile, 128);
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="finalFile">The final file.</param>
        /// <param name="size">The size.</param>
        private void ResizeImage(IFile finalFile, int size)
        {
            using var TempImage = new Image(finalFile);
            if (TempImage.Width < size)
                return;
            _ = TempImage.Resize(size);
            var FileName = $"{finalFile.Directory?.FullName}/{TempImage.Width}-{finalFile.Name}";
            _ = TempImage.Save(FileManager?.File(FileName), 100);
        }
    }
}