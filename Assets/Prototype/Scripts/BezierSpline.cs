using UnityEngine;

[System.Serializable]
public struct BezierSpline {

    public BezierSplinePoint a;

    public BezierSplinePoint b;

    public Vector3 CalculatePoint (float t) {
        t = PrecheckT (t);

        return this.CalculatePointOptimized (t);
    }

    public Vector3 CalculateTangent (float t) {
        t = PrecheckT (t);

        return this.CalculateTangentOptimized (t);
    }

    public Vector3 CalculateNormal (float t, Vector3 up) {
        t = PrecheckT (t);

        var tangent = this.CalculateTangentOptimized (t);

        return this.CalculateNormalOptimized (t, tangent, up);
    }

    public Quaternion CalculateOrientation (float t, Vector3 up) {
        t = PrecheckT (t);

        var tangent = this.CalculateTangentOptimized (t);
        var normal = this.CalculateNormalOptimized (t, tangent, up);

        return Quaternion.LookRotation (tangent, normal);
    }

    public BezierSpline (BezierSplinePoint a, BezierSplinePoint b) {
        this.a = a;
        this.b = b;
    }

    private Vector3 CalculatePointOptimized (float t) {
        var inverseT = 1f - t;
        var inverseTSquared = inverseT * inverseT;
        var inverseTCubed = inverseTSquared * inverseT;
        var tSquared = t * t;

        return Vector3.zero
            + this.a.position * (inverseTCubed)
            + this.a.controlPoint * (3f * inverseTSquared * t)
            + this.b.controlPoint * (3f * inverseT * tSquared)
            + this.b.position * (tSquared * t)
            ;
    }

    private Vector3 CalculateNormalOptimized (float t, Vector3 tangent, Vector3 up) {
        var binormal = Vector3.Cross (up, tangent);

        return Vector3.Cross (tangent, binormal);
    }

    private Vector3 CalculateTangentOptimized (float t) {
        var omt = 1f - t;
        var omt2 = omt * omt;
        var t2 = t * t;

        var tangent = Vector3.zero
            + this.a.position * (-1f * omt2)
            + this.a.controlPoint * (3f * omt2 - 2f * omt)
            + this.b.controlPoint * (-3f * t2 + 2 * t)
            + this.b.position * (t2)
            ;

        return tangent.normalized;
    }

    private static float PrecheckT (float t) {
        return 0 > t 
            ? 0 
            : (1f < t 
                ? 1f
                : t)
            ;
    }

}