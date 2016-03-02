using UnityEngine;
using UnityEditor;
using BlurryRoots.Procedural;
using System.Collections.Generic;

public class AddVariableBindingEditor : EditorWindow {

    public event System.Action<Object, string, Object, string> OnAddCustomVariable;

    // Add menu named "My Window" to the Window menu
    public static AddVariableBindingEditor CreateWindowFor (ChainHeadEditor chEditor) {
        // Get existing open window or if none, make a new one:
        var window = EditorWindow.GetWindow<AddVariableBindingEditor> ();
        window.titleContent.text = "New binding";

        window.view = new AddVariableBindingView ();
        window.view.OnAddCustomVariable += window.OnAddVariableBinding;

        return window;
    }

    internal void OnGUI () {
        this.view.Draw ();
    }

    private void OnAddVariableBinding (Object newChainLinkTarget, string newVarName, Object newVarTarget, string newViewVarName) {
        if (null != this.OnAddCustomVariable) {
            this.OnAddCustomVariable (newChainLinkTarget, newVarName, newVarTarget, newViewVarName);
        }
        else {
            throw new UnityException ("No listener for OnAddCustomVariable specified!");
        }

        this.Close ();
    }
    
    private AddVariableBindingView view;

}
