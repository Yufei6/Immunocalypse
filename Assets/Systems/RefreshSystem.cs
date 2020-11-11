using UnityEngine;
using System;
using FYFY;

public class RefreshSystem : FSystem {

	//get the family using refresh
	private Family _controllerR = FamilyManager.getFamily(new AllOfComponents(typeof(Amount)));
	private const float Increase = 5f;
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {

		foreach (GameObject r in _controllerR){
			r.GetComponent<Amount>().amount += (int)Math.Round(Increase*Time.deltaTime);
		}

	}
}