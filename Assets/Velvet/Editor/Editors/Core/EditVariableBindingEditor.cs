using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditVariableBindingEditor : EditorWindow {

    public event System.Action<VariableBinding, bool> OnToggleRandomizeVariable;

    public static EditVariableBindingEditor CreateWindow (EditVariableBindingModel model) {
        var window = EditorWindow.CreateInstance<EditVariableBindingEditor> ();

        window.model = model;
        window.titleContent.text = "Randomization";

        window.view = new EditVariableBindingView (
            window.model.variable,
            window.model.GetRandomization ()
        );
        window.view.OnToggleRandomization += window.OnToggleRandomization;

        return window;
    }

    internal void OnGUI () {
        this.view.Draw ();
    }

    private void OnToggleRandomization (VariableBinding v, bool s) {
        if (null != this.OnToggleRandomizeVariable) {
            this.OnToggleRandomizeVariable (v, s);
        }
        else {
            Debug.LogWarning ("No OnToggleRandomizeVariable registered in EditVariableBindingEditor!");
        }
    }

    private EditVariableBindingModel model;
    private EditVariableBindingView view;

}

