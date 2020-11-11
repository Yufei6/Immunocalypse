using UnityEngine;
using FYFY;

public class FactorySystem : FSystem {
	private Family factory=FamilyManager.getFamily(new AllOfComponents(typeof(ID)));
	private int frame_compteur=0;
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)));
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
		frame_compteur=frame_compteur+1;


	}
}