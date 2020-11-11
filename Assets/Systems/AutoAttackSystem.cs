﻿using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;

public class AutoAttackSystem : FSystem {
	private Family virus_bacterie_anticorp = FamilyManager.getFamily(new AllOfComponents(typeof(Attack),typeof(Move)));
	private Family lym_T_macro = FamilyManager.getFamily(new AllOfComponents(typeof(Attack)),new NoneOfComponents(typeof(Move)));
	private float c=1000f;
	private bool hastraget=false;
	private int hp=0;
	private float d;
	private int bd;
	private GameObject t;

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
		foreach(GameObject vb in virus_bacterie_anticorp){
			if(attack_cd(vb)){
				Triggered2D vbt= vb.GetComponent<Triggered2D> ();
				foreach(GameObject target in vbt.Targets){
					d=Vector2.Distance(vbt.transform.position, vb.transform.position);
					if (d<c){
						c=d;
						t=target;
						hastraget=true;
					}
				}
				change_is_move(vb);
				if(hastraget==true){
					attack(t,vb);
				}
			}
		}

		foreach(GameObject vb in lym_T_macro){
			if(attack_cd(vb)){
				Triggered2D vbt= vb.GetComponent<Triggered2D> ();
				foreach(GameObject target in vbt.Targets){
					d=Vector2.Distance(vbt.transform.position, vb.transform.position);
					if (d<c){
						c=d;
						t=target;
						hastraget=true;
					}
				}
				if(hastraget==true){
					attack(t,vb);
				}
			}
		}
	}
	private void attack(GameObject target,GameObject att){
		hp= target.GetComponent<HP>().hp;
		bd= att.GetComponent<Attack>().baseDamage;
		hp=hp-bd;
		if(hp<0){
			GameObjectManager.unbind(target);
			Object.Destroy(target);
		}else{
			target.GetComponent<HP>().hp=hp;
		}
		hastraget=false;
		
	}
	private void change_is_move(GameObject g){
		g.GetComponent<Attack>().isAttacking=hastraget;

	}
	private bool attack_cd(GameObject v){
		float b=v.GetComponent<Attack>().startpoint;
		float a=v.GetComponent<Attack>().frequency;
		if(a>b){
			v.GetComponent<Attack>().startpoint=0;
			return true;
		}else{
			v.GetComponent<Attack>().startpoint=b+1;
			return false;
		}
	}
	
}