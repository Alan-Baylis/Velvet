using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (float))]
    public class FloatBindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.FloatField (this.label, (float)value);
        }

        public FloatBindingView (VariableBinding variable)
        : base (variable) {

        }

    }

}
