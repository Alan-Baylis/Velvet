using UnityEngine;
using UnityEditor;

namespace Velvet.UI.Views {

    [CustomVariableBindingView (typeof (BezierSplinePointSerializable))]
    public class BezierSplinePointView : VariableBindingView {

        protected override object UpdateAndDrawValue (object value) {
            var k = (BezierSplinePointSerializable)value;
            k.position = EditorGUILayout.Vector3Field (this.label, k.position);

            return k;
        }

        public BezierSplinePointView (VariableBinding variable)
        : base (variable) {

        }

    }

}
