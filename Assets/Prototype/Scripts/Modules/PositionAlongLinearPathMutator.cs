using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PositionAlongLinearPathMutator : ChainLink {

    public Vector3 origin = Vector3.zero;
    public Vector3 direction = Vector3.up;
    public float offset = 1f;

    public override List<GameObject> Process (List<GameObject> input) {

        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];

            go.transform.position = i * this.offset * this.direction + this.origin;
        }

        return input;
    }

}
