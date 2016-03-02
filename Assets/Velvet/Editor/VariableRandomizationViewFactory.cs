using UnityEngine;
using Velvet.Randomizations;
using Velvet.UI.Views;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public static class VariableRandomizationViewFactory {

    private static readonly string NAMESPACE = "Velvet.UI.Views";

    public static IView CreateFromRandomization (IVariableRandomization randomization) {
        var types = FindViewTypes ();
        foreach (var type in types) {
            var attr = (CustomRandomizationViewAttribute)type.GetCustomAttributes (
                typeof (CustomRandomizationViewAttribute), true
            )[0];
            if (attr.type == randomization.GetType ()) {
                return (IView)System.Activator.CreateInstance (type, randomization);
            }
        }

        throw new UnityException ("Unknown randomization type " + randomization.GetType ());
    }

    private static List<System.Type> FindViewTypes () {
        var q = from t in Assembly.GetExecutingAssembly ().GetTypes ()
                where t.IsClass
                    && NAMESPACE == t.Namespace
                    && typeof (IView).IsAssignableFrom (t)
                    && 0 < t.GetCustomAttributes (typeof (CustomRandomizationViewAttribute), true).Length
                select t;

        return q.ToList ();
    }

}