using UnityEngine;
using FYFY;
using System.Collections.Generic;

public class MovingSystem : FSystem {
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.

	private Family _controllableGO = FamilyManager.getFamily(new AllOfComponents(typeof(Move)));

	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach (GameObject go in _controllableGO){
			bool isMove = !go.GetComponent<Attack>().isAttack;
			if(isMove){
				//get the target nearest
				Vector3 target = go.GetComponent<Move>().routine[0];
				float speed = go.GetComponent<Move>().speed;
				//get the transform of the guidance and walks towards it
				Vector3 dir = target-go.transform.position;

				if(Vector3.Distance(go.transform.position,target)>0){
					go.transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);
				}else{
					//acheive the target, change to the next one
					go.GetComponent<Move>().routine.RemoveAt(0);
				}



			}
			//else: when the collider works, it stops walkings
		}
		
	}
}