using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public struct NotUsed : IEquatable<NotUsed> {
        public static NotUsed Default { get; } = 
            new NotUsed();

        public bool Equals(NotUsed other) => 
            true;

        public override bool Equals(object? obj) =>
            obj is NotUsed;

        public override int GetHashCode() => 
            0;

        public override string ToString() =>
            string.Empty;

#pragma warning disable IDE0060 // Remove unused parameter
        public static bool operator ==(NotUsed a, NotUsed b) =>
            true;

        public static bool operator !=(NotUsed a, NotUsed b) =>
            false;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}
