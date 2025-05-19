// Copyright SwifterTheDragon, and the SwifterTheDragon.NuGetGitTasks contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Diagnostics;

namespace SwifterTheDragon.NuGetGitTasks.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
    /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Description/*'/>
    [DebuggerDisplay(
        value: "{DebuggerDisplay,nq}")]
    public sealed class NuGetGitBranchTask : Task
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="k_DefaultUnknownHeadLabel"]/*'/>
        private const string k_DefaultUnknownHeadLabel = "-UNKNOWN-HEAD-";
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="k_DefaultInvalidHeadLabel"]/*'/>
        private const string k_DefaultInvalidHeadLabel = "-INVALID-HEAD-";
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="k_DefaultDetachedHeadLabel"]/*'/>
        private const string k_DefaultDetachedHeadLabel = "-DETACHED-HEAD-";
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="i_unknownHeadLabel"]/*'/>
        private string i_unknownHeadLabel;
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Property[@name="UnknownHeadLabel"]/*'/>
        [Required]
        public string UnknownHeadLabel
        {
            get
            {
                return i_unknownHeadLabel;
            }
            set
            {
                i_unknownHeadLabel = value;
            }
        }
        private string i_invalidHeadLabel;
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Property[@name="InvalidHeadLabel"]/*'/>
        [Required]
        public string InvalidHeadLabel
        {
            get
            {
                return i_invalidHeadLabel;
            }
            set
            {
                i_invalidHeadLabel = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="i_detachedHeadLabel"]/*'/>
        private string i_detachedHeadLabel;
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Property[@name="DetachedHeadLabel"]/*'/>
        [Required]
        public string DetachedHeadLabel
        {
            get
            {
                return i_detachedHeadLabel;
            }
            set
            {
                i_detachedHeadLabel = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Field[@name="i_parsedGitBranchName"]/*'/>
        private string i_parsedGitBranchName;
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Property[@name="ParsedGitBranchName"]/*'/>
        [Output]
        public string ParsedGitBranchName
        {
            get
            {
                return i_parsedGitBranchName;
            }
            private set
            {
                i_parsedGitBranchName = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Property[@name="DebuggerDisplay"]/*'/>
        [DebuggerBrowsable(
            state: DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get
            {
                return ToString();
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="Execute"]/*'/>
        public override bool Execute()
        {
            SetGitBranchName();
            return !Log.HasLoggedErrors;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="ToString"]/*'/>
        public override string ToString()
        {
            return base.ToString()
                + "U:"
                + i_unknownHeadLabel
                + ",I:"
                + i_invalidHeadLabel
                + ",D:"
                + i_detachedHeadLabel
                + ",O:"
                + i_parsedGitBranchName;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="SetGitBranchName"]/*'/>
        private void SetGitBranchName()
        {
            ParsedGitBranchName = FetchGitBranchName();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="FetchGitBranchName"]/*'/>
        private string FetchGitBranchName()
        {
            string parsedCurrentGitBranchName = RunCommandLineCommand(
                commandToRun: "git branch --show-current");
            if (!string.IsNullOrWhiteSpace(
                value: parsedCurrentGitBranchName))
            {
                return parsedCurrentGitBranchName;
            }
            string parsedGitDescribeFallback = RunCommandLineCommand(
                commandToRun: "git describe --always");
            if (!string.IsNullOrWhiteSpace(
                value: parsedGitDescribeFallback))
            {
                return GetCurrentDetachedHeadLabel();
            }
            string parsedGitVersion = RunCommandLineCommand(
                commandToRun: "git --version");
            if (!string.IsNullOrWhiteSpace(
                value: parsedGitVersion))
            {
                return GetCurrentInvalidHeadLabel();
            }
            return GetCurrentUnknownHeadLabel();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="RunCommandLineCommand(System.String)"]/*'/>
        private static string RunCommandLineCommand(
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
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="GetCurrentDetachedHeadLabel"]/*'/>
        private string GetCurrentDetachedHeadLabel()
        {
            if (!string.IsNullOrWhiteSpace(
                value: DetachedHeadLabel))
            {
                return k_DefaultDetachedHeadLabel;
            }
            return DetachedHeadLabel;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="GetCurrentInvalidHeadLabel"]/*'/>
        private string GetCurrentInvalidHeadLabel()
        {
            if (!string.IsNullOrWhiteSpace(
                value: InvalidHeadLabel))
            {
                return k_DefaultInvalidHeadLabel;
            }
            return InvalidHeadLabel;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.NuGetGitTasks.xml'
        /// path='Assembly[@name="SwifterTheDragon.NuGetGitTasks"]/Namespace[@name="Core"]/Type[@name="NuGetGitBranchTask"]/Method[@name="GetCurrentUnknownHeadLabel"]/*'/>
        private string GetCurrentUnknownHeadLabel()
        {
            if (!string.IsNullOrWhiteSpace(
                value: UnknownHeadLabel))
            {
                return k_DefaultUnknownHeadLabel;
            }
            return UnknownHeadLabel;
        }
        #endregion Methods
    }
}
