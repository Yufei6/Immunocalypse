using UnityEngine;
using FYFY;
using System.Collections.Generic;

public class MovingSystem : FSystem {
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.

	
	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameObject controller;

	//modification for adjusting animator
	private Family _controllableGO = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(Move),typeof(Nutrition)
			));
	//private Family _controllableGO = FamilyManager.getFamily(new AllOfComponents(typeof(Move)),new NoneOfComponents(typeof(Lifetime)));


	public MovingSystem(){
		controller = FamilyController.First();
	}
	//private int cpt = 0;

	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		
		foreach (GameObject go in _controllableGO){
			bool isMove = !go.GetComponent<Attack>().isAttacking;
			//get his own compteur for the targets
			int _cpt = go.GetComponent<Move>().cpt;
			GameObject g = go.GetComponent<Ani>().ani;
			int state = g.GetComponent<Animator>().GetInteger("State");
			Vector3 target = go.GetComponent<Move>().routine[_cpt];
			float speed = go.GetComponent<Move>().speed;
			//get the transform of the guidance and walks towards it
			Vector3 dir = target-go.transform.position;


			if(state==-1 || state==0 || state==3){
				if(dir.x>0){
					g.GetComponent<Animator>().SetInteger("State", 0);
				}else{
					g.GetComponent<Animator>().SetInteger("State", 3);
				}
			}

			if(isMove){

				//modification 01/09
				//get the target nearest

				if(Vector3.Distance(go.transform.position,target)>0.1f){
					go.transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);

				}else if(_cpt<go.GetComponent<Move>().routine.Count-1){
					//_cpt+=1;
					go.GetComponent<Move>().cpt+=1;
					//target = go.GetComponent<Move>().routine[_cpt];
					//Debug.Log(go.GetComponent<Move>().routine.Count);
					//Debug.Log(target);
				}else{
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("LoseScene");
					//Debug.Log("All targets reached");
				}



			}
		}
		
	}
}