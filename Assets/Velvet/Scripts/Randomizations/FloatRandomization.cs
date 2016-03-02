
namespace Velvet.Randomizations {

    [CustomRandomizer (typeof (float))]
    [System.Serializable]
    public class FloatRandomization : IVariableRandomization {

        public float min;
        public float max;

        public object GetValue (IRandomNumberGenerator rng) {
            return rng.Range (this.min, this.max);
        }

    }

}