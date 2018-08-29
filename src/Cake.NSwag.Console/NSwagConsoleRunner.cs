using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.NSwag.Console.Sources;

namespace Cake.NSwag.Console
{
    public class NSwagConsoleRunner : Tool<NSwagConsoleSettings>
    {
        internal NSwagConsoleRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log) : base(fileSystem, environment, processRunner, tools)
        {
            FileSystem = fileSystem;
            Log = log;
            Environment = environment;
        }

        private ICakeEnvironment Environment { get; set; }

        private ICakeLog Log { get; set; }

        private IFileSystem FileSystem { get; set; }

        public void ExecuteDocuments()
        {
            var args = GetToolArguments();
            args.Append("run");
            Run(new NSwagConsoleSettings(), args);
        }

        public void ExecuteDocument(FilePath configurationDocument)
        {
            var args = GetToolArguments();
            args.Append("run");
            args.Append(configurationDocument.FullPath);
            Run(new NSwagConsoleSettings(), args);
        }

        /// <summary>
        ///     Parses a plain .NET assembly for metadata
        /// </summary>
        /// <param name="assemblyPath">Path to the assembly to load</param>
        /// <returns>A metadata source for the given assembly</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <c>assemblyPath</c> is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when the assembly could not be found on the file system</exception>
        public AssemblySource FromAssembly(FilePath assemblyPath)
        {
            return CreateAssemblySource(assemblyPath, false);
        }

        /// <summary>
        ///     Parses an ASP.NET Web API assembly for API metadata
        /// </summary>
        /// <param name="assemblyPath">Path to the API assembly to load</param>
        /// <returns>A metadata source for the given assembly</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <c>assemblyPath</c> is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when the assembly could not be found on the file system</exception>
        public AssemblySource FromWebApiAssembly(FilePath assemblyPath)
        {
            return CreateAssemblySource(assemblyPath, true);
        }

        /// <summary>
        ///     Parses a Swagger (Open API) specification for API metadata
        /// </summary>
        /// <param name="specificationFilePath">Path to the JSON definition file</param>
        /// <returns>A metadata source for the given API spec</returns>
        /// <exception cref="ArgumentNullException">Thrown if the definition file is not provided</exception>
        /// <exception cref="FileNotFoundException">Thrown if the definition file is not found on the file system</exception>
        public SwaggerSource FromSwaggerSpecification(FilePath specificationFilePath)
        {
            if (specificationFilePath == null) throw new ArgumentNullException(nameof(specificationFilePath));
            if (!FileSystem.Exist(specificationFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{specificationFilePath}", nameof(specificationFilePath));
            }

            return new SwaggerSource(this, specificationFilePath, Environment);
        }

        /// <summary>
        ///     Parses a JSON Schema file for API metadata
        /// </summary>
        /// <param name="definitionFilePath">Path to the JSON Schema file</param>
        /// <returns>A metadata source for the given schema</returns>
        /// <exception cref="ArgumentNullException">Thrown if <c>specificationFilePath</c> is null</exception>
        /// <exception cref="FileNotFoundException">Thrown if the schema file is not found on the file system</exception>
        public JsonSchemaSource FromJsonSchema(FilePath definitionFilePath)
        {
            if (definitionFilePath == null) throw new ArgumentNullException(nameof(definitionFilePath));
            if (!FileSystem.Exist(definitionFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{definitionFilePath}", nameof(definitionFilePath));
            }
            return new JsonSchemaSource(this, definitionFilePath, Environment);
        }

        private AssemblySource CreateAssemblySource(FilePath assemblyPath, bool useWebApi)
        {
            if (assemblyPath == null) throw new ArgumentNullException(nameof(assemblyPath));
            if (!FileSystem.Exist(assemblyPath))
            {
                throw new FileNotFoundException($"Could not find file '{assemblyPath}", nameof(assemblyPath));
            }
            // TODO: Maybe remove this check in future? .dll and .exe is hardly 100% reliable anymore.
            if (assemblyPath.HasExtension && !assemblyPath.GetExtension().Contains("dll") && !assemblyPath.GetExtension().Contains("exe"))
            {
                Log.Warning($"The '{assemblyPath}' does not appear to be an assembly!");
            }
            return new AssemblySource(this, assemblyPath, Environment, useWebApi);
        }

        internal ProcessArgumentBuilder GetToolArguments()
        {
            var args = new ProcessArgumentBuilder();
            return args;
        }

        internal ProcessArgumentBuilder GetToolArguments(string command)
        {
            var args = GetToolArguments();
            args.Append(command);
            return args;
        }

        internal void Run(ProcessArgumentBuilder args)
        {
            Log.Verbose(args.ToString());
            Run(new NSwagConsoleSettings(), args);
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "nswag.exe";
            yield return "nswag";
            yield return "nswag.cmd";
            // need to look at how to use the `dotnet` versions
            //yield return "dotnet dotnet-nswag.dll" 
        }

        protected override string GetToolName() => "NSwag Console";
    }
}
