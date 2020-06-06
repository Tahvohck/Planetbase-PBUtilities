using Planetbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Tahvohck_Mods
{
    public static class CameraManagerHelper
    {
        private static readonly BindingFlags InstanceFlags =
            BindingFlags.Instance |
            BindingFlags.NonPublic | BindingFlags.Public;

        public static GameObject GetSkyboxCamera(this CameraManager manager)
        {
            FieldInfo skydomeField = typeof(CameraManager).GetField("mSkydomeCamera", InstanceFlags);
            return (GameObject)skydomeField.GetValue(manager);
        }
    }
}
