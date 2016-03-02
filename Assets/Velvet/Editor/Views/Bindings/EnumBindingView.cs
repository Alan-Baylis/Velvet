using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (System.Enum))]
    public class EnumBindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.EnumPopup (this.label, (System.Enum)value);
        }

        public EnumBindingView (VariableBinding variable)
        : base (variable) {
            //this.values = System.Enum.GetNames (variable.target.GetType ());
        }

        //private string[] values;

    }

}