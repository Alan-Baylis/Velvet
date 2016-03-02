using UnityEngine;
using System.Collections;

public class CustomVariableBindingViewAttribute : System.Attribute {

    public readonly System.Type type;

    public CustomVariableBindingViewAttribute (System.Type type) {
        this.type = type;
    }

}
