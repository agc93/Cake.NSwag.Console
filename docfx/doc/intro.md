# Getting Started

This addin (unlike the normal `Cake.NSwag` package) does not include NSwag in the addin, but acts as a wrapper over the NSwag command line interface (CLI). This means that while you do have to ensure NSwag is available in your environment, you can use this addin when running under .NET Core (unlike `Cake.NSwag`).

> [!NOTE]
> This addin shares 100% API compatibility with `Cake.NSwag` so you should be able to simply swap the two in and out with no script changes required.

## Installing NSwag's CLI

There are a variety of ways to install NSwag's CLI, including the `NSwag.ConsoleCore` NuGet package or the `nswag` package for `npm`.

At this time, it's recommended to use the npm package as it natively supports both the .NET Framework and .NET Core versions of the NSwag CLI.

> [!WARNING]
> For this addin to work, you need to make sure one of `nswag.exe`, `nswag`, or `nswag.cmd` is available in the PATH or the `tools/` folder.

## Including the addin

At the top of your script, just add the following to install the addin:

```
#addin nuget:?package=Cake.NSwag.Console
```

## Usage

The addin exposes a single property alias `NSwag` with all of the NSwag functionality available as methods.

The general process of using the alias is to get a source (a `GenerationSource` implementation) and then output it to any number of generated targets. So, generating a Swagger spec from a Web API assembly is simply:

```csharp
NSwag.FromWebApiAssembly("./web.assembly.dll").ToSwaggerSpecification("./swagger.json");
```

Or creating a TypeScript client from a JSON Schema:

```csharp
NSwag.FromJsonSchema("./schema.json").ToTypeScriptClient("./client.ts");
```

The supported sources are:

- .NET assembly (`FromAssembly()`)
- ASP.NET Web API assembly (`FromWebApiAssembly()`)
- JSON Schema (`FromJsonSchema()`)
- Swagger specification (`FromSwaggerSpecification()`)

Each source will have its own possible destinations that are covered in the documentation for those source types (see the *Reference* tab above).

## Settings

Some targets allow for including a settings action to fine-tune the code generation process. This is highly specific to each generation target and you will need to check the documentation to confirm the available options. For more detail check the [Settings](settings.md) page.