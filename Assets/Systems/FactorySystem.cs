using UnityEngine;
using FYFY;
using System.Collections.Generic;
using System.Collections;

public class FactorySystem : FSystem {
	private Family factory=FamilyManager.getFamily (new AllOfComponents(typeof (ID)));
	private int frame_compteur=0;
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	private int enemy_compteur=0;
	private TimeLine tl;
	private Family alltype=FamilyManager.getFamily(new AllOfComponents(typeof(AllTypeEnemy)));
	private AllTypeEnemy te;
	private Vector3 v;
	private Routine r;
	private Family celluleB=FamilyManager.getFamily (new AllOfComponents(typeof (Anticorp)));
	private Family virus_bacterie = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(Move),typeof(Nutrition)
			));

	

	
	public FactorySystem(){
		tl=timeline.First().GetComponent<TimeLine>();
		te=alltype.First().GetComponent<AllTypeEnemy>();
		//test
		
	}
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
		tl=timeline.First().GetComponent<TimeLine>();
		te=alltype.First().GetComponent<AllTypeEnemy>();
		frame_compteur=frame_compteur+1;

		//Debug.Log(tl.i);
		if(frame_compteur==tl.frame[enemy_compteur]){
			int type=tl.type_enemy[enemy_compteur];
			int id=tl.id_fac[enemy_compteur];
			GameObject prefab;
			if(type ==1){
				prefab=te.virus;	
			}else{
				prefab=te.bac;
			}
			foreach(GameObject fac in factory ){
				if(id==fac.GetComponent<ID>().id){
					v=fac.transform.position;
					r=fac.GetComponent<Routine>();
				}
			}
			create_enemy(prefab,v,r);
			enemy_compteur=enemy_compteur+1;
		}
		foreach(GameObject go in celluleB){
			if (go.GetComponent<Anticorp>().cdcur>go.GetComponent<Anticorp>().cdfab){
				GameObject target=search_target(go.GetComponent<TowerId>().id);
				if(target!=null){
					create_anticorp(go.GetComponent<Anticorp>().at , go.transform.position,target);
					go.GetComponent<Anticorp>().cdcur=0;
				}
			}else{
				go.GetComponent<Anticorp>().cdcur +=1;
			}
		}
	}

	public void create_enemy(GameObject prefab,Vector3 v,Routine r){
		GameObject go=Object.Instantiate<GameObject>(prefab,v,Quaternion.identity);
		go.GetComponent<Move>().routine=r.routine;
		GameObjectManager.bind(go);
	}

	private void create_anticorp(GameObject at,Vector3 v,GameObject target){
		GameObject go=Object.Instantiate<GameObject>(at,v,Quaternion.identity);
		go.GetComponent<Attack>().target=target;
		GameObjectManager.bind(go);
	}

	private GameObject search_target(int id){
		foreach(GameObject go in virus_bacterie){
			if(go.GetComponent<Id_enemy>().id==id){
				return go;
			}
		}
		return null;
	}
}