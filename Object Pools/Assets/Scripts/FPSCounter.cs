using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	//public getters and setters for FPSDisplay
	public int AverageFPS { get; private set; }
	public int HighestFPS { get; private set; }
	public int LowestFPS { get; private set; }

	//# of frames to sample when averaging
	public int frameRange; 

	int[] fpsBuffer;
	int fpsBufferIndex;
	
	// Update is called once per frame
	void Update () {

		//initialize array
		if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
			InitializeBuffer ();
		}

		UpdateBuffer ();
		CalculateFPS ();
	}

	void InitializeBuffer(){

		//ensure array size of at least 1
		//array size of 1 provides no benefit, as the point is to make the readout less twitchy
		if (frameRange <= 0) {
			frameRange = 1;
		}
			
		fpsBuffer = new int[frameRange];
		fpsBufferIndex = 0;
	}

	void UpdateBuffer(){

		//frame rate for one frame stored in array
		fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);

		//if end of array, return to beginning
		if (fpsBufferIndex >= frameRange) {
			fpsBufferIndex = 0;
		}
	}

	void CalculateFPS(){

		//initialize variables
		int sum = 0;
		int highest = 0;
		int lowest = int.MaxValue;

		//loop through all stored frame rates and calculate avg, high, and low
		for (int i = 0; i < frameRange; i++) {

			int fps = fpsBuffer [i];
			sum = sum + fps;

			if (fps > highest) {
				highest = fps;
			}

			if (fps < lowest) {
				lowest = fps;
			}
		}

		//assign output
		AverageFPS = sum / frameRange;
		HighestFPS = highest;
		LowestFPS = lowest;
	}
}
