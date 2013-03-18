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

using TheGateService.Extensions;

using dotless.Core;

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
    public class CssService : MinifierServiceBase {
        public CssService()
            : base("~/assets/css/app.css.d", GetCss, ".css", ".less") { }

        public object Get(CSS unused) {
            Response.AddHeader("Content-Type", "text/css");
            // Check the query string to see if we should return the minified 
            // version of the CSS; default value will be 'true' if it isn't there
            var minify = int.Parse(Request.QueryString.Get("minify") ?? "1") == 1;

            return GenerateFile("css", minify);
        }

        private static void GetCss(IEnumerable<FileInfo> files, out string css, out string cssmini) {
            Global.Log.Debug("Rebuilding CSS.");
            var output = new StringBuilder("/* Generated on {0} */\n".F(DateTime.Now));

            foreach (var file in files) {
                using (var stream = file.Open(FileMode.Open)) {
                    using (var reader = new StreamReader(stream)) {
                        // Append a little comment with the filename separating each file for clarity
                        output.Append("\n/********\n * {0}  \n */\n\n".F(file.Name));
                        switch (file.Extension) {
                            case ".css":
                                output.Append(reader.ReadToEnd());
                                break;
                            case ".less":
                                output.Append(Less.Parse(reader.ReadToEnd()));
                                break;
                        }
                    }
                }
            }

            css = output.ToString();
            cssmini = Compress(output.ToString());
        }

        private static string Compress(string css) {
            //TODO: YUI Compressor minifier
            // Remove css comments /* i.e., everything in a block such as this */
            css = Regex.Replace(css, @"/\*(.*?)\*/", "", RegexOptions.Singleline);

            // Remove all extra whitespace
            css = Regex.Replace(css, @"\s+", " ");
            return css.Trim();
        }
    }
}
