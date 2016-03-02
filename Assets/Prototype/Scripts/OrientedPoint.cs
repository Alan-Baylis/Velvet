using UnityEngine;

class OrientedPoint {

    public Vector3 position;
    public Quaternion rotation;

    public OrientedPoint (Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }

    public Vector3 LocalToWorld (Vector3 point) {
        var rotatedPoint = this.rotation * point;

        return this.position + rotatedPoint;
    }

    public Vector3 WorldToLocal (Vector3 point) {
        var inverseRotation = Quaternion.Inverse (this.rotation);

        return inverseRotation * (point - this.position);
    }

    public Vector3 LocalToWorldDirection (Vector3 direction) {
        return this.rotation * direction;
    }

}
