using UnityEngine;
using UnityEditor;
using Velvet.Randomizations;


namespace Velvet.UI.Views {

    [CustomRandomizationView (typeof (Vector3Randomization))]
    public class Vector3RandomizationView : IView {

        public void Draw () {
            EditorGUILayout.BeginHorizontal ();
            value.min = EditorGUILayout.Vector3Field ("min", value.min);
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            value.max = EditorGUILayout.Vector3Field ("max", value.max);
            EditorGUILayout.EndHorizontal ();
        }

        public Vector3RandomizationView (Vector3Randomization value) {
            this.value = value;
        }

        private Vector3Randomization value;

    }

}

