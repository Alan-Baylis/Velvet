using UnityEngine;
using UnityEditor;
using System.Collections;

public class VariableRandomizationView : IView {

    public event System.Action<bool> OnActivationToggled;

    public void Draw () {
        GUI.enabled = this.hasRandomization;

        var prevState = this.state;
        this.state = EditorGUILayout.BeginToggleGroup (
            "Randomize values", prevState
        );
        if (prevState != this.state) {
            this.InvokeAction (this.OnActivationToggled, this.state);
        }
        {
            if (this.hasRandomization) {
                var view = VariableRandomizationViewFactory.CreateFromRandomization (randomization);
                ++EditorGUI.indentLevel;
                view.Draw ();
                --EditorGUI.indentLevel;
            }
            else {
                EditorGUILayout.LabelField ("No randomization available!");
            }
        }
        EditorGUILayout.EndToggleGroup ();

        GUI.enabled = true;
    }

    public VariableRandomizationView (IVariableRandomization randomization, bool state) {
        this.randomization = randomization;
        this.hasRandomization = null != this.randomization;
        this.state = state;
    }

    private void InvokeAction (System.Action<bool> action, bool newState) {
        if (null != action) {
            action (newState);
        }
    }

    private IVariableRandomization randomization;
    private bool hasRandomization;
    private bool state;

}
