using UnityEngine;
using UnityEditor;
using Velvet.Randomizations;

namespace Velvet.UI.Views {

    [CustomRandomizationView (typeof (FloatRandomization))]
    public class FloatRandomizationView : IView {

        public void Draw () {
            EditorGUILayout.BeginHorizontal ();
            value.min = EditorGUILayout.FloatField ("min", value.min);
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            value.max = EditorGUILayout.FloatField ("max", value.max);
            EditorGUILayout.EndHorizontal ();
        }

        public FloatRandomizationView (FloatRandomization value) {
            this.value = value;
        }

        private FloatRandomization value;

    }

}

