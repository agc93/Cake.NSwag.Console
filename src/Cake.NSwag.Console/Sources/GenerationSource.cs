using System;
using System.Collections.Generic;
using System.Text;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.NSwag.Console.Sources
{
    /// <summary>
    ///     Base class for metadata sources
    /// </summary>
    public abstract class GenerationSource
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenerationSource" /> class.
        /// </summary>
        /// <param name="runner">The console runner.</param>
        /// <param name="sourceFilePath">Source file for the generation process</param>
        /// <param name="environment">The Cake evironment</param>
        protected GenerationSource(NSwagConsoleRunner runner, FilePath sourceFilePath, ICakeEnvironment environment)
        {
            Runner = runner;
            Environment = environment;
            Source = sourceFilePath;
        }

        protected NSwagConsoleRunner Runner { get; set; }

        /// <summary>
        ///     The Cake environment
        /// </summary>
        protected ICakeEnvironment Environment { get; set; }

        /// <summary>
        ///     Source file for API metadata.
        /// </summary>
        protected FilePath Source { get; set; }
    }
}
