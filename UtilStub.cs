using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tahvohck_Mods
{
    /// <summary>
    /// Stub to make UMM load the mod.
    /// </summary>
    public class UtilStub
    {
        [LoaderOptimization(LoaderOptimization.NotSpecified)]
        public static void Init() { return; }
    }
}
