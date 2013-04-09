using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using JetBrains.Annotations;

namespace TheGateService.Extensions {
    public static class GeneralExtensions {
        private static readonly Random Random = new Random();

        [DebuggerHidden]
        public static bool Empty<T>(this IEnumerable<T> e) {
            return e.Count() != 0;
        }

        [DebuggerHidden]
        public static float NextFloat(this Random random) {
            return (float) random.NextDouble();
        }

        [DebuggerHidden]
        [StringFormatMethod("str")]
        public static string Fmt(this string str, params object[] args) {
            return String.Format(str, args);
        }

        [DebuggerHidden]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var x in enumerable) action(x);
        }

        [DebuggerHidden]
        public static bool Contains<T>(this IEnumerable<T> enumerable, IEnumerable<T> items) {
            return items.Aggregate(true, (b, x) => b && Enumerable.Contains<T>(enumerable, x));
        }

        [DebuggerHidden]
        public static bool IsType<T>(this object o) {
            return o.IsType(typeof(T));
        }

        [DebuggerHidden]
        public static bool IsType(this object o, Type t) {
            return o.GetType() == t;
        }

        [DebuggerHidden]
        public static bool IsDerivedFrom<TBaseType>(this Type type) {
            if (type.BaseType == null) return false;
            return type == typeof(TBaseType) || type.BaseType.IsDerivedFrom<TBaseType>();
        }

        [DebuggerHidden]
        public static bool Implements<TInterface>(this Type type) {
            return type.GetInterfaces().Contains(typeof(TInterface));
        }

        [DebuggerHidden]
        public static int ToNearestInt(this float f) {
            return (int) (f > 0 ? (f + 0.5f) : (f - 0.5f));
        }

        [DebuggerHidden]
        public static void Swap<T>(this IList<T> list, int firstIdx, int secondIdx) {
            if (firstIdx < 0 || firstIdx > list.Count) throw new ArgumentOutOfRangeException("firstIdx");
            if (secondIdx < 0 || secondIdx > list.Count) throw new ArgumentOutOfRangeException("secondIdx");

            var temp = list[firstIdx];
            list[firstIdx] = list[secondIdx];
            list[secondIdx] = temp;
        }

        [DebuggerHidden]
        public static void Swap<T>(this IList<T> list, T first, T second) {
            list.Swap(list.IndexOf(first), list.IndexOf(second));
        }

        [DebuggerHidden]
        public static string Name(this Enum e) {
            return Enum.GetName(e.GetType(), e);
        }

        [DebuggerHidden]
        public static bool IsAny<T>(this T input, T p1, params T[] pn) {
            return input.Equals(p1) ||
                   pn.Contains(input);
        }

        [DebuggerHidden]
        public static bool IsNotAny<T>(this T input, T p1, params T[] pn) {
            return !input.IsAny(p1, pn);
        }

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

        [DebuggerHidden]
        [StringFormatMethod("str")]
        public static string F(this string str, params object[] args) {
            return String.Format(str, args);
        }

        public static string Limit(this string str, int characterCount) {
            return str.Length <= characterCount ?
                       str :
                       str.Substring(0, characterCount).TrimEnd(' ');
        }

        public static string LimitWords(this string str, int characterCount) {
            if (characterCount < 5) return str.Limit(characterCount); 
            if (str.Length <= characterCount - 3) return str;

            var lastspace = str.Substring(0, characterCount - 3).LastIndexOf(' ');
            if (lastspace > 0 && lastspace > characterCount - 10)
                return str.Substring(0, lastspace) + "…";

            return str.Substring(0, characterCount - 3) + "…";
        }
    }
}
