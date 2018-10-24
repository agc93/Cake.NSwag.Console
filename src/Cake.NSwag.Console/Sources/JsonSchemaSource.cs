using Cake.Core;
using Cake.Core.IO;

namespace Cake.NSwag.Console.Sources
{
    /// <summary>
    ///     Represents a JSON Schema to gather metadata from
    /// </summary>
    public class JsonSchemaSource : GenerationSource
    {
        public JsonSchemaSource(NSwagConsoleRunner runner, FilePath schemaPath, ICakeEnvironment environment) : base(runner, schemaPath, environment)
        {
        }

        /// <summary>
        ///     Generates a TypeScript client from the JSON Schema metadata
        /// </summary>
        /// <param name="outputFile">File path for the generated client code</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromJsonSchema("./schema.json").ToTypeScriptClient("./client.ts");]]></code>
        /// </example>
        public JsonSchemaSource ToTypeScriptClient(FilePath outputFile)
        {
            var args = Runner.GetToolArguments("jsonschema2tsclient");
            args.AddSwitch("input", Source.FullPath)
                .AddSwitch("output", outputFile.FullPath);
            Runner.Run(args);
            return this;
        }

        /// <summary>
        ///     Genreates a C# client from the JSON Schema metadata
        /// </summary>
        /// <param name="outputFile">File path for the generated client code</param>
        /// <returns>The metadata source</returns>
        /// <example>
        ///     <code><![CDATA[NSwag.FromJsonSchema("./schema.json").ToCSharpClient("./client.cs");]]></code>
        /// </example>
        public JsonSchemaSource ToCSharpClient(FilePath outputFile)
        {
            var args = Runner.GetToolArguments("jsonschema2csclient")
                .AddSwitch("input", Source.FullPath)
                .AddSwitch("output", outputFile.FullPath);
            Runner.Run(args);
            return this;
        }
    }
}
