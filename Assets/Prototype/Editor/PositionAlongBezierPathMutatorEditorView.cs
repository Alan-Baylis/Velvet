using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (PositionAlongBezierPathMutator))]
public class PositionAlongBezierPathMutatorEditorView : Editor {

    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();
    }

    internal void OnSceneGUI () {
        var mutator = (PositionAlongBezierPathMutator)this.target;

        if (!mutator.showPointHandles) {
            return;
        }

        var handleTransform = mutator.transform;
        // get rotation depending on current editor settings
        var handleRotation = Tools.pivotRotation == PivotRotation.Local
            ? handleTransform.rotation
            : Quaternion.identity
            ;

        var a = mutator.pointA;
        a.position = UpdatePoint (mutator,
            handleTransform, handleRotation, mutator.spline.a.position,
           "Move point a"
        );
        a.controlPoint = UpdatePoint (mutator,
            handleTransform, handleRotation, mutator.spline.a.controlPoint,
            "Move control point for a"
        );
        mutator.pointA = a;
        Handles.DrawLine (
            mutator.pointA.controlPoint,
            mutator.pointA.position
        );

        var b = mutator.pointB;
        b.position = UpdatePoint (mutator,
           handleTransform, handleRotation, mutator.spline.b.position,
           "Move point b"
        );
        b.controlPoint = UpdatePoint (mutator,
            handleTransform, handleRotation, mutator.spline.b.controlPoint,
            "Move control point for b"
        );
        mutator.pointB = b;
        Handles.DrawLine (
            mutator.pointB.controlPoint,
            mutator.pointB.position
        );
    }

    private static Vector3 UpdatePoint (PositionAlongBezierPathMutator mutator, Transform t, Quaternion r, Vector3 rawPoint, string actionName) {
        // get world position from local point
        var point = t.TransformPoint (rawPoint);

        // draw in white
        Handles.color = Color.white;

        // tell editor to check if handle has been selected and moved
        EditorGUI.BeginChangeCheck ();
        // update the local position
        point = Handles.DoPositionHandle (point, r);
        // if the handle has been moved
        if (EditorGUI.EndChangeCheck ()) {
            // write to the undo history
            Undo.RecordObject (mutator, "Move Point");
            // tell unity a value has been changed so it can redraw the interface
            EditorUtility.SetDirty (mutator);
            // update the point from world space to local space
            rawPoint = t.InverseTransformPoint (point);
        }

        return rawPoint;
    }

}
