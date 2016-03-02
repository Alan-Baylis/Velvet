using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (Vector3))]
    public class Vector3BindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.Vector3Field (this.label, (Vector3)value);
        }

        public Vector3BindingView (VariableBinding variable)
        : base (variable) {

        }

    }

}