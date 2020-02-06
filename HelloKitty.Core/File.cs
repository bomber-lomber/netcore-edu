using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HelloKitty.Core
{
    public class File : FileSystemEntry
    {
        private FileInfo FileInfo { get { return (FileInfo)systemFileInfo; } }

        public File(Directory parent, string name) : this(parent, new System.IO.FileInfo(System.IO.Path.Combine(parent.GetPath(), name)))
        {
        }

        public File(Directory parent, System.IO.FileInfo fileInfo) : base(parent, fileInfo)
        {
        }

        public long Size
        {
            get
            {
                return FileInfo.Length;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return FileInfo.LastWriteTimeUtc;
            }
        }

        public static IEnumerable<File> GetFiles(Directory dir)
        {
            var path = dir.GetPath();
            return System.IO.Directory.GetFiles(path).Select(filePath => new File(dir, new System.IO.FileInfo(filePath)));
        }

        public string GetContent()
        {
            return System.IO.File.ReadAllText(GetPath());

        }

        public void Set(string content)
        {
            System.IO.File.WriteAllText(GetPath(), content);
        }

        public override void Rename(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                var path = GetPath();
                var newPath = Path.Combine(Path.GetDirectoryName(path), newName);
                if (!path.Equals(newPath))
                {
                    FileInfo.MoveTo(newPath);
                }
            }
        }
    }
}
