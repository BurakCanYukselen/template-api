using System.IO;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace API.Base.Core.Extensions
{
    public static class FileExtensions
    {
        public static string GetRelativePath(this string path, string relativePath)
        {
            var directory = new DirectoryInfo(path);
            var subRelativePath = Path.Combine(relativePath, directory.Name);
            return subRelativePath;
        }

        public static string[] GetDirectoriesInLocation(this string relativePath)
        {
            var targetPath = GetTargetPath(relativePath);
            var directories = Directory.GetDirectories(targetPath);
            return directories;
        }

        public static string[] GetFilesInLocation(this string relativePath)
        {
            var targetPath = GetTargetPath(relativePath);
            var files = Directory.GetFiles(targetPath);
            return files;
        }

        public static string GetTargetPath(this string relativePath)
        {
            var rootPath = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var targetPath = Path.Combine(rootPath.Root, relativePath);
            return targetPath;
        }

        public static dynamic GetJsonContent(this string path)
        {
            var fileContent = File.ReadAllText(path);
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(fileContent);
            var route = dynamicObject.Routes.First;
            return route;
        }
    }
}