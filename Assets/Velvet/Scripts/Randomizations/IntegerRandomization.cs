
namespace Velvet.Randomizations {

    [CustomRandomizer (typeof (int))]
    [System.Serializable]
    public class IntegerRandomization : IVariableRandomization {

        public int min;
        public int max;

        public object GetValue (IRandomNumberGenerator rng) {
            return rng.Range (this.min, this.max);
        }

    }

}
