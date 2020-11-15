using UnityEngine;
using FYFY;

public class CheckWinSystem : FSystem {
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	
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
		GameObject tl=timeline.First();
		if(tl.GetComponent<TimeLine>().win_condtion <=0){
			GameObjectManager.loadScene("WinScene");
		}
	}
}