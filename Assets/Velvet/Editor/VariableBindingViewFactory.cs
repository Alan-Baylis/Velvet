using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class VariableBindingViewFactory {

    public static VariableBindingView CreateFromVariable (VariableBinding variable) {
        var field = variable.GetField ();

        var types = FindViewTypes ();
        foreach (var type in types) {
            var attr = (CustomVariableBindingViewAttribute)type.GetCustomAttributes (
                typeof (CustomVariableBindingViewAttribute), true)[0];
            if (attr.type.IsAssignableFrom (field.FieldType)) {
                return (VariableBindingView)System.Activator.CreateInstance (type, variable);
            }
        }

        throw new UnityException ("Unkown field type: " + field.FieldType);
    }

    private static List<System.Type> FindViewTypes () {
        var q = from t in Assembly.GetExecutingAssembly ().GetTypes ()
                where t.IsClass
                    && NAMESPACE == t.Namespace
                    && typeof (VariableBindingView).IsAssignableFrom (t)
                    && 0 < t.GetCustomAttributes (typeof (CustomVariableBindingViewAttribute), true).Length
                select t;

        return q.ToList ();
    }

    private static readonly string NAMESPACE = "Velvet.UI.Views";

}