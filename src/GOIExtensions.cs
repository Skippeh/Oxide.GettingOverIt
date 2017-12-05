using Oxide.GettingOverIt.Types;
using UnityEngine;

namespace Oxide.GettingOverIt
{
    public static class GOIExtensions
    {
        public static LayerMask ToLayerMask(this LayerType layerType)
        {
            return 1 << (int) layerType;
        }
    }
}
