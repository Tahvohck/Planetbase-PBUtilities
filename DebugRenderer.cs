using Planetbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tahvohck_Mods
{
    /// <summary>
    /// Largely duplicated from the internal Planetbase.DebugRenderer
    /// </summary>
    public class DebugRenderer
    {
        internal static readonly int layerIgnoreRaycast = 2;

        private static Dictionary<string, GameObject> _DebugObjects = new Dictionary<string, GameObject>();

        /// <summary>
        /// If a group exists, return it, otherwise create a new group and return it.
        /// </summary>
        public static GameObject AddOrGetGroup(string name)
        {
            if (!_DebugObjects.ContainsKey(name)) {
                _DebugObjects.Add(name, new GameObject() {
                    name = name,
                });
            }
            return _DebugObjects[name];
        }

        /// <summary>
        /// Remove a GameObject group. Return true if successful.
        /// </summary>
        public static bool ClearGroup(string name)
        {
            if (_DebugObjects.ContainsKey(name)) {
                UnityEngine.Object.Destroy(_DebugObjects[name]);
                _DebugObjects.Remove(name);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw a new line.
        /// </summary>
        /// <param name="group">Group name to associate the line with.</param>
        /// <param name="start">Starting location.</param>
        /// <param name="end">Ending location.</param>
        /// <param name="color">Color of the line.</param>
        /// <param name="size">Width of the line.</param>
        public static void AddLine(string group, Vector3 start, Vector3 end, Color color, float size)
        {
            GameObject groupObject = AddOrGetGroup(group);
            LineRenderer lineR = new GameObject() {
                name = "Line",
                layer = layerIgnoreRaycast
            }.AddComponent<LineRenderer>();
            var matPropBlock = new MaterialPropertyBlock();

            matPropBlock.SetColor(ShaderProperty.MainColor, color);
            lineR.SetVertexCount(2);
            lineR.SetPosition(0, start);
            lineR.SetPosition(1, end);
            lineR.SetWidth(size, size);
            lineR.GetComponent<Renderer>().material = ResourceList.getInstance().Materials.PlainColor;
            lineR.SetColors(color, color);

            lineR.transform.parent = groupObject.transform;

            SetMatPropBlock(lineR, color);
            DisableShadows(lineR);
        }

        /// <summary>
        /// Draw a new cube.
        /// </summary>
        /// <param name="group">Group name to associate the cube with.</param>
        /// <param name="position">Location of the cube.</param>
        /// <param name="color">Color of the cube.</param>
        /// <param name="size">Dimensions of the cube.</param>
        public static void AddCube(string group, Vector3 position, Color color, float size)
        {
            GameObject groupObject = AddOrGetGroup(group);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeR = cube.GetComponent<Renderer>();

            cube.name = "Cube";
            cube.layer = layerIgnoreRaycast;
            cube.transform.position = position;
            cube.transform.localScale = new Vector3(size, size, size);
            cube.transform.parent = groupObject.transform;
            cubeR.material = ResourceList.getInstance().Materials.PlainColor;
            SetMatPropBlock(cubeR, color);
            DisableShadows(cubeR);
        }

        private static void SetMatPropBlock(in Renderer renderer, Color color)
        {
            var matPropBlock = new MaterialPropertyBlock();
            matPropBlock.SetColor(ShaderProperty.MainColor, color);
            renderer.SetPropertyBlock(matPropBlock);
        }

        private static void DisableShadows(Renderer renderer)
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
        }
    }
}
