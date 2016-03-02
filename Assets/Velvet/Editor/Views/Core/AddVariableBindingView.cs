using UnityEngine;
using UnityEditor;
using BlurryRoots.Procedural;
using System.Collections.Generic;

public class AddVariableBindingView : IView {

    public event System.Action<Object, string, Object, string> OnAddCustomVariable;

    private void UpdateTargetObject () {
        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Target GameObject:");
        // setup object reference for head
        this.newVarTarget = EditorGUILayout.ObjectField (
            this.newVarTarget,
            typeof (ChainHead),
            true
        );
        EditorGUILayout.EndHorizontal ();
    }

    public void Draw () {
        // add new binding part
        EditorGUILayout.BeginVertical ();
        {
            this.UpdateTargetObject ();
            GUI.enabled = null != this.newVarTarget;

            if (GUI.enabled) {
                var targetHead = (BlurryRoots.BlurryBehaviour)this.newVarTarget;
                var chainLinks = new List<IChainLink<List<GameObject>>> ();
                targetHead.GetComponents (chainLinks);

                // remove head from list
                //chainLinks.Remove ((IChainLink<List<GameObject>>)targetHead);

                if (0 < chainLinks.Count) {
                    var chainLinkNames = new string[chainLinks.Count];
                    for (var i = 0; i < chainLinks.Count; ++i) {
                        chainLinkNames[i] = chainLinks[i].GetType ().ToString ();
                    }

                    EditorGUILayout.BeginHorizontal ();
                    EditorGUILayout.LabelField ("Module:");
                    // setup object reference for chain link
                    this.newChainLinkTargetIndex = EditorGUILayout.Popup (
                        this.newChainLinkTargetIndex,
                        chainLinkNames
                    );
                    EditorGUILayout.EndHorizontal ();

                    this.newChainLinkTarget = (Object)chainLinks[this.newChainLinkTargetIndex];

                    // inspect the type of the selected module
                    var type = this.newChainLinkTarget.GetType ();
                    var options = System.Reflection.BindingFlags.Public
                        | System.Reflection.BindingFlags.Instance
                        ;
                    var fields = type.GetFields (options);
                    if (0 < fields.Length) {
                        var fieldNames = new string[fields.Length];
                        // extract the names of the public fields
                        for (var i = 0; i < fields.Length; ++i) {
                            fieldNames[i] = fields[i].Name + " (" + fields[i].FieldType + ")";
                        }

                        EditorGUILayout.BeginHorizontal ();
                        EditorGUILayout.LabelField ("Field:");
                        // present them as a pull down menu
                        this.newFieldToBeBoundIndex = EditorGUILayout.Popup (
                            this.newFieldToBeBoundIndex, fieldNames
                        );
                        EditorGUILayout.EndHorizontal ();

                        // and store the selected name
                        this.newVarName = fields[this.newFieldToBeBoundIndex].Name;

                        EditorGUILayout.BeginHorizontal ();
                        EditorGUILayout.LabelField ("Alias:");
                        this.newViewVarName = EditorGUILayout.TextField (this.newViewVarName);
                        EditorGUILayout.EndHorizontal ();
                    }
                    else {
                        EditorGUILayout.LabelField ("No public fields.");
                    }
                }
                else {
                    EditorGUILayout.LabelField ("No modules attached.");
                }
            }

            var validVar = !string.IsNullOrEmpty (this.newVarName)
                && !string.IsNullOrEmpty (this.newViewVarName)
                ;
            if (GUI.enabled && !validVar) {
                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.HelpBox ("Please specifiy a proper alias!", MessageType.Error);
                EditorGUILayout.EndHorizontal ();
            }

            GUI.enabled = GUI.enabled && validVar;
            if (GUILayout.Button ("Confirm")) {
                this.OnAddCustomVariable (this.newChainLinkTarget, this.newVarName, this.newVarTarget, this.newViewVarName);
            }
            GUI.enabled = true;
        }
        EditorGUILayout.EndVertical ();
    }

    public Object newVarTarget;
    public string newVarName;
    public string newViewVarName;
    public int newChainLinkTargetIndex;
    public Object newChainLinkTarget;
    public int newFieldToBeBoundIndex;

}
