using System;
using UnityModManagerNet;

namespace Tahvohck_Mods
{
    using ModData = UnityModManager.ModEntry;


    /// <summary>
    /// A vareity of mod-level utilities.
    /// </summary>
    public class TahvohckUtil
    {
        internal static BufferedLogger Logger;

        [LoaderOptimization(LoaderOptimization.NotSpecified)]
        public static void Init(ModData data) {
            Logger = new BufferedLogger(data);
            data.OnFixedUpdate = FirstFixedUpdate;
        }

        private static void FirstFixedUpdate(ModData data, float tDelta)
        {
            try {
                Logger.Write("Running all single-fire events.");
                OnFirstUpdate();
            } catch (Exception e) {
                Logger.Buffer("Complete failure loading OnFirstUpdate.");
                Logger.Buffer(e.Message);
                Logger.Buffer(e.StackTrace);
                Logger.Flush();
            }

            // Swap off of this method.
            data.OnFixedUpdate = SubsequentFixedUpdates;
        }

        /// <summary>
        /// Subscribe to this to run a method only once, at the earliest possible point.
        /// </summary>
        public static event Action FirstUpdate;
        private static void OnFirstUpdate()
        {
            if (FirstUpdate is null) {
                Logger.Write("No events subscribed.");
                return;
            }
            foreach (Delegate del in FirstUpdate?.GetInvocationList()) {
                del.DynamicInvoke();
            }
            Logger.Flush();
        }

        private static void SubsequentFixedUpdates(ModData data, float tDelta) { return; }
    }


    public class BufferedLogger
    {
        private ModData.ModLogger _Logger;
        private string _Buffer = "";

        public BufferedLogger(ModData e)
        {
            _Logger = e.Logger;
        }

        public void Buffer(object input)
        {
            if (_Logger is null) return;
            if (_Buffer is "") {
                _Buffer = $"{input}";
            } else {
                _Buffer += $"\n{input}";
            }
        }

        public void Flush()
        {
            if (_Buffer is "" || _Logger is null) return;
            _Logger.Log(_Buffer);
            _Buffer = "";
        }

        public void Write(object input)
        {
            Buffer(input);
            Flush();
        }
    }
}
