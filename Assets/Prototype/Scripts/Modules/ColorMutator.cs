using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ColorMutator : ChainLink {

    public Color color;

    public override List<GameObject> Process (List<GameObject> input) {
        // creates one material for all elements in input
        var mat = new Material (Shader.Find ("Standard"));
        mat.name = "procedural mat " + this.color.ToString ();
        mat.color = this.color;

        foreach (var obj in input) {
            var mr = obj.GetComponent<MeshRenderer> ();
#if UNITY_EDITOR
            mr.sharedMaterial = mat;
#else
            mr.material = mat;
#endif
		}

		return input;
	}
    

}