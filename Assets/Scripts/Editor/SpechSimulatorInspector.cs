using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(hci.mmi.speech.SpeechRecognitionSystem.SpeechSimulator))]
public class SpeechSimulatorInspector : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Add a simple label
        myInspector.Add(new PropertyField(serializedObject.FindProperty("simulateSpeech"), "Simulate Speech"));

        // Return the finished inspector UI
        return myInspector;
    }
}
