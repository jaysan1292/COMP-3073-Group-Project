using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Web;

namespace TheGateService.Utilities {
    public static class FileHelper {
        public static IEnumerable<FileInfo> GetFiles(IEnumerable<string> fileNames, string basePath = "") {
            return fileNames.Select(f => new FileInfo(HttpContext.Current.Server.MapPath(basePath) + f));
        }
    }
}