using UnityEngine;

// TODO: move this to library

namespace BlurryRoots.Common {

    /// <summary>
    /// Helper class to serialize color value types.
    /// </summary>
    [System.Serializable]
    public class ColorSerializable {

        public float r;
        public float g;
        public float b;
        public float a;

        /// <summary>
        /// Create new default serializable color.
        /// </summary>
        public ColorSerializable () {
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.a = 255;
        }

        /// <summary>
        /// Createa serializable color from color.
        /// </summary>
        /// <param name="other"></param>
        public ColorSerializable (Color other) {
            this.r = other.r;
            this.g = other.g;
            this.b = other.b;
            this.a = other.a;
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3.
        /// </summary>
        /// <param name="other">Serializable Vector to convert.</param>
        /// <returns>A new Vector3.</returns>
        public static implicit operator Color (ColorSerializable other) {
            return new Color (other.r, other.g, other.b, other.a);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3.
        /// </summary>
        /// <param name="other">Vectro3 to convert.</param>
        /// <returns>A new Vector3Serializable.</returns>
        public static implicit operator ColorSerializable (Color other) {
            return new ColorSerializable (other);
        }

    }

}
