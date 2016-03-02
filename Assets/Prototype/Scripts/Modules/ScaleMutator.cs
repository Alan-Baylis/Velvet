using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ScaleMutator : ChainLink {
    
    public Vector3 scale;

    public override List<GameObject> Process (List<GameObject> input) {
        if (null == input) {
            return null;
        }
        
        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];

            go.transform.localScale = scale;
        }

        return input;
    }

}
