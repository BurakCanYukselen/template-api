using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using API.Base.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ConfigurationBuilderExtension
    {
        private const string OCELOT_FILE_NAME = "ocelot.merged.json";

        public static IConfigurationBuilder RegisterMultipleOcelotConfig(this IConfigurationBuilder builder, string relativePath,
            IWebHostEnvironment env)
        {
            DeleteExistingMergedConfigurationFile(relativePath);
            var routes = MergeConfigs(relativePath);
            SaveMergedConfigurationFile(routes, relativePath);
            builder.AddOcelot(relativePath, env);
            return builder;
        }

        public static IEnumerable<dynamic> MergeConfigs(string relativePath)
        {
            var routes = new List<dynamic>();

            var files = relativePath.GetFilesInLocation();
            foreach (var file in files)
                if (Regex.IsMatch(file, "ocelot.([a-zA-Z0-9]*).json"))
                {
                    var route = file.GetJsonContent();
                    routes.Add(route);
                }

            var directories = relativePath.GetDirectoriesInLocation();
            foreach (var directory in directories)
            {
                var nextRelativePath = directory.GetRelativePath(relativePath);
                routes.AddRange(MergeConfigs(nextRelativePath));
            }

            return routes;
        }

        public static void DeleteExistingMergedConfigurationFile(string relativePath)
        {
            var path = relativePath.GetTargetPath();
            var filePath = Path.Join(path, OCELOT_FILE_NAME);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static void SaveMergedConfigurationFile(IEnumerable<dynamic> routes, string relativePath)
        {
            var path = relativePath.GetTargetPath();
            var jsonObject = new {Routes = routes};
            var filePath = Path.Join(path, OCELOT_FILE_NAME);
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject));
        }
    }
}