namespace Cake.NSwag.Console.Settings
{
    internal static class SettingsFactory
    {
        private static SettingsContainer Container { get; } = new SettingsContainer();

        internal static SwaggerToCSharpClientGeneratorSettings GetSwaggerToCSharpSettings()
        {
            return Container.SwaggerToCSharpClientGeneratorSettings;
        }

        internal static SwaggerToTypeScriptClientGeneratorSettings GetSwaggerToTypeScriptSettings()
        {
            return Container.SwaggerToTypeScriptClientGeneratorSettings;
        }

        internal static SwaggerToCSharpWebApiControllerGeneratorSettings GetSwaggerToControllerSettings()
        {
            return Container.SwaggerToCSharpWebApiControllerGeneratorSettings;
        }

        internal static WebApiAssemblyToSwaggerGeneratorSettings GetWebApiToSwaggerSettings()
        {
            return Container.WebApiAssemblyToSwaggerGeneratorSettings;
        }

        internal static AssemblyTypeToSwaggerGeneratorSettings GetAssemblyToSwaggerSettings()
        {
            return Container.AssemblyTypeToSwaggerGeneratorSettings;
        }
    }
}