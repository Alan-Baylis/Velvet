using UnityEngine;
using BlurryRoots.Common;


namespace Velvet.Randomizations {

    [CustomRandomizer (typeof (Vector3))]
    [System.Serializable]
    public class Vector3Randomization : IVariableRandomization {

        public Vector3Serializable min;
        public Vector3Serializable max;

        public object GetValue (IRandomNumberGenerator rng) {
            return Vector3.Lerp (this.min, this.max, rng.Float ());
        }

    }

}
