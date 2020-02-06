using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HelloKitty.Core
{
    public class Directory : FileSystemEntry
    {
        public Directory(Directory parent, System.IO.DirectoryInfo directoryInfo) : base(parent, directoryInfo)
        {
        }

        public Directory(Directory parent, string name): this(parent, new System.IO.DirectoryInfo(System.IO.Path.Combine(parent.GetPath(), name)))
        {
        }

        public static RootDirectory GetRootDirectory(string name, string rootFolderPath)
        {
            return new RootDirectory(name, rootFolderPath);
        }

        public static IEnumerable<Directory> GetDirectories(Directory dir)
        {
            var path = dir.GetPath();
            return System.IO.Directory.GetDirectories(path).Select(dirPath => new Directory(dir, new System.IO.DirectoryInfo(dirPath)));
        }

        public DateTime ModifiedDate
        {
            get
            {
                return systemFileInfo.LastWriteTimeUtc;
            }
        }

        public Directory Create(string folderName)
        {
            var subFolder = ((System.IO.DirectoryInfo)systemFileInfo).CreateSubdirectory(folderName);
            return new Directory(this, subFolder);
        }

        public override void Rename(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                var path = GetPath();
                var newPath = Path.Combine(Path.GetDirectoryName(path), newName);
                if (!path.Equals(newPath))
                {
                    ((System.IO.DirectoryInfo)systemFileInfo).MoveTo(newPath);
                }
            }
        }
    }

    public class RootDirectory : Directory
    {
        private string name;

        public RootDirectory(string name, string rootFolderPath): base(null, new System.IO.DirectoryInfo(rootFolderPath))
        {
            this.name = name;
        }
        public override string Name => this.name;
    }
}
