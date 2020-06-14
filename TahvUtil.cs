using System;
using UnityModManagerNet;

namespace Tahvohck_Mods
{
    using ModData = UnityModManager.ModEntry;
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
