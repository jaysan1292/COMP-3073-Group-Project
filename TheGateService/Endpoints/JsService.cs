// Project: TheGateService
// Filename: JsService.cs
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

/*
 * Everything in this file is pretty much a direct copy of
 * CssService.cs, except this file deals with Javascript.
 * 
 * Normally, I would abstract this functionality into an
 * abstract class to avoid duplicating too much code, but 
 * due to the nature of ServiceStack's API (i.e., method 
 * signatures determine the type of request), it doesn't
 * appear to be possible to abstract things in that way.
 * 
 * For more information, see the docs for CssService.cs.
 * 
 * - Jason
 */

namespace TheGateService.Endpoints {
    [Route("/assets/js/app.js")]
    public class Javascript { }

    public class JsService : Service {
        private const string JsBasePath = "~/assets/js/app.js.d";

        private static readonly Dictionary<string, DateTime> ModificationTimes = new Dictionary<string, DateTime>();

        public object Get(Javascript unused) {
            Response.AddHeader("Content-Type", "text/javascript");

            var minify = int.Parse(Request.QueryString.Get("minify") ?? "1") == 1;

            var shouldRegenerate = false;

            var files = FileHelper.GetDirectory(JsBasePath).GetFiles("*.js");
            foreach (var file in files) {
                DateTime mtime;
                ModificationTimes.TryGetValue(file.Name, out mtime);

                if ((mtime == default(DateTime)) || (mtime < file.LastWriteTime)) {
                    ModificationTimes[file.Name] = file.LastWriteTime;
                    shouldRegenerate = true;
                    break;
                }
            }

            if (shouldRegenerate) {
                Cache.Set("js", GetJs(files, false));
                Cache.Set("js-mini", GetJs(files, true));
            }

            return Cache.Get<string>("js" + (minify ? "-mini" : ""));
        }

        private string GetJs(IEnumerable<FileInfo> files, bool minify) {
            Global.Log.Debug("Rebuilding{0}Javascript.".F(minify ? " minified " : " "));
            var js = new StringBuilder();

            foreach (var file in files) {
                using (var stream = file.Open(FileMode.Open)) {
                    using (var reader = new StreamReader(stream)) {
                        if (!minify) js.Append("\n/********\n * {0}  \n */\n\n".F(file.Name));
                        js.Append(reader.ReadToEnd());
                    }
                }
            }

            return minify ? Compress(js.ToString()) : js.ToString();
        }

        // This method is the only one that really differs
        // CssService.Compress(), as this one is tailored 
        // towards Javascript (obviously).
        private static string Compress(string js) {
            // TODO: Proper Javascript minification (i.e., removing all comments and unnecessary whitespace)

            // Remove /* block comments */
            js = Regex.Replace(js, @"/\*(.*?)\*/", "", RegexOptions.Singleline);

            // Remove line comments
            js = Regex.Replace(js, @"(?<="".*"".*)//.*$|(?<!"".*)//.*$", "", RegexOptions.Multiline);

            // Remove blank lines
            js = Regex.Replace(js, @"^\s*$[\r\n]*", "", RegexOptions.Multiline);
            return js.Trim();
        }
    }
}
