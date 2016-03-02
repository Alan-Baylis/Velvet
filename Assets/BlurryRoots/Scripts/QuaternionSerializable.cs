using UnityEngine;

// TODO: move this to library

namespace BlurryRoots.Common {

    /// <summary>
    /// Helper class to serialize Quaternions.
    /// </summary>
    [System.Serializable]
    public struct QuaternionSerializable {

        /// <summary>
        /// x component
        /// </summary>
        public float x;

        /// <summary>
        /// y component
        /// </summary>
        public float y;

        /// <summary>
        /// z component
        /// </summary>
        public float z;

        /// <summary>
        /// w component
        /// </summary>
        public float w;

        /// <summary>
        /// Simple Constructor.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        public QuaternionSerializable (float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a QuaternionSerializable from a Quaternion.
        /// </summary>
        /// <param name="other">Vector3 to make serializable.</param>
        public QuaternionSerializable (Quaternion other) {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
            this.w = other.w;
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>A stringified Vector3.</returns>
        public override string ToString () {
            return new Quaternion (this.x, this.y, this.z, this.w).ToString ();
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3.
        /// </summary>
        /// <param name="other">Serializable Vector to convert.</param>
        /// <returns>A new Vector3.</returns>
        public static implicit operator Quaternion (QuaternionSerializable other) {
            return new Quaternion (other.x, other.y, other.z, other.w);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3.
        /// </summary>
        /// <param name="other">Vectro3 to convert.</param>
        /// <returns>A new Vector3Serializable.</returns>
        public static implicit operator QuaternionSerializable (Quaternion other) {
            return new QuaternionSerializable (other);
        }

    }

}
