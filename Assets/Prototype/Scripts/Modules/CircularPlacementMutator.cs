using UnityEngine;
using BlurryRoots.Procedural;
using System.Collections.Generic;

[System.Serializable]
public class CircularPlacementMutator : ChainLink {

    public float radius;

	public override List<GameObject> Process (List<GameObject> input) {
		var results = new List<GameObject> ();

		var steps = (2f * Mathf.PI) / input.Count;
		for (var i = 0; i < input.Count; ++i) {
			var obj = input[i];
			var angle = i * steps;

			var x = Mathf.Cos (angle) * this.radius + obj.transform.position.x;
			var y = Mathf.Sin (angle) * this.radius + obj.transform.position.z;
			var z = obj.transform.position.y;

			obj.transform.position = new Vector3 (x, y, z);
			results.Add (obj);
		}

		return results;
	}

}