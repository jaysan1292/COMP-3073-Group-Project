// Project: TheGateService
// Filename: CssService.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using TheGateService.Extensions;
using TheGateService.Utilities;

namespace TheGateService.Endpoints {
    [Route("/assets/css/app.css")]
    public class CSS { }

    /// <summary>
    /// Concatenates CSS files together and minifies/compresses them. The minified 
    /// output can be reached at ~/assets/css/app.css. To return non-minified 
    /// output, append '?minify=0' to the end of the request URL. 
    /// (i.e., http://localhost:3073/assets/css/app.css?minify=0)
    /// </summary>
    /// <remarks>
    /// Files will be added to the output in alphabetical order by filename. To
    /// customize the order of the added files, prepend a number to the beginning
    /// of the filename, denoting the order you wish to sort the output.
    /// For example, if the files are file1.css and file2.css, the order they would
    /// be concatenated is file1.css, file2.css. To switch the order, rename them as
    /// follows: 00-file2.css, 01-file1.css. This will result in file2 appearing before
    /// file1 in the output.
    /// </remarks>
    public class CssService : Service {
        // The base path where all of the CSS files are stored
        private const string CssBasePath = "~/assets/css/app.css.d";

        // Modification times for all of the files in the above directory
        private static readonly Dictionary<string, DateTime> ModificationTimes = new Dictionary<string, DateTime>();

        public object Get(CSS unused) {
            Response.AddHeader("Content-Type", "text/css");
            // Check the query string to see if we should return the minified 
            // version of the CSS; default value will be 'true' if it isn't there
            var minify = int.Parse(Request.QueryString.Get("minify") ?? "1") == 1;

            var shouldRegenerate = false;

            // Check modification times of each file, and if one has changed,
            // we should regenerate the CSS to send
            var files = FileHelper.GetDirectory(CssBasePath).GetFiles("*.css");
            foreach (var file in files) {
                DateTime mtime;
                ModificationTimes.TryGetValue(file.Name, out mtime);
                // If mtime is not set, or the current file is newer than what we have, regenerate the CSS
                if ((mtime == default(DateTime)) || (mtime < file.LastWriteTime)) {
                    ModificationTimes[file.Name] = file.LastWriteTime;
                    shouldRegenerate = true;
                    break;
                }
            }

            // If any files have changed, regenerate the css and its minified version
            if (shouldRegenerate) {
                Cache.Set("css", GetCss(files, false));
                Cache.Set("css-mini", GetCss(files, true));
            }

            return Cache.Get<string>("css" + (minify ? "-mini" : ""));
        }

        private string GetCss(IEnumerable<FileInfo> files, bool minify) {
            Global.Log.Debug("Rebuilding{0}CSS.".F(minify ? " minified " : " "));
            var css = new StringBuilder();

            foreach (var file in files) {
                using (var stream = file.Open(FileMode.Open)) {
                    using (var reader = new StreamReader(stream)) {
                        // Append a little comment with the filename separating each file for clarity
                        if (!minify) css.Append("\n/********\n * {0}  \n */\n\n".F(file.Name));
                        css.Append(reader.ReadToEnd());
                    }
                }
            }
            return minify ? Compress(css.ToString()) : css.ToString();
        }

        private static string Compress(string css) {
            // Remove css comments /* i.e., everything in a block such as this */
            css = Regex.Replace(css, @"/\*(.*?)\*/", "", RegexOptions.Singleline);

            // Remove all extra whitespace
            css = Regex.Replace(css, @"\s+", " ");
            return css.Trim();
        }
    }
}
