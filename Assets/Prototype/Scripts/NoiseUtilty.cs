using UnityEngine;

public static class NoiseUtility {

    private static float CalcLayerFrequency (float zoom, int lacunarity, int layer) {
        // add one to avoid flat perlin values
        return (zoom + 1f) / Mathf.Pow (lacunarity, layer);
    }

    private static Color CalculateColor (float height, float waterLevel, float mountainLevel, float mountainPeakLevel) {
        if (height > mountainPeakLevel) {
            return Color.white;
        }
        else if (height > mountainLevel) {
            return Color.Lerp (Color.yellow, Color.grey, height);
        }
        else if (height > waterLevel) {
            return Color.Lerp (Color.green, Color.yellow, height);
        }
        else {
            return Color.Lerp (Color.black, Color.blue, height);
        }
    }

    public static TerrainHeightMap Generate (int width, int height, float zoom, int lacunarity, float persistence, float seaLevel, float mountainHeight, float mountainPeakHeight, int offset) {
        var m1 = NoiseUtility.GeneratePerlinNoiseMap (width, height, CalcLayerFrequency (zoom, lacunarity, 0), offset);
        var m2 = NoiseUtility.GeneratePerlinNoiseMap (width, height, CalcLayerFrequency (zoom, lacunarity, 1), offset);
        var m3 = NoiseUtility.GeneratePerlinNoiseMap (width, height, CalcLayerFrequency (zoom, lacunarity, 2), offset);

        var heightMap = new float[width, height];

        var min = 1f;
        var max = 0f;
        var waterLevel = seaLevel;
        var mountainLevel = (1f - waterLevel) * mountainHeight + waterLevel;
        var mountainPeakLevel = (1f - mountainLevel) * mountainPeakHeight + mountainLevel;
        var c = new Color[width * height];
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                var rought = m1[w, h] * Mathf.Pow (persistence, 0);
                var fine = m2[w, h] * Mathf.Pow (persistence, 1);
                var detail = m3[w, h] * Mathf.Pow (persistence, 2);

                var total = rought + fine + detail;
                heightMap[w, h] = total;

                // used later to normalize the values
                min = total < min ? total : min;
                max = total > max ? total : max;
            }
        }

        // TODO: THIS IS SHIT!!!!??
        // normalize the values and then elevate
        var intervall = max - min;
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                var value = heightMap[w, h];
                var normalized = (value - min) / intervall;

                heightMap[w, h] = normalized;
            }
        }

        // figure out the colors
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                var total = heightMap[w, h];
                var colorIndex = w + h * width;

                c[colorIndex] = CalculateColor (total, waterLevel, mountainLevel, mountainPeakLevel);
            }
        }

        var texture = new Texture2D (width, height);
        texture.SetPixels (c);
        texture.Apply ();

        return new TerrainHeightMap () {
            texture = texture,
            heightMap = heightMap
        };
    }

    public static TerrainHeightMap Generate (int width, int height, IRandomNumberGenerator rng) {
        var c = new Color[width * height];
        var heightMap = GenerateWhiteNoiseMap (width, height, rng);

        // figure out the colors
        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                var total = heightMap[w, h];
                var colorIndex = w + h * width;

                c[colorIndex] = Color.Lerp (Color.white, Color.black, total);
            }
        }

        var texture = new Texture2D (width, height);
        texture.SetPixels (c);
        texture.Apply ();

        return new TerrainHeightMap () {
            texture = texture,
            heightMap = heightMap
        };
    }

    public static float[,] GeneratePerlinNoiseMap (int width, int height, float zoom, int offset = 0) {
        var map = new float[width, height];

        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                var x = (float)(w + offset) / zoom;
                var y = (float)(h + offset) / zoom;

                map[w, h] = Mathf.PerlinNoise (x, y);
            }
        }

        return map;
    }

    public static float[,] GenerateWhiteNoiseMap (int width, int height, IRandomNumberGenerator rng) {
        var map = new float[width, height];

        for (var w = 0; w < width; ++w) {
            for (var h = 0; h < height; ++h) {
                map[w, h] = rng.Float ();
            }
        }

        return map;
    }

}