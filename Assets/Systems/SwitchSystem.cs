using UnityEngine;
using FYFY;

public class SwitchSystem : FSystem {
	private Family page = FamilyManager.getFamily(new AllOfComponents(typeof(Page)));
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	public SwitchSystem(){
		//test
		//PlayerPrefs.SetInt("Page":1);
	}
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		


	}
}