namespace Cake.NSwag.Console.Settings
{
    /// <summary>
    ///     Settings to further control the generation of TypeScript code
    /// </summary>
    public class TypeScriptGeneratorSettings : GeneratorSettings
    {
        internal TypeScriptGeneratorSettings()
        {
        }

        /// <summary>
        ///     The class name to use for generated client code
        /// </summary>
        public string ClassName { get; set; } = "Client";

        /// <summary>
        ///     The module name to use when generating a module
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///     The template name to use when generating the client.
        /// </summary>
        public string Template { get; set; }
    }
}