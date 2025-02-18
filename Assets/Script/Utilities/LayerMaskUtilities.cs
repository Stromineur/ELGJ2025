using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legendhair.Utilities
{
    public static class LayerMaskUtilities
    {
        public static bool IsMaskContainedIn(LayerMask maskToCheck, LayerMask masks)
        {
            return masks == (masks | (1 << maskToCheck));
        }
    }
}
