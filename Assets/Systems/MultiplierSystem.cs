﻿using UnityEngine;
using FYFY;

public class MultiplierSystem : FSystem {
	private Family nut=FamilyManager.getFamily(new AllOfComponents(typeof(Nutrition)));
	private int cible,actuelle;
	private Move mo;
	private Family alltype=FamilyManager.getFamily(new AllOfComponents(typeof(AllTypeEnemy)));
	private AllTypeEnemy te;
	private GameObject pf;
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	public MultiplierSystem(){
		te=alltype.First().GetComponent<AllTypeEnemy>();
		//test
		
	}
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach(GameObject nu in nut){
			cible=nu.GetComponent<Nutrition>().nut_cible;

			actuelle=nu.GetComponent<Nutrition>().nut_actuelle;
			//nu.GetComponent<Nutrition>().nut_actuelle=actuelle+1;
			if (cible<=actuelle){
				nu.GetComponent<Nutrition>().nut_actuelle=actuelle-cible;
				se_multi(nu);
			}
		}
	}
	public void se_multi(GameObject prefab){
		GameObject tl=timeline.First();
		tl.GetComponent<TimeLine>().win_condtion +=1;
		int type=prefab.GetComponent<Id_enemy>().id;
		if(type ==1){
				pf=te.virus;	
			}else{
				pf=te.bac;
			}
		GameObject go=Object.Instantiate<GameObject>(pf);
		go.transform.position=prefab.transform.position;
		go.GetComponent<Move>().speed=prefab.GetComponent<Move>().speed;
		go.GetComponent<Move>().isMove=prefab.GetComponent<Move>().isMove;
		go.GetComponent<Move>().routine=prefab.GetComponent<Move>().routine;
		go.GetComponent<Move>().cpt=prefab.GetComponent<Move>().cpt;
		GameObjectManager.bind(go);
	}
}