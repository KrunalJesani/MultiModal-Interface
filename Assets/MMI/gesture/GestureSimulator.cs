using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hci.mmi.gesture.GestureRecognitionSystem
{
    public class GestureSimulator : MonoBehaviour
    {
        public string simulateGesture;

        public delegate void SimulateGestureInput(string input);
        public SimulateGestureInput OnSimulatedSpeechInput;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (simulateGesture.Contains(" "))
            {
                OnSimulatedSpeechInput?.Invoke(simulateGesture.Replace(" ", ""));
                simulateGesture = "";
            }
        }
    }
}
