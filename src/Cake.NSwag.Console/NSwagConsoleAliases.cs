using Cake.Core;
using Cake.Core.Annotations;
using System;

namespace Cake.NSwag.Console
{
    [CakeAliasCategory("Swagger")]
    public static class NSwagConsoleAliases
    {
        [CakePropertyAlias]
        [CakeNamespaceImport("Cake.NSwag")]
        [CakeNamespaceImport("Cake.NSwag.Console")]
        public static NSwagConsoleRunner NSwag(this ICakeContext ctx)
        {
            return new NSwagConsoleRunner(ctx.FileSystem, ctx.Environment, ctx.ProcessRunner, ctx.Tools, ctx.Log);
        }
    }
}
