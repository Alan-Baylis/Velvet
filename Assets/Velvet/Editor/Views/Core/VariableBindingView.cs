using UnityEngine;
using UnityEditor;
using System.Text;

public abstract class VariableBindingView : IView {

    public void Draw () {
        var field = this.variable.GetField ();
        var value = this.UpdateAndDrawValue (field.GetValue (this.variable.target));
        field.SetValue (this.variable.target, value);
    }

    public VariableBindingView (VariableBinding variable) {
        this.variable = variable;

        var hint = new StringBuilder ()
            .Append ("Alias for ")
            .Append (this.variable.name)
            .Append (" from object ")
            .Append (this.variable.target)
            .ToString ()
            ;
        this.label = new GUIContent (this.variable.viewName, hint);
    }

    protected abstract object UpdateAndDrawValue (object value);

    protected VariableBinding variable;
    protected GUIContent label;

}