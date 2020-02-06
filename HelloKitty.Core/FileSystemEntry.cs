using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HelloKitty.Core
{
    public abstract class FileSystemEntry
    {
        protected FileSystemInfo systemFileInfo;

        public FileSystemEntry(FileSystemEntry parent, System.IO.FileSystemInfo systemFileInfo)
        {
            Parent = parent;
            Name = systemFileInfo.Name;
            this.systemFileInfo = systemFileInfo;
        }

        public FileSystemEntry Parent { get; }
        public IEnumerable<FileSystemEntry> Parents
        {
            get
            {
                var list = new List<FileSystemEntry>();
                var p = Parent;
                while (p != null)
                {
                    list.Insert(0, p);
                    p = p.Parent;
                }
                return list;
            }
        }
        public virtual string Name { get; }

        public string GetPath()
        {
            return systemFileInfo.FullName;
        }

        public string GetRelativePath()
        {
            return GetRelativePath(null);
        }

        public string GetRelativePath(FileSystemEntry root)
        {
            if (root == null)
            {
                if (this is RootDirectory)
                {
                    return string.Empty;
                }
            }
            else  if (root.systemFileInfo == this.systemFileInfo)
            {
                return string.Empty;
            }

            if (Parent != null)
            {
                return System.IO.Path.Combine(Parent.GetRelativePath(root), Name);
            }
            return Name;
        }

        public void Delete()
        {
            systemFileInfo.Delete();
        }

        public abstract void Rename(string newName);
    }
}
