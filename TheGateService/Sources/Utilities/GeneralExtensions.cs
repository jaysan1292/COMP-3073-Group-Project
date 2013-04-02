using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

namespace TheGateService.Utilities {
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
            return items.Aggregate(true, (b, x) => b && enumerable.Contains(x));
        }

        [DebuggerHidden]
        public static T GetRandom<T>(this IEnumerable<T> enumerable) {
            var values = enumerable as T[] ?? enumerable.ToArray();
            var idx = Random.Next(0, values.Count() - 1);
            return values.ElementAt(idx);
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
    }
}
