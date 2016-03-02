using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (Color))]
    public class ColorBindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.ColorField (this.label, (Color)value);
        }

        public ColorBindingView (VariableBinding variable)
        : base (variable) {

        }

    }

}
