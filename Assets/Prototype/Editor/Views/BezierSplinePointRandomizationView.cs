using UnityEngine;
using UnityEditor;
using Velvet.Randomizations;


namespace Velvet.UI.Views {

    [CustomRandomizationView (typeof (BezierSplinePointRandomization))]
    public class BezierSplinePointRandomizationView : IView {

        public void Draw () {
            EditorGUILayout.BeginVertical ();

            EditorGUILayout.BeginHorizontal ();
            this.randomization.origin = EditorGUILayout.Vector3Field (
                "Origin:", this.randomization.origin
            );
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            this.randomization.radius = EditorGUILayout.FloatField (
                "Distance from origin:", this.randomization.radius
            );
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            this.randomization.controlPointDistance = EditorGUILayout.FloatField (
                "Controlpoint distance to point:", this.randomization.controlPointDistance
            );
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.EndVertical ();
        }

        public BezierSplinePointRandomizationView (BezierSplinePointRandomization randomization) {
            this.randomization = randomization;
        }

        private BezierSplinePointRandomization randomization;

    }

}