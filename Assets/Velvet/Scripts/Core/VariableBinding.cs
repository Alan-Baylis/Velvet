using UnityEngine;

[System.Serializable]
public class VariableBinding {

    public string name;

    public Object target;

    public string viewName;

    public bool randomize;

    public System.Reflection.FieldInfo GetField () {
        var type = this.target.GetType ();
        var options = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
        var field = type.GetField (this.name, options);

        return field;
    }

}
