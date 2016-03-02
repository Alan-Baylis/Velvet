using UnityEngine;

// TODO: move this to library

namespace BlurryRoots.Common {

    /// <summary>This one will automatically convert
    /// between Vector3 and SerializableVector3
    /// </summary>
    [System.Serializable]
    public struct Vector3Serializable {

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
        /// Simple Constructor.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        public Vector3Serializable (float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructs a Vector3Serializable from a Vector3.
        /// </summary>
        /// <param name="other">Vector3 to make serializable.</param>
        public Vector3Serializable (Vector3 other) {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>A stringified Vector3.</returns>
        public override string ToString () {
            return string.Format ("{{x: {0}, y: {1}, z: {2}}}", this.x, this.y, this.z);
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3.
        /// </summary>
        /// <param name="other">Serializable Vector to convert.</param>
        /// <returns>A new Vector3.</returns>
        public static implicit operator Vector3 (Vector3Serializable other) {
            return new Vector3 (other.x, other.y, other.z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3.
        /// </summary>
        /// <param name="other">Vectro3 to convert.</param>
        /// <returns>A new Vector3Serializable.</returns>
        public static implicit operator Vector3Serializable (Vector3 other) {
            return new Vector3Serializable (other);
        }

    }

}