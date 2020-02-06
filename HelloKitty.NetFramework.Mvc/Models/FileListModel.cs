using HelloKitty.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloKitty.NetFramework.Mvc.Models
{
    public class FileListModel
    {
        public Directory CurrentFolder { get; set; }
        public IEnumerable<Directory> Folders { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}