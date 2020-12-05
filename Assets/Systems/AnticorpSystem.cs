using UnityEngine;
using FYFY;

public class AnticorpSystem : FSystem {
	private Family anticorp=FamilyManager.getFamily (new AllOfComponents(typeof (Lifetime)));
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
		foreach (GameObject go in anticorp){
			float speed = go.GetComponent<Move>().speed;
			GameObject target=go.GetComponent<Attack>().target;
			if(Vector3.Distance(go.transform.position, target.transform.position) > 0.001f){
				go.transform.position = Vector3.MoveTowards(go.transform.position, target.transform.position, speed);
			}
		}
	}
}