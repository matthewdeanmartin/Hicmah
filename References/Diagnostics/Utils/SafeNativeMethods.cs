using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ukadc.Diagnostics.Utils
{
    internal static class SafeNativeMethods
    {
        [SuppressMessage("Microsoft.Globalization",
            "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0"),
         SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern void OutputDebugString(string message);
    }
}