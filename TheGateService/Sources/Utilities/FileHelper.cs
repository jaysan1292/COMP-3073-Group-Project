// Project: TheGateService
// Filename: FileHelper.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

using TheGateService.Extensions;

namespace TheGateService.Utilities {
    public static class FileHelper {
        [DebuggerHidden]
        public static string GetRealPath(string path, string basePath = "") {
            return HttpContext.Current.Server.MapPath(basePath + path);
        }

        [DebuggerHidden]
        public static DirectoryInfo GetDirectory(string path) {
            var dir = new DirectoryInfo(GetRealPath(path));
            if (!dir.Exists) throw new InvalidOperationException("{0} was not found.".F(dir.FullName));
            return dir;
        }
    }
}
