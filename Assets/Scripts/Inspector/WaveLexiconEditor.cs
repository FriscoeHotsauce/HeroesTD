using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveBuilder))]
public class WaveLexiconEditor : Editor
{
    public override void OnInspectorGUI(){
        WaveBuilder waveBuilder = (WaveBuilder)target;
        waveBuilder.transform.hideFlags = HideFlags.HideInInspector;

  
        //waveBuilder.EditorCreate();


        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.HelpBox ("Eat a dick yo", MessageType.Info);
        EditorGUILayout.Space ();
}
}
