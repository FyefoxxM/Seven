﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapgen = (MapGenerator)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            mapgen.GenerateMap();
        }
    }
}
