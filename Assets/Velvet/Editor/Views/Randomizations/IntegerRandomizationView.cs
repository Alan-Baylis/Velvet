using UnityEngine;
using UnityEditor;
using Velvet.Randomizations;


namespace Velvet.UI.Views {

    [CustomRandomizationView (typeof (IntegerRandomization))]
    public class IntegerRandomizationView : IView {

        public void Draw () {
            EditorGUILayout.BeginHorizontal ();
            value.min = EditorGUILayout.IntField ("min", value.min);
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            value.max = EditorGUILayout.IntField ("max", value.max);
            EditorGUILayout.EndHorizontal ();
        }

        public IntegerRandomizationView (IntegerRandomization value) {
            this.value = value;
        }

        private IntegerRandomization value;

    }

}
