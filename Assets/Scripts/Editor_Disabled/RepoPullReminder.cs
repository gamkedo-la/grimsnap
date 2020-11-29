using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class RepoPullReminder : EditorWindow
{
    static RepoPullReminder()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode) {
            EditorApplication.update += RunOnce;
        }
    }

    static void RunOnce(){
        int windowWidth = 250;
        int windowHeight = 100;

        RepoPullReminder window = ScriptableObject.CreateInstance<RepoPullReminder>();
        window.position = new Rect((Screen.width / 2) + (windowWidth / 2), Screen.height / 2 + (windowHeight / 2), windowWidth, windowHeight);
        window.ShowPopup();
        EditorApplication.update -= RunOnce;
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Did you remember to pull the latest commits from the Git repo?", EditorStyles.wordWrappedLabel);
        GUILayout.Space(30);
        if (GUILayout.Button("Yes")){
            this.Close();
        } 
    }
}