using Planetbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tahvohck_Mods
{
    public static class PlanetHelper
    {
        public static ResourceType Metal = ResourceTypeList.find<Metal>();
        public static ResourceType Bioplastic = ResourceTypeList.find<Bioplastic>();
        public static ResourceType MedicalSupplies = ResourceTypeList.find<MedicalSupplies>();
        public static Specialization Carrier = SpecializationList.find<Carrier>();
        public static Specialization Constructor = SpecializationList.find<Constructor>();

        public static void AddStartingResource(this Planet p, ResourceType t, int amount)
        {
            try {
                (p.GetType()
                    .GetField("mStartingResources", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(p) as ResourceAmounts)
                    .add(t, amount);
            } catch {

            }
        }

        public static void AddStartingSpecialization(this Planet p, Specialization spec, int amount)
        {
            try {
                (p.GetType()
                    .GetField("mStartingSpecializations", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(p) as List<SpecializationCount>)
                    .Add(new SpecializationCount(spec, amount));
            } catch {

            }
        }
    }
}
