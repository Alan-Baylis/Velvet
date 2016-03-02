using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PartialRotateMutator : ChainLink {

    public float startAngle;
    public float endAngle;
    
    public Space rotationMode;
    public Vector3 rotationAxis;

    public override List<GameObject> Process (List<GameObject> input) {
        var rotationDistance = this.endAngle - this.startAngle;

        for (var i = 0; i < input.Count; ++i) {
            var go = input[i];
            var angle = ((float)(i + 1) / (float)input.Count) * rotationDistance;

            go.transform.Rotate (this.rotationAxis, angle, this.rotationMode);
        }

        return input;
    }

}
