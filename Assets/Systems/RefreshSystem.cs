using UnityEngine;
using System;
using FYFY;

public class RefreshSystem : FSystem {

	//get the family using refresh
	private Family _controllerR = FamilyManager.getFamily(new AllOfComponents(typeof(Amount)));
	private const float Increase = 1f;
	private float timer = 0f;
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
		if(_controllerR!=null){
			//Debug.Log("Refresh found");
		}
		foreach (GameObject r in _controllerR){
			//for every 1 second, the money increases a certain amount
			timer += Time.deltaTime;
			if(timer>=1f){
				r.GetComponent<Amount>().amount += Increase;
				timer = 0f;
			}
			
			//Debug.Log(r.GetComponent<Amount>().amount);
		}

	}
}