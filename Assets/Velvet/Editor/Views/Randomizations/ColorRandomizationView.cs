using UnityEngine;
using UnityEditor;
using Velvet.Randomizations;

namespace Velvet.UI.Views {

    [CustomRandomizationView (typeof (ColorRandomization))]
    public class ColorRandomizationView : IView {

        public void Draw () {
            EditorGUILayout.BeginHorizontal ();
            this.color.min = EditorGUILayout.ColorField ("min", this.color.min);
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.BeginHorizontal ();
            this.color.max = EditorGUILayout.ColorField ("max", this.color.max);
            EditorGUILayout.EndHorizontal ();
        }

        public ColorRandomizationView (ColorRandomization color) {
            this.color = color;
        }

        private ColorRandomization color;

    }

}
