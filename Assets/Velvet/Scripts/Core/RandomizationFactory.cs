using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class RandomizationFactory {

    public static IVariableRandomization CreateFromBinding (VariableBinding variable) {
        var field = variable.GetField ();
        var value = field.GetValue (variable.target);

        var types = FindRandomizationTypes ();
        foreach (var type in types) {
            var attr = (CustomRandomizerAttribute)type.GetCustomAttributes (typeof (CustomRandomizerAttribute), true)[0];
            if (attr.type == field.FieldType) {
                return (IVariableRandomization)System.Activator.CreateInstance (type);
            }
        }

        throw new UnityException ("Type not supported! " + field.FieldType.ToString ());
    }

    private static List<System.Type> FindRandomizationTypes () {
        var q = from t in Assembly.GetExecutingAssembly ().GetTypes ()
                where t.IsClass
                    && NAMESPACE == t.Namespace
                    && typeof (IVariableRandomization).IsAssignableFrom (t)
                    && 0 < t.GetCustomAttributes (typeof (CustomRandomizerAttribute), true).Length
                select t;
        
        return q.ToList ();
    }

    private static readonly string NAMESPACE = "Velvet.Randomizations";

}
