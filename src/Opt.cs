using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public struct Opt<T> : IStructuralEquatable {
        private const MethodImplOptions Options =
            MethodImplOptions.AggressiveInlining
            | MethodImplOptions.AggressiveOptimization;

        // defaults to false
        private readonly bool _hasValue;

        // defaults to default(T)
        private readonly T _value;

        public static readonly Opt<T> Default =
            new();

        public Opt(T value) {
            _hasValue = true;
            _value = value;
        }

        [MethodImpl(Options)]
        public TResult If<TResult>(Func<T, TResult> function, Func<TResult> alternativeFunction) =>
            _hasValue
            ? function(_value)
            : alternativeFunction();

        [MethodImpl(Options)]
        public TResult If<TResult>(Func<T, TResult> function, TResult alternative) =>
            _hasValue
            ? function(_value)
            : alternative;

        [MethodImpl(Options)]
        public Opt<TResult> If<TResult>(Func<T, TResult> function) =>
            _hasValue
            ? new(function(_value))
            : Opt<TResult>.Default;

        [MethodImpl(Options)]
        public bool Equals(Opt<T>? other, IEqualityComparer<T>? comparer) =>
            other is Opt<T> opt
                && (!_hasValue && !opt._hasValue || (comparer ?? EqualityComparer<T>.Default).Equals(_value, opt._value));

        [MethodImpl(Options)]
        public bool Equals(object? other, IEqualityComparer comparer) =>
            other is Opt<T> opt
                && (!_hasValue && !opt._hasValue || (comparer ?? EqualityComparer<T>.Default).Equals(_value, opt._value));

        [MethodImpl(Options)]
        public override bool Equals(object? obj) =>
            obj is Opt<T> opt
                && (!_hasValue && !opt._hasValue || Equals(_value, opt._value));

        [MethodImpl(Options)]
        public int GetHashCode(IEqualityComparer comparer) =>
            _hasValue && _value is not null
            ? comparer.GetHashCode(_value)
            : 0;

        [MethodImpl(Options)]
        public override int GetHashCode() =>
            _hasValue && _value is not null
            ? _value.GetHashCode()
            : 0;

        [MethodImpl(Options)]
        public static Opt<T> operator |(Opt<T> a, Opt<T> b) =>
            a._hasValue
            ? a
            : b;

        [MethodImpl(Options)]
        public static T operator |(Opt<T> a, T b) =>
            a._hasValue
            ? a._value
            : b;

        [MethodImpl(Options)]
        public override string ToString() =>
            _hasValue
            ? _value?.ToString() ?? string.Empty 
            : string.Empty;

        [MethodImpl(Options)]
        public static bool operator ==(Opt<T> a, Opt<T> b) =>
            !a._hasValue && !b._hasValue
            || Equals(a._value, b._value);

        [MethodImpl(Options)]
        public static bool operator !=(Opt<T> a, Opt<T> b) =>
            (a._hasValue || b._hasValue) 
            && !Equals(a._value, b._value);
    }

    public static partial class Functions {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Opt<T> OneOf<T>(T value) =>
            new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Opt<T> NoneOf<T>() =>
            new();
    }

    namespace CatMath.Linq {
        using static System.Functions;
        public static class Opt {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static Opt<TResult> Select<TSource, TResult>(this Opt<TSource> source, Func<TSource, TResult> conversion) =>
                    source.If(conversion.Compose(OneOf), alternative: default);

            // This requires System.Reactive
            //public static IObservable<T> AsObservable<T>(this Opt<T> source) {
            //    source.If(x =>  Observable.Return)
            //}
        }
    }
}
