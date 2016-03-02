using UnityEngine;
using BlurryRoots.Common;

namespace Velvet.Randomizations {

    [CustomRandomizer (typeof (Color))]
    [System.Serializable]
    public class ColorRandomization : IVariableRandomization {

        public Color min
        {
            get { return this.minSer; }
            set { this.minSer = value; }
        }

        public Color max
        {
            get { return this.maxSer; }
            set { this.maxSer = value; }
        }

        public object GetValue (IRandomNumberGenerator rng) {
            return Color.Lerp (this.min, this.max, rng.Float ());
        }

        public ColorRandomization () {
            this.minSer = new ColorSerializable ();
            this.maxSer = new ColorSerializable ();
        }

        private ColorSerializable minSer;
        private ColorSerializable maxSer;

    }

}
