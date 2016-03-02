using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (int))]
    public class IntegerBindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.IntField (this.label, (int)value);
        }

        public IntegerBindingView (VariableBinding variable)
        : base (variable) {

        }

    }

}