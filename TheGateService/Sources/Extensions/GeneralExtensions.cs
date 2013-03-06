// Project: TheGateService
// Filename: GeneralExtensions.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using JetBrains.Annotations;

namespace TheGateService.Extensions {
    public static class GeneralExtensions {
        [DebuggerHidden]
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            return source.OrderBy(x => Guid.NewGuid());
        }

        [DebuggerHidden]
        public static T GetRandom<T>(this IEnumerable<T> source) {
            return source.Shuffle().ElementAt(0);
        }

        [DebuggerHidden]
        public static IEnumerable<T> GetRandomRange<T>(this IEnumerable<T> source, int count = 1) {
            var enumerable = source as T[] ?? source.ToArray();
            var items = enumerable.Shuffle().ToArray();
            var total = items.Length;

            for (var i = 0; i < count && i < total; i++) 
                yield return items[i];
        }

        // http://stackoverflow.com/questions/3527203/getfiles-with-multiple-extentions
        [DebuggerHidden]
        public static FileInfo[] GetFiles(this DirectoryInfo dir, params string[] extensions) {
            if (extensions == null) throw new ArgumentNullException("extensions");
            var files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension)).ToArray();
        }

        [DebuggerHidden, StringFormatMethod("str")]
        public static string F(this string str, params object[] args) {
            return string.Format(str, args);
        }
    }
}
