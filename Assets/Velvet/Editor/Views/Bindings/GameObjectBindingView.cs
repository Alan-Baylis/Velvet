using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (GameObject))]
    public class GameObjectBindingView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            return EditorGUILayout.ObjectField (this.label, (GameObject)value, typeof (GameObject), true);
        }

        public GameObjectBindingView (VariableBinding variable)
        : base (variable) {

        }

    }

}
