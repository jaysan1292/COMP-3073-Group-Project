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
using System.Web;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using TheGateService.Extensions;

namespace TheGateService.Endpoints {
    [Route("/assets/css/minified.css")]
    public class Css { }

    /// <summary>
    /// Concatenates CSS files together and minifies/compresses them. The minified 
    /// output can be reached at ~/assets/css/minified.css. To return non-minified 
    /// output, append '?minify=0' to the end of the request URL. 
    /// (i.e., http://localhost:3073/assets/css/minified.css?minify=0)
    /// </summary>
    public class CssService : Service {
        // The base path where all of the CSS files are stored
        private const string CssBasePath = "~/assets/css/";

        // The list of CSS files to concatenate together in the response
        private static readonly List<string> CssFiles = new List<string> {
            "bootstrap.css",
            "application.css",
        };

        // Modification times for all of the above files
        private static readonly Dictionary<string, DateTime> ModificationTimes = new Dictionary<string, DateTime>(CssFiles.Count);

        public object Get(Css unused) {
            Response.AddHeader("Content-Type", "text/css");
            // Check the query string to see if we should return the minified 
            // version of the CSS; default value will be 'true' if it isn't there
            var minify = int.Parse(Request.QueryString.Get("minify") ?? "1") == 1;

            var shouldRegenerate = false;
            // Check modification times of each file, and if one has changed,
            // we should regenerate the CSS to send
            foreach (var file in CssFiles.Select(f => new FileInfo(HttpContext.Current.Server.MapPath(CssBasePath + f)))) {
                DateTime mtime;
                ModificationTimes.TryGetValue(file.Name, out mtime);
                // If mtime is not set, or the current file is newer than what we have
                if ((mtime == default(DateTime)) || (mtime < file.LastWriteTime)) {
                    ModificationTimes[file.Name] = file.LastWriteTime;
                    shouldRegenerate = true;
                    break;
                }
            }

            if (shouldRegenerate) {
                Cache.Set("css", GetCss(false));
                Cache.Set("css-mini", GetCss(true));
            }

            return Cache.Get<string>("css" + (minify ? "-mini" : ""));
        }

        private string GetCss(bool minify) {
            Global.Log.Debug("Rebuilding{0}CSS.".F(minify ? " minified " : " "));
            var css = new StringBuilder();

            // All of these files will be included in the css output, in this order
            foreach (var file in CssFiles.Select(f => new FileInfo(HttpContext.Current.Server.MapPath(CssBasePath + f)))) {
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
