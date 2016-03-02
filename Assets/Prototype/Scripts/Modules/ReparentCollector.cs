using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ReparentCollector : ChainLink {

    public GameObject parentPrefab;
    public string parentName;

    public override List<GameObject> Process (List<GameObject> input) {
        if (null == input) {
            return null;
        }

        var parent = null == this.parentPrefab
            ? new GameObject (this.parentName)
            : GameObject.Instantiate (this.parentPrefab)
            ;
        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];
            go.transform.SetParent (parent.transform);
        }

        return new List<GameObject> () {
            parent,
        };
    }

}
