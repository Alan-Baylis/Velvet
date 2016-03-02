using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BindingListView {

    public event System.Action OnAddBinding;
    public event System.Action OnClearAllBindings;
    public event System.Action<VariableBinding> OnEditBinding;
    public event System.Action<VariableBinding> OnRemoveBinding;

    public void Draw () {
        EditorGUILayout.Separator ();
        EditorGUILayout.BeginHorizontal ();
        {
            if (GUILayout.Button ("Add new binding")) {
                this.InvokeAction (this.OnAddBinding);
            }

            if (GUILayout.Button ("Clear all bindings")) {
                this.InvokeAction (this.OnClearAllBindings);
            }
        }
        EditorGUILayout.EndHorizontal ();

        this.model.foldoutOpen = EditorGUILayout.Foldout (
            this.model.foldoutOpen,
            "Custom variables:"
        );

        if (!this.model.foldoutOpen || !this.model.HasEnumerator ()) {
            return;
        }

        ++EditorGUI.indentLevel;
        {
            this.DrawBindings ();
        }
        --EditorGUI.indentLevel;
    }

    public BindingListView (BindingListModel model) {
        this.model = model;
    }

    private void DrawBindings () {
        foreach (var variable in this.model) {
            EditorGUILayout.BeginHorizontal ();
            {
                EditorGUILayout.BeginVertical ();
                EditorGUILayout.BeginHorizontal ();
                {
                    variable.randomize = EditorGUILayout.Toggle (variable.randomize);

                    if (GUILayout.Button ("Randomize")) {
                        this.InvokeEvent (this.OnEditBinding, variable);
                    }

                    if (GUILayout.Button ("Clear")) {
                        this.InvokeEvent (this.OnRemoveBinding, variable);
                    }
                }
                EditorGUILayout.EndHorizontal ();
                EditorGUILayout.EndVertical ();

                var view = VariableBindingViewFactory.CreateFromVariable (variable);
                EditorGUILayout.BeginVertical ();
                {
                    view.Draw ();
                }
                EditorGUILayout.EndVertical ();
            }
            EditorGUILayout.EndHorizontal ();
        }
    }

    private void InvokeAction (System.Action action) {
        if (null != action) {
            action ();
        }
    }

    private void InvokeEvent (System.Action<VariableBinding> action, VariableBinding variable) {
        if (null != action) {
            action (variable);
        }
    }

    private BindingListModel model;

}