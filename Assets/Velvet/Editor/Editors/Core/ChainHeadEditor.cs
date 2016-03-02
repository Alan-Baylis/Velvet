using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof (ChainHead))]
public class ChainHeadEditor : Editor {

    public override void OnInspectorGUI () {
        // render public fields of ChainHead
        base.OnInspectorGUI ();

        // retrive target and cast it to proper type
        var head = (ChainHead)this.target;

        // render the inpsector view
        var view = new ChainHeadView (
            new ChainHeadModel (head.GetVariableBindings ())
        );
        view.OnAddBinding += this.OnAddBinding;
        view.OnClearAllBindings += this.OnClearAllBindings;
        view.OnEditBinding += this.OnEditBinding;
        view.OnProcess += this.OnProcess;
        view.OnRemoveBinding += this.OnRemoveBinding;
        view.Draw ();

        // remove all marked variables
        for (var i = 0; i < this.removeList.Count; ++i) {
            head.RemoveVariable (this.removeList[i]);
        }
        this.removeList.Clear ();

        // tell unity to redraw the inspector, values have changed
        if (GUI.changed) {
            EditorUtility.SetDirty (this.target);
        }
    }

    public ChainHeadEditor () {
        this.removeList = new List<VariableBinding> ();
    }

    private void OnAddBinding () {
        var window = AddVariableBindingEditor.CreateWindowFor (this);
        window.OnAddCustomVariable += this.OnAddCustomVariable;

        window.Show ();
    }
    
    private void OnEditBinding (VariableBinding variable) {
        var head = (ChainHead)this.target;
        var window = EditVariableBindingEditor.CreateWindow (new EditVariableBindingModel (head, variable));
        window.OnToggleRandomizeVariable += this.OnToggleRandomizeVariable;

        window.Show ();
    }

    private void OnToggleRandomizeVariable (VariableBinding variable, bool state) {
        variable.randomize = state;
    }

    private void OnRemoveBinding (VariableBinding variable) {
        this.removeList.Add (variable);
    }

    private void OnProcess () {
        var head = (ChainHead)this.target;

        head.Process (null);
    }

    private void OnClearAllBindings () {
        var head = (ChainHead)this.target;

        head.ClearBindings ();
    }

    private void OnAddCustomVariable (Object newChainLinkTarget, string newVarName, Object newVarTarget, string newViewVarName) {
        var type = newChainLinkTarget.GetType ();
        var options = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
        var field = type.GetField (newVarName, options);

        if (null == field) {
            Debug.LogError ("No field called " + newVarName + " on object " + newVarTarget);
            return;
        }

        var head = (ChainHead)this.target;
        head.AddCustomVariable (newVarName, newChainLinkTarget, newViewVarName);
    }

    public List<VariableBinding> removeList;

}