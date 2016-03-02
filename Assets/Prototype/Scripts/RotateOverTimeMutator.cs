using UnityEngine;
using System.Collections.Generic;

public class RotateOverTimeMutator : BlurryRoots.BlurryBehaviour {

    public Space rotationMode;
    public Vector3 rotationAxis;
    public float degreesPerSecond;

    protected override void OnUpdate () {
        var velocity = Time.deltaTime * degreesPerSecond;

        // rotate 1 degree times velocity
        this.gameObject.transform.Rotate (this.rotationAxis, 1f * velocity, this.rotationMode);
    }

}
