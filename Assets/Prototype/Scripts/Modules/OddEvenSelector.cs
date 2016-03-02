using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OddEvenSelector : ChainLink {
    
    public SelectionType type;
    
    public override List<GameObject> Process (List<GameObject> input) {
        var result = new List<GameObject> ();

        System.Func<int, bool> check;        
        switch (this.type) {
            case SelectionType.Odd:
                check = (k) => {
                    return 0 != (k % 2);
                };
                break;
            case SelectionType.Even:
                check = (k) => {
                    return 0 == (k % 2);
                };
                break;
            default:
                throw new System.Exception ("Unrecognized type!");
        }

        for (var i = 0; i < input.Count; ++i) {
            if (check (i)) {
                result.Add (input[i]);
            }
        }

        return result;
    }

    public enum SelectionType {
        Odd,
        Even,
    }

}
