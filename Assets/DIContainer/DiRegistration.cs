using System;

namespace DI
{
    internal class DiRegistration
    {
        internal Func<DiRegistration, object> Factory { get; set; }
        internal bool IsSiglton { get; set; }
        internal object Instance { get; set; }
    }
}