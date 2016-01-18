using System.Collections.Generic;

namespace Builder2
{
    internal sealed class FreezableList<T> : List<T>
    {
        // This would actually just implement IList<T>,
        // and prohibit changes after a Freeze call.

        internal void Freeze()
        {
        }
    }
}
