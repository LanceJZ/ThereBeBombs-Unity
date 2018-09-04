using UnityEditor;

// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool IsLayerPrioritiesUnfolded = true; // store the UI state
    string PropertiesName = "layerPriorities"; //First litter must be lowercase.

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Serialize cameraRaycaster instance
        IsLayerPrioritiesUnfolded = EditorGUILayout.Foldout(IsLayerPrioritiesUnfolded, "Layer Priorities");

        if (IsLayerPrioritiesUnfolded)
        {
            EditorGUI.indentLevel++;
            BindArraySize();
            BindArrayElements();
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
    }

    void BindArraySize()
    {
        string layerSize = PropertiesName + ".Array.size";
        int currentArraySize = serializedObject.FindProperty(layerSize).intValue;
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);

        if (requiredArraySize != currentArraySize)
        {
            serializedObject.FindProperty(layerSize).intValue = requiredArraySize;
        }
    }

    void BindArrayElements()
    {
        string layerSize = PropertiesName + ".Array.size";
        int currentArraySize = serializedObject.FindProperty(layerSize).intValue;

        for (int i = 0; i < currentArraySize; i++)
        {
            string findprop = PropertiesName + ".Array.data[{0}]";
            SerializedProperty prop = serializedObject.FindProperty(string.Format(findprop, i));
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
        }
    }
}
