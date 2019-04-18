using System;
using System.ComponentModel;

namespace Ncs
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembers
    {
        /// <summary>
        /// Hides the <see cref="Equals"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);

        /// <summary>
        /// Hides the <see cref="GetHashCode"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Hides the <see cref="GetType"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Hides the <see cref="ToString"/> method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}