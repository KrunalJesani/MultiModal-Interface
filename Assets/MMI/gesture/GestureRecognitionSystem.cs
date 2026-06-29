using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace hci.mmi.gesture.GestureRecognitionSystem
{
    public class GestureRecognitionSystem : MonoBehaviour
    {
        [SerializeField]
        private float MinThreshold;

        [SerializeField]
        private GestureSimulator simulator;

        public event EventHandler<Gesture> OnGestureRecognized;

        private void OnEnable()
        {
            simulator.OnSimulatedSpeechInput += DoSimulatedGestureInput;
        }

        private void DoSimulatedGestureInput(string input)
        {
            OnGestureRecognized?.Invoke(this, new Gesture { name = input, id = 0, timestamp = Time.time, confidence = 0.9f });
        }

        public void OnGestureCompleted(GestureCompletionData data)
        {
            if (data.similarity >= MinThreshold)
            {
                OnGestureRecognized?.Invoke(this, new Gesture { name = data.gestureName, id = data.gestureID, timestamp = Time.time, confidence = (float)data.similarity });
                //Debug.Log("[GestureRecognitionSystem] Recognized Gesture: " + name);

            }
        }
    }

    public class Gesture : EventArgs
    {
        public string name;
        public int id;
        public float timestamp;
        public float confidence;

        
    }
}


