using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(WaveBuilder))] todo come back to this later
public class WaveLexiconEditor : Editor
{
    public override void OnInspectorGUI(){
        WaveBuilder waveBuilder = (WaveBuilder)target;
        waveBuilder.transform.hideFlags = HideFlags.HideInInspector;

  
        //waveBuilder.EditorCreate();


        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.HelpBox ("'Sup'", MessageType.Info);
        EditorGUILayout.Space ();
}
}
