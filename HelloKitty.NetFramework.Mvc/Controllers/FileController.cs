using HelloKitty.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloKitty.NetFramework.Mvc.Controllers
{
    [Route("File/{action}/{*path}", Name = "files-default", Order=99)]
    public class FileController : Controller
    {
        [Route("~/Files/{*path}", Name = "files-list", Order=98)]
        public ActionResult Index(string path)
        {
            var currentDir = GetFileSystem(path, true) as Directory;
            var model = new Models.FileListModel()
            {
                CurrentFolder = currentDir,
                Folders = Directory.GetDirectories(currentDir),
                Files = Core.File.GetFiles(currentDir)
            };
            return View(model);
        }

        public ActionResult Details(string path)
        {
            var fileInfo = GetFileSystem(path, false);
            var filePath = fileInfo.GetPath();
            var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            return File(stream, GetContentType(filePath));
        }

        private string GetContentType(string fileName)
        {
            string contentType = MimeMapping.GetMimeMapping(fileName);
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public ActionResult CreateFolder(string path)
        {
            Directory parentDir = (Directory)GetFileSystem(path, true);
            return View(parentDir);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFolder(string path, string name)
        {
            Directory parentDir = (Directory)GetFileSystem(path, true);
            var folderName = name;
            var folder = parentDir.Create(folderName);
            return RedirectToAction(nameof(Index), new { path = PathHelper.ConvertFileSystemPathToPath(folder.GetRelativePath()) });
        }

        public ActionResult Rename(string path, bool isDir = false)
        {
            var model = GetFileSystem(path, isDir);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string path, bool isDir, string name)
        {
            try
            {
                var model = GetFileSystem(path, isDir);
                var newName = name;
                model.Rename(newName);
                return RedirectToAction(nameof(Index), new { path = PathHelper.ConvertFileSystemPathToPath(model.Parent.GetRelativePath()) });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string path, bool isDir = false)
        {
            if (!string.IsNullOrEmpty(path))
            {
                FileSystemEntry entryToDelete = GetFileSystem(path, isDir);
                entryToDelete.Delete();
                path = entryToDelete.Parent.GetRelativePath();
            }
            else
            {
                path = string.Empty;
            }
            return RedirectToAction(nameof(Index), new { path = PathHelper.ConvertFileSystemPathToPath(path) });
        }

        private FileSystemEntry GetFileSystem(string path, bool isDir)
        {
            path = path ?? string.Empty;
            var rootFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get(ConfigurationKeys.FilesFolderPath);
            var filesFolder = System.Configuration.ConfigurationManager.AppSettings.Get(ConfigurationKeys.FilesFolderName);
            var filesDir = Directory.GetRootDirectory(filesFolder, rootFolderPath);
            FileSystemEntry result = filesDir;
            var fragments = PathHelper.SplitPathToFragments(path);
            if (fragments.Any())
            {
                Directory currentDir = filesDir;
                foreach (var dirName in fragments.Take(fragments.Count() - 1))
                {
                    currentDir = new Directory(currentDir, dirName);
                }
                result = isDir ? (FileSystemEntry)new Directory(currentDir, fragments.Last()) : new File(currentDir, fragments.Last());
            }
            return result;
        }
    }
}