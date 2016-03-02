using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlaceOnUnitSphereMutator : ChainLink {

    public float radius;

    public override List<GameObject> Process (List<GameObject> input) {
        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];

            var pos = Random.onUnitSphere * radius;
            go.transform.position = pos;
        }

        return input;
    }

}
