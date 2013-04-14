using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TheGateService.Extensions;
using TheGateService.Utilities;

namespace TheGateService.Endpoints {
    public abstract class MinifierServiceBase : GateServiceBase {
        // The base path where all of the files to be combined are stored
        private readonly string _basePath;

        // The file extensions to search _basePath for
        private readonly string[] _fileExtensions;

        // The function to use to generate the output
        private readonly GenerateFileDelegate<IEnumerable<FileInfo>, string> _generate;

        // Modification times for all of the files in _basePath
        private readonly Dictionary<string, DateTime> _mtimes = new Dictionary<string, DateTime>();

        protected MinifierServiceBase(string basePath,
                                      GenerateFileDelegate<IEnumerable<FileInfo>, string> action,
                                      params string[] extensions) {
            if (extensions == null) throw new ArgumentNullException("extensions");
            _basePath = basePath;
            _generate = action;
            _fileExtensions = extensions;
        }

        protected string GenerateFile(string cachePrefix, bool minify) {
            var shouldRegenerate = false;

            // Check modification times of each file, and if 
            // one has changed, we should regenerate the output
            var files = FileHelper.GetDirectory(_basePath).GetFiles(_fileExtensions);
            foreach (var file in files) {
                DateTime mtime;
                _mtimes.TryGetValue(file.Name, out mtime);

                // If mtime is not set, or the current file is newer than what we have, regenerate the output
                if ((mtime == default(DateTime)) || (mtime < file.LastWriteTime)) {
                    _mtimes[file.Name] = file.LastWriteTime;
                    shouldRegenerate = true;
                    break;
                }
            }

            // If any files have changed, regenerate the output and its minified version
            if (shouldRegenerate) {
                string file, mini;
                _generate(files, out file, out mini);
                Cache.Set(cachePrefix, file);
                Cache.Set(cachePrefix + "-mini", mini);
            }

            return Cache.Get<string>(cachePrefix + (minify ? "-mini" : ""));
        }

        protected delegate void GenerateFileDelegate<in TFiles, TOutput>(TFiles input, out TOutput file, out TOutput mini);
    }
}
