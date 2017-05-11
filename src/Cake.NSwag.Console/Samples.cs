using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.NSwag.Console.Settings;
using Cake.Core.IO;

namespace Cake.NSwag.Console
{
    class Samples
    {
        void Run(ICakeContext ctx)
        {
            ctx.NSwag().FromAssembly("./assembly.dll").ToSwaggerSpecification("./swagger.json");
            ctx.NSwag().FromWebApiAssembly("./apicontroller.dll").ToSwaggerSpecification("./api.json");
            ctx.NSwag().FromWebApiAssembly("./apicontroller.dll")
                .ToSwaggerSpecification("./api.json", s =>
                    s.SearchAssemblies("./reference.dll")
                        .UseBasePath("api")
                        .UseStringEnums()
                        .WithTitle("Sample API"));
            ctx.NSwag().FromWebApiAssembly("./apicontroller.dll")
                .ToSwaggerSpecification("./api.json", new SwaggerGeneratorSettings
                {
                    AssemblyPaths = new List<FilePath> { "./reference.dll" },
                    BasePath = "api",
                    EnumAsString = true,
                    ApiTitle = "Sample API"
                });
            ctx.NSwag().FromJsonSchema("./schema.json").ToCSharpClient("./client.cs");
            ctx.NSwag().FromJsonSchema("./schema.json").ToTypeScriptClient("./client.ts");
            ctx.NSwag().FromSwaggerSpecification("./swagger.json").ToCSharpClient("./client.cs", "Swagger.Client");
            ctx.NSwag().FromSwaggerSpecification("./swagger.json").ToTypeScriptClient("./client.ts");
            ctx.NSwag()
                .FromSwaggerSpecification("./swagger.json")
                .ToTypeScriptClient("./client.ts");
            ctx.NSwag()
                .FromSwaggerSpecification("./swagger.json")
                .ToTypeScriptClient("./client.ts", s =>
                    s.WithClassName("ApiClient")
                        .WithModuleName("SwaggerApi")
                );
            ctx.NSwag()
                .FromSwaggerSpecification("./swagger.json")
                .ToWebApiController("./controller.cs", "Generated.Api.ValuesController");
        }
    }
}
