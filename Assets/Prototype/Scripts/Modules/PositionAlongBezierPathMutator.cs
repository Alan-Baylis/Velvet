using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[System.Serializable]
public class PositionAlongBezierPathMutator : ChainLink {

    public BezierSplinePointSerializable pointA =
        new BezierSplinePoint () {
            position = new Vector3 (0, 0, -2),
            controlPoint = new Vector3 (0, 4, -2)
        };

    public BezierSplinePointSerializable pointB =
        new BezierSplinePoint () {
            position = new Vector3 (0, 0, 10),
            controlPoint = new Vector3 (0, 4, 10)
        };

    public bool showPointHandles;
    
	public int gizmoResolution = 12;

	public float curvePointSize = 0.3f;
	public Color curvePointColor = Color.green;

	public float controlPointSize = 0.3f;
	public Color controlPointColor = Color.red;

	public float tangentLength = 1f;
	public Color tangentColor = Color.blue;

    public float normalLength = 1f;
    public Color normalColor = Color.red;

    [HideInInspector]
    public BezierSpline spline;

    public override List<GameObject> Process (List<GameObject> input) {
        if (null == input || 0 == input.Count) {
            Debug.LogWarning (this + " gets bypassed because not input was provided!");
            return input;
        }

        this.UpdateSplineValues (input.Count - 1);
        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];

            var p = this.points[i];
            var t = this.tangents[i];
            var n = this.normals[i];

            this.AlignGameObject (go, p, t, n);
        }

        return input;
    }

    public PositionAlongBezierPathMutator () {
		//this.spline = new BezierSpline ();
    }

    protected override void OnAwake () {
        this.UpdateSplineValues (this.gizmoResolution - 1);
    }

    protected override void OnValueChanged () {
        this.gizmoResolution = 3 > this.gizmoResolution ? 3 : this.gizmoResolution;
    }

    protected override void OnUpdate () {
        this.UpdateSplineValues (this.gizmoResolution - 1);
    }

    protected override void OnGizmos (bool selected) {
        if (!selected) {
            return;
        }

        this.OnDrawControlPointsGizmos ();
        this.OnDrawCurveSegementsGizmos ();
        this.OnDrawCurveTangentsAndNormals ();

    }

    private void AlignGameObject (GameObject go, Vector3 point, Vector3 tangent, Vector3 normal) {
        // change objects position to be at point in curve
        go.transform.position = point;

        // and rotate it so it faces the tangent of the point in the curve
        var lookRotation = Quaternion.LookRotation (tangent, normal);
        go.transform.rotation = lookRotation;
    }

    private void UpdateSplineValues (int count) {
        this.spline = new BezierSpline (this.pointA, this.pointB);

        this.points = new List<Vector3> ();
        this.points.Add (this.spline.CalculatePoint (0f));
        for (var i = 1; i < count; ++i) {
            var t = i / (float)count;
            var p = this.spline.CalculatePoint (t);
            this.points.Add (p);
        }
        this.points.Add (this.spline.CalculatePoint (1f));

        this.tangents = new List<Vector3> ();
        this.normals = new List<Vector3> ();
        var localUp = this.transform.up;
        var stepWidth = 1f / (float)(points.Count - 1);
        for (var i = 0; i < points.Count; ++i) {
            var t = i * stepWidth;

            this.tangents.Add (this.spline.CalculateTangent (t));
            this.normals.Add (this.spline.CalculateNormal (t, localUp));
        }
    }

    private void OnDrawControlPointsGizmos () {
        var c = Gizmos.color;
        Gizmos.color = this.controlPointColor;

        Gizmos.DrawSphere (this.spline.a.position, this.controlPointSize);
        Gizmos.DrawSphere (this.spline.a.controlPoint, this.controlPointSize);
        Gizmos.DrawSphere (this.spline.b.controlPoint, this.controlPointSize);
        Gizmos.DrawSphere (this.spline.b.position, this.controlPointSize);

        Gizmos.color = c;
    }

    private void DrawSegmentGizmos (Vector3 a, Vector3 b) {
        Gizmos.DrawSphere (b, this.curvePointSize);
        Gizmos.DrawLine (a, b);
    }

    private void OnDrawCurveSegementsGizmos () {
        var c = Gizmos.color;
        Gizmos.color = this.curvePointColor;

        var previousPoint = this.spline.a.position;
        for (var i = 0; i < this.points.Count; ++i) {
            var p = this.points[i];

            if (0 < i) {
                this.DrawSegmentGizmos (previousPoint, p);
            }

            previousPoint = p;
        }

        Gizmos.DrawLine (previousPoint, this.spline.b.position);

        Gizmos.color = c;
    }

    private void OnDrawCurveTangentsAndNormals () {
        var c = Gizmos.color;

        for (var i = 0; i < this.points.Count; ++i) {
            var p = this.points[i];

            var t = this.tangents[i];
            Gizmos.color = this.tangentColor;
            Gizmos.DrawRay (p, t * this.tangentLength);

            var n = this.normals[i];
            Gizmos.color = this.normalColor;
            Gizmos.DrawRay (p, n * this.normalLength);
        }

        Gizmos.color = c;
    }

    private List<Vector3> points = new List<Vector3> ();
    private List<Vector3> tangents = new List<Vector3> ();
    private List<Vector3> normals = new List<Vector3> ();

}





