using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PositionMutator : ChainLink {
    
    public Vector3 position;

    public override List<GameObject> Process (List<GameObject> input) {
        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];

            go.transform.position = this.position;
        }

        return input;
    }

}
