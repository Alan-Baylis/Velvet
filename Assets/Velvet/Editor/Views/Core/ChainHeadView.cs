using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ChainHeadView : IView {

    public event System.Action OnProcess;
    public event System.Action OnAddBinding;
    public event System.Action OnClearAllBindings;
    public event System.Action<VariableBinding> OnEditBinding;
    public event System.Action<VariableBinding> OnRemoveBinding;

    public void Draw () {
        EditorGUILayout.Separator ();
        if (GUILayout.Button ("Process")) {
            this.OnProcess ();
        }

        EditorGUILayout.Separator ();
        var bindingsView = new BindingListView (
            new BindingListModel (this.model.bindings)
        );
        bindingsView.OnAddBinding += this.OnAddBinding;
        bindingsView.OnClearAllBindings += this.OnClearAllBindings;
        bindingsView.OnEditBinding += this.OnEditBinding;
        bindingsView.OnRemoveBinding += this.OnRemoveBinding;
        bindingsView.Draw ();
    }

    public ChainHeadView (ChainHeadModel model) {
        this.model = model;
    }

    private ChainHeadModel model;

}
