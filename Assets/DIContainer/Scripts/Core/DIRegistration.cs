using System;

namespace DI
{
    internal class DIRegistration
    {
        internal Func<DIContainer, object> Factory { get; set; }
        internal bool IsSiglton { get; set; }
        internal object Instance { get; set; }
    }
}