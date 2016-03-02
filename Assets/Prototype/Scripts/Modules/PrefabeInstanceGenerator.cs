using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PrefabeInstanceGenerator : ChainLink {

    public GameObject prefab;
    public int amount;
    public string designation;

    public override List<GameObject> Process (List<GameObject> input) {
        var result = new List<GameObject> ();

        if (null != input && 0 < input.Count) {
            result.AddRange (input);
        }

        for (var i = 0; i < this.amount; ++i) {
            var obj = GameObject.Instantiate (this.prefab);
            obj.name = this.designation + "_" + i;

            result.Add (obj);
        }

        return result;
    }

}
