using System;
using System.Globalization;

namespace DocumentSender.Model
{
    public sealed class DocumentId : IEquatable<DocumentId>, IComparable<DocumentId>
    {
        /// <summary>
        /// Initializes instances of this class.
        /// </summary>
        /// <param name="rawId"></param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="rawId"/> is not positive.</exception>
        public DocumentId(long rawId)
        {
            if (rawId <= default(long))
            {
                throw new ArgumentOutOfRangeException(nameof(rawId), rawId, "Must be positive integer.");
            }

            Value = rawId;
        }

        public long Value { get; }

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => Equals(obj as DocumentId);

        public bool Equals(DocumentId other)
        {
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || Value == other.Value;
        }

        public int CompareTo(DocumentId other)
        {
            if (other == null)
            {
                return 1;
            }

            return ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value);
        }

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}