using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tahvohck_Mods
{
    /// <summary>
    /// Component flags based on the huge amount of ints in <see cref="Planetbase.ComponentType"/>
    /// </summary>
    [Flags]
    public enum ComponentFlags
    {
        Extractor =         0x000_0001,
        Sleeper =           0x000_0002, // Combined with 0x40 for normal bed
        NeedsRepair =       0x000_0004,
        NeedsOperator =     0x000_0008, // In code as this OR base.anyInteractions()

        Relaxing =          0x000_0010,
        CanDisable =        0x000_0020,
        Healing =           0x000_0040, // In sick bay code
        NoPowerNeed =       0x000_0080,

        WallPart =          0x000_0100,
        SlowRate =          0x000_0400,
        FastRate =          0x000_0800,

        Hydrating =         0x000_1000,
        // There's seriously a gap here for no good reasons.
        IsTree =            0x000_8000,

        IsHanging =         0x001_0000,
        AltProduce =        0x002_0000, // in GuiInfoPanel
        AltConsume =        0x004_0000, // Also in GuiInfoPanel
        AwayFromCorridor =  0x008_0000,

        StoreProduct =      0x010_0000,
        QuadrantAutoRot =   0x020_0000, // What the fuck
        AutoAnchor =        0x040_0000,
        HasSparks =         0x080_0000,

        FaceCorridor =      0x100_0000,
        NeedsTracksuit =    0x200_0000,
        UncoveredHead =     0x400_0000,
        DisasterDetector =  0x800_0000
    }
}
