using Planetbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tahvohck_Mods
{
    /// <summary>
    /// Extensions for ModuleTypes
    /// </summary>
    public static class ModuleHelper
    {
        /// <summary>
        /// Set the required module to build to a new
        /// </summary>
        /// <param name="module"></param>
        public static void ResetRequiredStructure(this ModuleType module)
        {
            var mReqField = module.GetType().GetField(
                "mRequiredStructure",
                BindingFlags.NonPublic | BindingFlags.Instance);
            mReqField.SetValue(module, new ModuleTypeRef());
        }

        /// <summary>
        /// Get a string representation of this module for use in logging.
        /// </summary>
        /// <param name="module"></param>
        /// <returns>A string that represents the module passed in, with newlines.</returns>
        public static string Representation(this ModuleType module)
        {
            int powGen = module.getPowerGeneration(1, Planet.Quantity.High, Planet.Quantity.High);
            int powStore = module.getPowerStorageCapacity(1);
            int powCollected = module.getPowerCollection(1);
            int sMax = module.getMaxSize();
            int sMin = module.getMinSize();
            var requiredType = module.getRequiredModuleType();

            return
                $"\n  NAME:     {module.getName()}" +
                $"\n  SizeMax:  {sMax,2}\tSizeMin:  {sMin,2}\tHeight:  {module.getHeight(),4}" +
                $"\n  POW_COL:  {powCollected}\tPOW_GEN:  {powGen}\tPOW_STO:   {powStore}" +
                $"\n  UsersMax: {module.getMaxUsers()}" +
                $"\n  O2_s1:    {module.getOxygenGeneration(1f)}" +
                $"\n  REQUIRED: {requiredType}{(requiredType is null ? "(null)" : "")}";
        }
    }


    /// <summary>
    /// Based on the ints in <see cref="Planetbase.ModuleType"/>
    /// </summary>
    [Flags]
    public enum ModuleFlags
    {
        LandingPad =        0x00_0001,
        Mine =              0x00_0002,
        Airlock =           0x00_0004,
        Storage =           0x00_0008,
        Dome =              0x00_0010,
        LightAtNight =      0x00_0020,
        NeedsWind =         0x00_0040,
        NeedsLight =        0x00_0080,
        NoFoundation =      0x00_0100,
        DeadEnd =           0x00_0400,
        Walkable =          0x00_0800,
        Blinkenlights =     0x00_1000,
        SnapComponent =     0x00_2000,
        Starport =          0x00_4000,
        Autorotate =        0x00_8000,
        Animated =          0x01_0000,
        CylinderBase =      0x02_0000,
        RemoteOperate =     0x04_0000,
        ScanAnimation =     0x08_0000,
        Prioritizable =     0x10_0000,
        AntiMeteor =        0x20_0000,
        LightningRod =      0x40_0000,
        DisasterDetector =  0x80_0000
    }
}
