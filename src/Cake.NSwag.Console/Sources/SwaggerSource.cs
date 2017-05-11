using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.IO;
using Cake.NSwag.Console.Settings;

namespace Cake.NSwag.Console.Sources
{
    /// <summary>
    ///     Represents a Swagger (Open API) specification to gather metadata from
    /// </summary>
    public class SwaggerSource : GenerationSource
    {

        public SwaggerSource(NSwagConsoleRunner runner, FilePath specificationFilePath, ICakeEnvironment environment)
            : base(runner, specificationFilePath, environment)
        {
        }

        /// <summary>
        ///     Generates a C# API client at the given path with the specified settings
        /// </summary>
        /// <param name="outputFile">File path for the generated client code</param>
        /// <param name="fullClientPath">The fully qualified class name (including namespace) for the client</param>
        /// <param name="configure">Optional settings to further control the code generation process</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromSwaggerSpecification("./swagger.json").ToCSharpClient("./client.cs", "Swagger.Client");]]></code>
        /// </example>
        public SwaggerSource ToCSharpClient(FilePath outputFile, string fullClientPath,
            Action<CSharpGeneratorSettings> configure = null)
        {
            var settings = new CSharpGeneratorSettings();
            configure?.Invoke(settings);
            var @class = fullClientPath.SplitClassPath();
            var args = Runner.GetToolArguments("swagger2csclient")
                .AddSwitch("input", Source.FullPath)
                .AddSwitch("output", outputFile.FullPath)
                .AddSwitch("ClientBaseClass", settings.BaseClass)
                .AddSwitch("GenerateClientInterfaces", settings.GenerateInterfaces.ToString())
                .AddSwitch("ExceptionClass", settings.ExceptionClass)
                .AddSwitch("AdditionalNamespaceUsages", string.Join(",", settings.Namespaces.Select(a => a.Trim())))
                .AddSwitch("ClassName", @class.Value)
                .AddSwitch("Namespace", @class.Key);
            Runner.Run(args);
            return this;
        }

        /// <summary>
        ///     Generates a TypeScript API client at the given path with the specified settings
        /// </summary>
        /// <param name="outputFile">File path for the generated client code</param>
        /// <param name="configure">Optional settings to further control the code generation process</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromSwaggerSpecification("./swagger.json").ToTypeScriptClient("./client.ts");]]></code>
        /// </example>
        public SwaggerSource ToTypeScriptClient(FilePath outputFile,
            Action<TypeScriptGeneratorSettings> configure = null)
        {
            var settings = new TypeScriptGeneratorSettings();
            configure?.Invoke(settings);
            var args = Runner.GetToolArguments("swagger2tsclient")
                .AddSwitch("input", Source.FullPath)
                .AddSwitch("output", outputFile.FullPath)
                .AddSwitch("ClassName", settings.ClassName)
                .AddSwitch("ModuleName", settings.ModuleName)
                .AddSwitch("Template", settings.Template);
            Runner.Run(args);
            return this;
        }

        /// <summary>
        ///     Generates a Web API controller class at the given path with the specified settings
        /// </summary>
        /// <param name="outputFile">File path for the generated client code</param>
        /// <param name="classPath">The fully qualified class name (including namespace) for the client</param>
        /// <param name="configure">Optional settings to further control the code generation process</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[
        /// NSwag
        /// .FromSwaggerSpecification("./swagger.json")
        /// .ToWebApiController("./controller.cs", "Generated.Api.ValuesController")
        /// ]]></code>
        /// </example>
        public SwaggerSource ToWebApiController(FilePath outputFile, string classPath,
            Action<CSharpGeneratorSettings> configure = null)
        {
            var settings = new CSharpGeneratorSettings();
            configure?.Invoke(settings);
            var @class = classPath.SplitClassPath();
            var args = Runner.GetToolArguments("swagger2cscontroller")
                .AddSwitch("input", Source.FullPath)
                .AddSwitch("output", outputFile.FullPath)
                .AddSwitch("ControllerBaseClass", settings.BaseClass)
                .AddSwitch("ClassName", @class.Value)
                .AddSwitch("Namespace", @class.Key)
                .AddSwitch("AdditionalNamespaceUsages", string.Join(",", settings.Namespaces.Select(a => a.Trim())));
            Runner.Run(args);
            return this;
        }
    }
}

