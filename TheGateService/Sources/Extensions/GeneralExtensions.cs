// Project: TheGateService
// Filename: GeneralExtensions.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        [DebuggerHidden, StringFormatMethod("str")]
        public static string F(this string str, params object[] args) {
            return string.Format(str, args);
        }
    }
}
