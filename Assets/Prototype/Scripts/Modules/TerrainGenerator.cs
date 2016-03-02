using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BlurryRoots.Randomizer;

public class TerrainGenerator : ChainLink {

    public int resolution;

    public int width;
    public int height;
    public float zoom;

    [Range (1f, 100f)]
    public float elevation;
    [Range (1, 8)]
    public int lacunarity;
    [Range (0f, 1f)]
    public float persistence;

    [Range (0f, 1f)]
    public float seaLevel;
    [Range (0f, 1f)]
    public float mountainHeight = 0.618f;
    [Range (0f, 1f)]
    public float mountainPeakHeight = 0.8f;

    public override List<GameObject> Process (List<GameObject> input) {
        if (null == input || 0 == input.Count) {
            return input;
        }

        var offset = new UniformRandomNumberGenerator (Randomizer.GenerateSeed ()).Range (-1000, 1000);
        var model = NoiseUtility.Generate (width, height, zoom, lacunarity, persistence, seaLevel, mountainHeight, mountainPeakHeight, offset);
        var heightMap = model.heightMap;
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                heightMap[w, h] = Mathf.Floor (heightMap[w, h] * this.elevation);
            }
        }

        var result = new List<GameObject> (); 
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                if (0 == input.Count) {
                    break;
                }

                var go = input[0];
                input.RemoveAt (0);

                go.name = "world_block_" + w + "_" + h;

                var mr = go.GetComponent<MeshRenderer> ();
                var extends = mr.bounds.size;
                go.transform.position = new Vector3 (w * extends.x, heightMap[w, h], h * extends.z);

                var material = new Material (Shader.Find ("Standard"));
                material.color = model.texture.GetPixel (w, h);
                mr.sharedMaterial = material;

                result.Add (go);
            }
        }

        return result;
    }

}
