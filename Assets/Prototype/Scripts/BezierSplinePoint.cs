using UnityEngine;
using BlurryRoots.Common;

public struct BezierSplinePoint {

    public Vector3 position;
    public Quaternion rotation;

    public Vector3 controlPoint;

}

[System.Serializable]
public struct BezierSplinePointSerializable {

    public Vector3Serializable position;
    public QuaternionSerializable rotation;

    public Vector3Serializable controlPoint;

    public BezierSplinePointSerializable (BezierSplinePoint other) {
        this.position = other.position;
        this.rotation = other.rotation;
        this.controlPoint = other.controlPoint;
    }

    /// <summary>
    /// Automatic conversion from BezierSplinePointSerializable to BezierSplinePoint.
    /// </summary>
    /// <param name="other">Serializable Vector to convert.</param>
    /// <returns>A new Vector3.</returns>
    public static implicit operator BezierSplinePoint (BezierSplinePointSerializable other) {
        return new BezierSplinePoint () {
            position = other.position,
            rotation = other.rotation,
            controlPoint = other.controlPoint
        };
    }

    /// <summary>
    /// Automatic conversion from BezierSplinePoint to BezierSplinePointSerializable.
    /// </summary>
    /// <param name="other">Vectro3 to convert.</param>
    /// <returns>A new Vector3Serializable.</returns>
    public static implicit operator BezierSplinePointSerializable (BezierSplinePoint other) {
        return new BezierSplinePointSerializable (other);
    }

}