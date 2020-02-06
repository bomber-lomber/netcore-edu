using HelloKitty.Core;
using System.Collections.Generic;

namespace HelloKitty.Mvc.Models
{
    public class FileListModel
    {
        public Directory CurrentFolder { get; set; }
        public IEnumerable<Directory> Folders { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}