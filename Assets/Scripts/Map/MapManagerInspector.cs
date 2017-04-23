using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapManager))]
public class MapManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        MapManager mapManager = target as MapManager;
        List<SpriteSet> sets = mapManager.spriteSets;
        for(int i=0;i<sets.Count;i++)
        {
            sets[i].showInInspector = EditorGUILayout.Foldout(sets[i].showInInspector, sets[i].title);
            if (sets[i].showInInspector)
            {
                EditorGUI.indentLevel++;
                sets[i].title = EditorGUILayout.TextField("Title", sets[i].title);
                sets[i].sprites[0] = EditorGUILayout.ObjectField("Left", sets[i].sprites[0], typeof(Sprite)) as Sprite;
                sets[i].sprites[1] = EditorGUILayout.ObjectField("Mid", sets[i].sprites[1], typeof(Sprite)) as Sprite;
                sets[i].sprites[2] = EditorGUILayout.ObjectField("Right", sets[i].sprites[2], typeof(Sprite)) as Sprite;
                sets[i].sprites[3] = EditorGUILayout.ObjectField("Filler", sets[i].sprites[2], typeof(Sprite)) as Sprite;
                EditorGUI.indentLevel--;
            }
        }
        if (GUILayout.Button("Add Sprite Set"))
        {
            SpriteSet set = new SpriteSet()
            {
                sprites = new Sprite[4],
                title = "New Sprite Set"
            };
            mapManager.spriteSets.Add(set);
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(mapManager);
        }
    }
}
