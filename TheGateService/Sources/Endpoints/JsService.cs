using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ServiceStack.ServiceHost;

using TheGateService.Extensions;

using Yahoo.Yui.Compressor;

namespace TheGateService.Endpoints {
    [Route("/assets/js/app.js")]
    public class Javascript { }

    /// <summary>
    /// Concatenates Javascript files together and minifies/compresses them. The minified 
    /// output can be reached at ~/assets/js/app.js. To return non-minified 
    /// output, append '?minify=0' to the end of the request URL. 
    /// (i.e., http://localhost:3073/assets/js/app.js?minify=0)
    /// </summary>
    /// <remarks>
    /// Files will be added to the output in alphabetical order by filename. To
    /// customize the order of the added files, prepend a number to the beginning
    /// of the filename, denoting the order you wish to sort the output.
    /// For example, if the files are file1.js and file2.js, the order they would
    /// be concatenated is file1.js, file2.js. To switch the order, rename them as
    /// follows: 00-file2.js, 01-file1.js. This will result in file2 appearing before
    /// file1 in the output.
    /// </remarks>
    public class JsService : MinifierServiceBase {
        public JsService()
            : base("~/assets/js/app.js.d", GetJs, ".js") { }

        public object Get(Javascript unused) {
            Response.AddHeader("Content-Type", "text/javascript");

            // Check the query string to see if we should return the minified version 
            // of the javascript; default value will be 'true' if it isn't there
            var minify = int.Parse(Request.QueryString.Get("minify") ?? "1") == 1;
            return GenerateFile("js", minify);
        }

        private static void GetJs(IEnumerable<FileInfo> files, out string js, out string jsmini) {
            Global.Log.Debug("Rebuilding Javascript.");
            var output = new StringBuilder("/* Generated on {0} */\n".F(DateTime.Now));

            foreach (var file in files) {
                using (var stream = file.Open(FileMode.Open)) {
                    using (var reader = new StreamReader(stream)) {
                        output.Append("\n/********\n * {0}  \n */\n\n".F(file.Name));
                        output.Append(reader.ReadToEnd());
                    }
                }
            }

            js = output.ToString();
            jsmini = Compress(output.ToString());
        }

        private static string Compress(string js) {
            return new JavaScriptCompressor {
                CompressionType = CompressionType.Standard,
                DisableOptimizations = true,
                Encoding = Encoding.UTF8,
                ObfuscateJavascript = true,
            }.Compress(js);
        }
    }
}
