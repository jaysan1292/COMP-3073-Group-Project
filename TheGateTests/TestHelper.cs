using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

using ServiceStack.Text;

namespace TheGateTests {
    /// <summary>
    /// Unit test common methods
    /// </summary>
    public static class TestHelper {
        /// <summary>
        /// Resets the database to ensure a consistent state.
        /// This requires you to have bash and mysql in your PATH environment variable.
        /// </summary>
        public static void ResetDatabase() {
            try {
                var dir = Directory.GetCurrentDirectory();
                var p = new Process {
                    StartInfo = {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        FileName = "bash",
                        Arguments = "-c 'echo thegate | \"{0}\"'".Fmt(dir + @"\..\..\..\TheGateService\Metadata\runscript.sh").Replace('\\', '/'),
                    }
                };
                p.Start();
                p.WaitForExit();
            } catch (Win32Exception) {
                Debug.WriteLine(@"It looks like you don't have bash in your PATH, so we can't reset the database automatically. ¯\_(ツ)_/¯");
            }
        }

        /// <summary>
        /// Returns true if any of <code>args</code> is equal to <code>input</code>.
        /// </summary>
        public static bool Is(this string input, params string[] args) {
            return args.Any(x => string.Equals(input, x));
        }
    }
}
