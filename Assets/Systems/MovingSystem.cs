using UnityEngine;
using FYFY;
using System.Collections.Generic;

public class MovingSystem : FSystem {
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.

	private Family _controllableGO = FamilyManager.getFamily(new AllOfComponents(typeof(Move)));
	private int cpt = 0;

	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach (GameObject go in _controllableGO){
<<<<<<< Updated upstream
			bool isMove = !go.GetComponent<Attack>().isAttacking;
=======
			bool isMove = !go.GetComponent<Attack>().isAttack;
			
>>>>>>> Stashed changes
			if(isMove){
				//get the target nearest
				Debug.Log(cpt);
				Vector3 target = go.GetComponent<Move>().routine[cpt];
				float speed = go.GetComponent<Move>().speed;
				//get the transform of the guidance and walks towards it
				Vector3 dir = target-go.transform.position;

				if(Vector3.Distance(go.transform.position,target)>0.5f){
					go.transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);
				}else if(cpt<go.GetComponent<Move>().routine.Count-1){
					cpt+=1;
					target = go.GetComponent<Move>().routine[cpt];
					Debug.Log(go.GetComponent<Move>().routine[cpt]);
				}else{
					Debug.Log("All targets reached");
				}



			}
		}
		
	}
}