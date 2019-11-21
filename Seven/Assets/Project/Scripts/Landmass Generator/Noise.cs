using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float [,] GenerateNoiseMap(int mapWidth, int mapheight, int seed, float scale, int octaves, float persistence, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapheight];
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000,100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }


        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfheight = mapheight / 2f;

        for (int y = 0; y < mapheight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseheight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x- halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfheight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseheight = perlinValue * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                if (noiseheight > maxNoiseHeight)
                    maxNoiseHeight = noiseheight;
                else
                    if (noiseheight < minNoiseHeight)
                {
                    minNoiseHeight = noiseheight;
                }
                noiseMap[x, y] = noiseheight;
            }
        }

        for (int y = 0; y < mapheight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

                return noiseMap;
    }
}
