using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;

namespace Cake.NSwag.Console.Settings
{
    /// <summary>
    ///     Fluent extensions methods for the <see cref="SwaggerGeneratorSettings" /> class
    /// </summary>
    public static class SwaggerGeneratorSettingsExtensions
    {
        /// <summary>
        ///     Sets the default URL template to be used when parsing routes
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="template">The url template to use as default</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseUrlTemplate(this SwaggerGeneratorSettings settings, string template)
        {
            settings.DefaultUrlTemplate = template;
            return settings;
        }

        /// <summary>
        ///     Enables representing enums as strings in generated specifications
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseStringEnums(this SwaggerGeneratorSettings settings)
        {
            settings.EnumAsString = true;
            return settings;
        }

        /// <summary>
        ///     Enables representing enums as integers in generated specifications
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseIntegerEnums(this SwaggerGeneratorSettings settings)
        {
            settings.EnumAsString = false;
            return settings;
        }

        /// <summary>
        ///     Adds the given assemblies to the srach paths to gather additional metadata from.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="assemblyPaths">Assemblies to search for metadata.</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings SearchAssemblies(this SwaggerGeneratorSettings settings, params FilePath[] assemblyPaths)
        {
            var a = settings.AssemblyPaths as List<FilePath> ?? settings.AssemblyPaths.ToList();
            a.AddRange(assemblyPaths.ToList());
            settings.AssemblyPaths = a;
            return settings;
        }

        /// <summary>
        ///     Sets the API title for the generated specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="title">API title or name</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings WithTitle(this SwaggerGeneratorSettings settings, string title)
        {
            settings.ApiTitle = title;
            return settings;
        }

        /// <summary>
        ///     Sets the API description for use in the generated specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="description">Description to include with the generated API spec</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings WithDescription(this SwaggerGeneratorSettings settings, string description)
        {
            settings.ApiDescription = description;
            return settings;
        }

        /// <summary>
        ///     Sets the base path for use in the Swagger specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="basePath">Base path to set in the generated specification</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseBasePath(this SwaggerGeneratorSettings settings, string basePath)
        {
            settings.BasePath = basePath;
            return settings;
        }

        /// <summary>
        ///     Sets the service host path for use in the Swagger specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="serviceHost">Service host to set in the generated specification</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseServiceHost(this SwaggerGeneratorSettings settings, string serviceHost)
        {
            settings.ServiceHost = serviceHost;
            return settings;
        }

        /// <summary>
        ///     Sets the service schemes path for use in the Swagger specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="serviceSchemes">Service schemes to set in the generated specification</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseServiceSchemes(this SwaggerGeneratorSettings settings, string serviceSchemes)
        {
            settings.ServiceSchemes = serviceSchemes;
            return settings;
        }

        /// <summary>
        ///     Sets the service schemes path for use in the Swagger specification
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="apiVersion">Service version to set in the generated specification</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings WithVersion(this SwaggerGeneratorSettings settings, string apiVersion)
        {
            settings.ApiVersion = apiVersion;
            return settings;
        }

        /// <summary>
        ///     Sets the runtime to use for the Swagger generation
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="runtime">Runtime to generate the specification</param>
        /// <returns>The updated settings object</returns>
        public static SwaggerGeneratorSettings UseRuntime(this SwaggerGeneratorSettings settings, string runtime)
        {
            settings.Runtime = runtime;
            return settings;
        }
    }
}