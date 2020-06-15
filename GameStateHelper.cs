using Planetbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Tahvohck_Mods
{
    using Module = Planetbase.Module;

    public static class GameStateHelper
    {
        public static BindingFlags instanceFlags =
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public;

        /// <summary>
        /// Created directly from GameStateGame.Mode. Direct casts are possible.
        /// </summary>
        public enum Mode
        {
            Idle,
            PlacingModule,
            EditingModule,
            PlacingComponent,
            CloseCamera,
            NullState
        }

        /// <summary>
        /// Get the game mode. Note that this is NOT the same enum as GameStateGame.Mode, although the
        /// backing variables all match, so direct casts are possible.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Mode CurrentState(this GameStateGame game)
        {
            if (game is null) {
                return Mode.NullState;
            }
            Type GSGMode = game.GetType().GetNestedType("Mode", BindingFlags.NonPublic);

            // This should work on both the Patched and non-patched version of the game,
            // since we ask reflection for public and non public fields.
            return (Mode)game.GetType()
                .GetField("mMode", instanceFlags)
                .GetValue(game);


        }

        /// <summary>
        /// Get the active module, or null if game is null.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Module GetActiveModule(this GameStateGame game)
        {
            if (game is null) {
                return null;
            }

            return (Module)game.GetType()
                .GetField("mActiveModule", instanceFlags)
                .GetValue(game);
        }

        /// <summary>
        /// Get mCurrentModuleSize, or zero if game is null.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static int GetActiveModuleSizeIndex(this GameStateGame game)
        {
            if (game is null) {
                return 0;
            }

            return (int)game.GetType()
                .GetField("mCurrentModuleSize", instanceFlags)
                .GetValue(game);
        }
    }
}
