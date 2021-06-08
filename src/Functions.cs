using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public static partial class Functions {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T Id<T>(T value) => 
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Func<TIgnored, TResult> Const<TIgnored, TResult>(this TResult value) =>
            x => value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Func<T2, T1, T3> Swap<T1, T2, T3>(Func<T1, T2, T3> function) =>
            (x, y) => function(y, x);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Func<T2, Func<T1, T3>> Swap<T1, T2, T3>(Func<T1, Func<T2, T3>> function) =>
            y => x => function(x)(y);


        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> function1, Func<T2, T3> function2) =>
            x => function2(function1(x));

        public static TOut Apply<TIn, TOut>(this Func<TIn, TOut> function, TIn value) =>
            function(value);
    }
}
