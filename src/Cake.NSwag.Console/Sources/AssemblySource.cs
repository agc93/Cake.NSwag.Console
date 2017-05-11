using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.IO;
using Cake.NSwag.Console.Settings;

namespace Cake.NSwag.Console.Sources
{
    public class AssemblySource : GenerationSource
    {
        public AssemblySource(NSwagConsoleRunner runner, FilePath assemblyPath, ICakeEnvironment environment, bool useWebApi)
            : base(runner, assemblyPath, environment)
        {
            Mode = useWebApi ? AssemblyMode.WebApi : AssemblyMode.Normal;
        }

        private AssemblyMode Mode { get; set; }

        /// <summary>
        ///     Generates a Swagger (Open API) specification at the given path using the specified settings
        /// </summary>
        /// <param name="outputFile">File path for the generated API specification</param>
        /// <param name="settings">Settings to further control the spec generation process</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromAssembly("./assembly.dll").ToSwaggerSpecification("./swagger.json");]]></code>
        ///     <code><![CDATA[NSwag.FromWebApiAssembly("./apicontroller.dll").ToSwaggerSpecification("./swagger.json");]]></code>
        /// </example>
        public AssemblySource ToSwaggerSpecification(FilePath outputFile, SwaggerGeneratorSettings settings)
        {
            settings = settings ?? new SwaggerGeneratorSettings();
            if (Mode == AssemblyMode.Normal)
            {
                GenerateTypeSwagger(outputFile, settings);
            }
            else
            {
                GenerateWebApiSwagger(outputFile, settings);
            }
            return this;
        }

        /// <summary>
        ///     Generates a Swagger (Open API) specification at the given path using the specified settings
        /// </summary>
        /// <param name="outputFile">File path for the generated API specification</param>
        /// <param name="configure">Optional settings to further control the specification</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromWebApiAssembly("./apicontroller.dll").ToSwaggerSpecification("./api.json");]]></code>
        /// </example>
        public AssemblySource ToSwaggerSpecification(FilePath outputFile, Action<SwaggerGeneratorSettings> configure = null)
        {
            var settings = new SwaggerGeneratorSettings();
            configure?.Invoke(settings);
            ToSwaggerSpecification(outputFile, settings);
            return this;
        }

        private void GenerateTypeSwagger(FilePath outputFile, SwaggerGeneratorSettings settings)
        {
            var args = Runner.GetToolArguments();
            args.Append("types2swagger");
            args.Append($"/assembly:{Source.GetRelativePath(Environment.WorkingDirectory)}");
            args.Append($"/output:{outputFile.FullPath}");
            args.Append($"/defaultenumhandling:{(settings.EnumAsString ? "String" : "Integer")}");
            args.Append($"/defaultpropertynamehandling:{(settings.CamelCaseProperties ? "CamelCase" : "Default")}");
            args.AddSwitch("ReferencePaths", string.Join(",", settings.AssemblyPaths.Select(a => a.FullPath)));
            Runner.Run(args);
        }

        private void GenerateWebApiSwagger(FilePath outputFile, SwaggerGeneratorSettings settings)
        {
            var args = Runner.GetToolArguments();
            args.Append("webapi2swagger");
            args.AddSwitch("assembly", Source.GetRelativePath(Environment.WorkingDirectory).FullPath)
                .AddSwitch("output", outputFile.FullPath)
                .AddSwitch("DefaultEnumHandling", settings.EnumAsString ? "String" : "Integer")
                .AddSwitch("ReferencePaths", string.Join(",", settings.AssemblyPaths.Select(a => a.FullPath)))
                .AddSwitch("DefaultPropertyNameHandling", settings.CamelCaseProperties ? "CamelCase" : "Default")
                .AddSwitch("ServiceBasePath", settings.BasePath)
                .AddSwitch("InfoTitle", settings.ApiTitle)
                .AddSwitch("InfoDescription", settings.ApiDescription)
                .AddSwitch("DefaultUrlTemplate", settings.DefaultUrlTemplate);
            Runner.Run(args);
        }
    }
}
