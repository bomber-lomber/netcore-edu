using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloKitty.Core
{
    public class PathHelper
    {
        public static readonly string PathFragmentsSeparator = "/";

        public static bool IsPathStartsWith(string path, params string[] pathFragments)
        {
            return path.StartsWith(MakePath(pathFragments), StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsPath(string path, params string[] pathFragments)
        {
            return path.Equals(MakePath(pathFragments), StringComparison.OrdinalIgnoreCase);
        }

        public static string MakePath(params string[] pathFragments)
        {
            var path = string.Join(PathFragmentsSeparator, pathFragments.Where(item => !string.IsNullOrEmpty(item)));
            return path;
        }

        public static IEnumerable<string> SplitPathToFragments(string path)
        {
            var fragments = path.Split(new[] { PathFragmentsSeparator }, StringSplitOptions.RemoveEmptyEntries);
            return fragments;
        }

        public static string GetParentPath(string path)
        {
            path = path.TrimEnd(PathFragmentsSeparator.ToArray());
            var idx = path.LastIndexOf(PathFragmentsSeparator);
            if (idx < 0)
            {
                return string.Empty;
            }
            path = path.Substring(0, idx);
            return path;
        }

        /// <summary>
        /// Gets the path from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static string GetPathFromId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return id;
            }
            var bytes = Convert.FromBase64String(id);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Gets the identifier from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetIdFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            var bytes = Encoding.UTF8.GetBytes(path);
            return Convert.ToBase64String(bytes);
        }

        public static string ConvertFileSystemPathToPath(string fileSystemPath)
        {
            if (fileSystemPath == null)
            {
                return string.Empty;
            }
            return fileSystemPath.Replace(System.IO.Path.DirectorySeparatorChar.ToString(), PathHelper.PathFragmentsSeparator);
        }
    }

}
