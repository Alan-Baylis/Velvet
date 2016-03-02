using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditVariableBindingView : IView {

    public event System.Action<VariableBinding, bool> OnToggleRandomization;

    public void Draw () {
        EditorGUILayout.BeginVertical ();
        EditorGUILayout.LabelField ("Gameobject (Module):\t" + this.variable.target);
        EditorGUILayout.EndVertical ();

        EditorGUILayout.BeginVertical ();
        EditorGUILayout.LabelField ("Field:\t\t\t" + this.variable.name);
        EditorGUILayout.EndVertical ();

        EditorGUILayout.Separator ();
        
        EditorGUILayout.LabelField ("Alias/Value:");
        ++EditorGUI.indentLevel;
        var view = VariableBindingViewFactory.CreateFromVariable (this.variable);
        view.Draw ();
        --EditorGUI.indentLevel;

        EditorGUILayout.Separator ();

        var randView = new VariableRandomizationView (this.randomization, this.variable.randomize);
        randView.OnActivationToggled += this.OnActivationToggled;
        randView.Draw ();
    }

    public EditVariableBindingView (VariableBinding variable, IVariableRandomization randomization) {
        this.variable = variable;
        this.randomization = randomization;
    }

    private void OnActivationToggled (bool newState) {
        if (null != this.OnToggleRandomization) {
            this.OnToggleRandomization (this.variable, newState);
        }
    }

    private VariableBinding variable;
    private IVariableRandomization randomization;

}
