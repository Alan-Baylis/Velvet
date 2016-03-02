using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupershapePolygonGenerator : ChainLink {

    public float radius;

    [Range (3, 100)]
    public int resolution;

    public int m;

    public int n1;
    public int n2;
    public int n3;

    public int a;
    public int b;

    protected override void OnValueChanged () {
        this.radius = !(0 < this.radius) ? 0.01f : this.radius;
    }

    public override List<GameObject> Process (List<GameObject> input) {
        var shape = new Supershape (this.m, this.n1, this.n2, this.n3, this.a, this.b);
        var vertices = new Vector3[this.resolution];

        for (var i = 0; i < this.resolution; ++i) {
            var angle = ((float)i / this.resolution) * 360f;
            var v = this.radius * shape.CalculatePoint (angle);

            vertices[i] = new Vector3 (v.x, v.y, 0);
        }

        var mesh = new Mesh ();
        mesh.vertices = vertices;

        var go = new GameObject ("supershape_"+m+"_"+n1+"_"+n2+"_"+n3+"_"+a+"_"+b);
        var mf = go.AddComponent<MeshFilter> ();
#if UNITY_EDITOR
        mf.sharedMesh = mesh;
#else
        mf.mesh = mesh;
#endif

        return new List<GameObject> () {
            go
        };
    }

}
