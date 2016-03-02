using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using BlurryRoots.Common;

public class CylindricalMeshFromPlanesGenerator : ChainLink {

    public Material material;

    public override List<GameObject> Process (List<GameObject> input) {
        if (null == input || 0 == input.Count) {
            Debug.LogWarning (this + " bypassed, no input!");
            return input;
        }

        var totalVertices = new List<Vector3> ();
        var totalTriangles = new List<int> ();
        var vertexCount = 0;
        for (var goi = 0; goi < input.Count; ++goi) {
            var mesh = GetMesh (input[goi].GetComponent<MeshFilter> ());

            if (0 == vertexCount) {
                vertexCount = mesh.vertexCount;
            }
            Assert.AreEqual (vertexCount, mesh.vertexCount, "Unequal vertex count!");

            var vertices = mesh.vertices;
            for (var vi = 0; vi < mesh.vertexCount; ++vi) {
                var vertex = input[goi].transform.TransformPoint (vertices[vi]);

                totalVertices.Add (vertex);
            }
        }

        for (var goi = 0; goi < input.Count - 1; ++goi) {
            var baseViGoi1 = goi * vertexCount;
            var baseViGoi2 = (goi + 1) * vertexCount;

            for (var vi = 0; vi < vertexCount; ++vi) {
                var vi1 = vi;
                var vi2 = vi == vertexCount - 1 ? 0 : vi1 + 1;

                var a = baseViGoi1 + vi1;
                var b = baseViGoi1 + vi2;
                var c = baseViGoi2 + vi1;
                var d = baseViGoi2 + vi2;

                totalTriangles.Add (b);
                totalTriangles.Add (c);
                totalTriangles.Add (a);

                totalTriangles.Add (d);
                totalTriangles.Add (c);
                totalTriangles.Add (b);
            }
        }

        // triangulate front/first shape face
        for (var i = 0; i < (vertexCount - 2); ++i) {
            totalTriangles.Add (i + 1);
            totalTriangles.Add (0);
            totalTriangles.Add (i + 2);
        }
        // triangulate back/last shape face
        for (var i = 0; i < (vertexCount - 2); ++i) {
            var offset = totalVertices.Count - vertexCount;
            totalTriangles.Add (offset + i + 2);
            totalTriangles.Add (offset + 0);
            totalTriangles.Add (offset + i + 1);
        }

        var totalMesh = new Mesh ();
        totalMesh.vertices = totalVertices.ToArray ();
        totalMesh.triangles = totalTriangles.ToArray ();
        totalMesh.RecalculateNormals ();

        var go = new GameObject ("tube");
        SetMesh (
            go.AddComponent<MeshFilter> (),
            totalMesh
        );
        SetMaterial (
            go.AddComponent<MeshRenderer> (),
            this.material
        );

        for (var i = 0; i < input.Count; ++i) {
            Destroyer.Destroy (input[i]);
        }

        return new List<GameObject> () {
            go
        };
    }

    private static Mesh GetMesh (MeshFilter meshFilter) {
#if UNITY_EDITOR
        var mesh = meshFilter.sharedMesh;
#else
        var mesh = meshFilter.mesh;
#endif 
        return mesh;
    }

    private static void SetMesh (MeshFilter meshFilter, Mesh mesh) {
#if UNITY_EDITOR
        meshFilter.sharedMesh = mesh;
#else
        meshFilter.mesh = mesh;
#endif   
    }

    private static void SetMaterial (Renderer r, Material m) {
#if UNITY_EDITOR
        r.sharedMaterial = m;
#else
        r.material = m;
#endif   

    }

}
