# Settings

This Cake addin includes a simplified object model for settings compared to the full NSwag settings model. This is in order to maintain the simplicity of your build scripts and expose the most commonly used settings directly, without requiring complex configuration.

What does this look like in practice?

```csharp
NSwag.FromWebApiAssembly("./web.assembly.dll").ToSwaggerSpecification("./swagger.json");
// or using the basic settings
NSwag.FromWebApiAssembly("./web.assembly.dll").ToSwaggerSpecification("./swagger.json", s => s.EnableInterfaces());
```

## Fluent interface

The majority of the settings used by `Cake.NSwag.Console` are available through a simple fluent API.

```csharp
NSwag.FromWebApiAssembly("./apicontroller.dll")
    .ToSwaggerSpecification("./api.json", s =>
        s.SearchAssemblies("./reference.dll")
            .UseBasePath("api")
            .UseStringEnums()
            .WithTitle("Sample API"));
```

## Object interface

But you can also use the "conventional" object format.

```csharp
NSwag.FromWebApiAssembly("./apicontroller.dll")
    .ToSwaggerSpecification("./api.json", new SwaggerGeneratorSettings
    {
        AssemblyPaths = new List<FilePath> { "./reference.dll" },
        BasePath = "api",
        EnumAsString = true,
        ApiTitle = "Sample API"
    });
```

This represents the same command as the fluent example above.