using UnityEngine;
using BlurryRoots.Common;

namespace Velvet.Randomizations {

    [CustomRandomizer (typeof (BezierSplinePointSerializable))]
    [System.Serializable]
    public class BezierSplinePointRandomization : IVariableRandomization {

        public Vector3 origin
        {
            get { return this.originSer; }
            set { this.originSer = value; }
        }
        public float radius;

        public float controlPointDistance;

        public object GetValue (IRandomNumberGenerator rng) {
            var position = this.radius * Random.insideUnitSphere + origin;
            var controlPoint = this.controlPointDistance * Random.insideUnitSphere + position;

            return new BezierSplinePointSerializable () {
                position = position,
                controlPoint = controlPoint
            };
        }

        private Vector3Serializable originSer;

    }

}
