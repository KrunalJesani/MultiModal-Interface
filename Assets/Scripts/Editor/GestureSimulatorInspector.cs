using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(hci.mmi.gesture.GestureRecognitionSystem.GestureSimulator))]
public class GestureSimulatorInspector : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Add a simple label
        myInspector.Add(new PropertyField(serializedObject.FindProperty("simulateGesture"), "Simulate Gesture"));

        // Return the finished inspector UI
        return myInspector;
    }
}
