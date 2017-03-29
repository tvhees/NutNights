using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameData
{
    public class PaletteColorAttribute : PropertyAttribute
    {
        public GlobalSettings.Colors Palette { get { return GlobalSettings.Palette; } }
    }
}