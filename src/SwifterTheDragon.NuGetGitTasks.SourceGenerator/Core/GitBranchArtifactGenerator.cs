// Copyright SwifterTheDragon, and the SwifterTheDragon.NuGetGitTasks contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SwifterTheDragon.NuGetGitTasks.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Description/*'/>
    [Generator]
    // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    internal sealed class GitBranchArtifactGenerator : IIncrementalGenerator
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Property[@name="HintName"]/*'/>
        private static string HintName
        {
            get
            {
                return "GitBranchArtifact.generated.cs";
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Method[@name="Initialize"]/*'/>
        public void Initialize(
            IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<string> pipeline = context.CompilationProvider.Select(
                selector: Transform);
            context.RegisterSourceOutput(
                source: pipeline,
                action: RegisterOutput);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Method[@name="Transform(Microsoft.CodeAnalysis.Compilation,System.Threading.CancellationToken)"]/*'/>
        private string Transform(
            Compilation compilation,
            CancellationToken cancellationToken)
        {
            string parsedGitBranchName = ExecuteCommandLineCommand(
                commandToRun: "git branch --show-current");
            if (!string.IsNullOrWhiteSpace(
                value: parsedGitBranchName))
            {
                return parsedGitBranchName;
            }
            string parsedGitDescribe = ExecuteCommandLineCommand(
                commandToRun: "git describe --always");
            if (!string.IsNullOrWhiteSpace(
                value: parsedGitDescribe))
            {
                return "-DETACHED-HEAD-";
            }
            string parsedGitVersion = ExecuteCommandLineCommand(
                commandToRun: "git --version");
            if (!string.IsNullOrWhiteSpace(
                value: parsedGitVersion))
            {
                return "-INVALID-HEAD-";
            }
            return "-UNKNOWN-HEAD-";
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Method[@name="ExecuteCommandLineCommand(System.String)"]/*'/>
        private static string ExecuteCommandLineCommand(
            string commandToRun)
        {
            if (string.IsNullOrWhiteSpace(
                value: commandToRun))
            {
                return string.Empty;
            }
            string output = string.Empty;
            using (var cmdProcess = new Process())
            {
                cmdProcess.StartInfo = new ProcessStartInfo(
                    fileName: "cmd.exe",
                    arguments: "/c "
                        + commandToRun)
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };
                _ = cmdProcess.Start();
#pragma warning disable MA0045 // Do not use blocking calls in a sync method (need to make calling method async)
                output = cmdProcess.StandardOutput.ReadToEnd().TrimEnd();
#pragma warning restore MA0045 // Do not use blocking calls in a sync method (need to make calling method async)
                cmdProcess.WaitForExit();
            }
            return output;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitBranchArtifactGenerator"]/Method[@name="RegisterOutput(Microsoft.CodeAnalysis.SourceProductionContext,System.String)"]/*'/>
        private void RegisterOutput(
            SourceProductionContext context,
            string model)
        {
            StringBuilder sourceTextBuilder = new StringBuilder()
                .Append(
                    value: "// ")
                .AppendLine(
                    value: model);
            var sourceText = SourceText.From(
                text: sourceTextBuilder.ToString(),
                encoding: Encoding.UTF8);
            context.AddSource(
                hintName: HintName,
                sourceText: sourceText);
        }
        #endregion Methods
    }
}
