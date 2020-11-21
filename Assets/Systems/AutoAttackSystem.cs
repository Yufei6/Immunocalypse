﻿using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;


public class AutoAttackSystem : FSystem {
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(
		typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	private Family virus_bacterie = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(Move),typeof(Nutrition)
			));
	private Family lym_T = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(TowerId)),
		new NoneOfComponents(typeof(Move)));
	private Family marco =FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack)),
		new NoneOfComponents(typeof(Move),typeof(TowerId)));
	private float c=1000f;
	private bool hastraget=false;
	private volatile bool hastraget2=false;
	private volatile bool hastraget3=false;
	private int hp=0;
	private float d;
	//private int bd;
	// private volatile GameObject t;
	// private volatile GameObject t2;
	// private volatile GameObject t3;



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
		foreach(GameObject vb in virus_bacterie){
			Triggered2D vbt= vb.GetComponent<Triggered2D>();
			if(vbt!=null){
				foreach(GameObject target in vbt.Targets){	
					if(target.CompareTag("def")||target.CompareTag("cellule")){	
						d=Vector2.Distance(vbt.transform.position, vb.transform.position);
						if (d<c){
							c=d;
							vb.GetComponent<Attack>().target=target;
							vb.GetComponent<Attack>().hastarget=true;
							change_is_move(vb);
						}
					}

				}
				
			}
			if(vb.GetComponent<Attack>().target==null){
						vb.GetComponent<Attack>().hastarget=false;
				}
			if(vb.GetComponent<Attack>().hastarget==false){
				change_is_move(vb);
			}
			if(vb.GetComponent<Attack>().target!=null){
				if(attack_cd(vb)){
					add_nutrition(vb);
					attack(vb);
				}
			}
		}		
		foreach(GameObject go in marco){
			Triggered2D got= go.GetComponent<Triggered2D> ();	
			if (got!=null){
				foreach(GameObject target in got.Targets){
					if(target.CompareTag("enemy")){
						d=Vector2.Distance(got.transform.position, go.transform.position);
						if (d<c){
							c=d;
							go.GetComponent<Attack>().target=target;
							go.GetComponent<Attack>().hastarget=true;
						}
					}
				}
				if(go.GetComponent<Attack>().target!=null){
					if(attack_cd(go)){
						attack2(go);
					}
				}
			}	
		}
		foreach(GameObject go in lym_T){
			Triggered2D got= go.GetComponent<Triggered2D> ();	
			if (got!=null){
				foreach(GameObject target in got.Targets){
					if(target.CompareTag("enemy")){
						d=Vector2.Distance(got.transform.position, go.transform.position);
						if (d<c){
							c=d;
							go.GetComponent<Attack>().target=target;
							go.GetComponent<Attack>().hastarget=true;
						}
					}
				}
				if(go.GetComponent<Attack>().target!=null){
					if(attack_cd(go)){
						attack3(go);
					}
				}
			}	
		}
	}
	private void attack3(GameObject att){
		GameObject target=att.GetComponent<Attack>().target;
		hp= target.GetComponent<HP>().hp;
		int bd= att.GetComponent<Attack>().baseDamage;
		TowerId idtower=att.GetComponent<TowerId>();
		if(idtower.id!=target.GetComponent<Id_enemy>().id){
			//Debug.Log(idtower.id);
			hp=hp-1;
		}else{
			hp=hp-bd;
		}
		if(hp<0){
			GameObject tl=timeline.First();
			tl.GetComponent<TimeLine>().win_condtion -=1;
			GameObjectManager.unbind(target);
			Object.DestroyImmediate(target);
		}else{
			//Debug.Log(hp);
			target.GetComponent<HP>().hp=hp;
		}
		hastraget3=false;
		c=1000f;
		
		
	}
	private void attack2(GameObject att){
		//Debug.Log(att);
		GameObject target=att.GetComponent<Attack>().target;
		hp= target.GetComponent<HP>().hp;
		int bd= att.GetComponent<Attack>().baseDamage;
		if(hp<0){
			GameObject tl=timeline.First();
			tl.GetComponent<TimeLine>().win_condtion -=1;
			GameObjectManager.unbind(target);
			Object.DestroyImmediate(target);
		}else{
			//Debug.Log(hp);
			target.GetComponent<HP>().hp=hp;
		}
		
		c=1000f;
		
	}
	private void attack(GameObject att){
		GameObject target=att.GetComponent<Attack>().target;
		hp= target.GetComponent<HP>().hp;
		int bd= att.GetComponent<Attack>().baseDamage;
		hp=hp-bd;
		if(hp<0){
			GameObjectManager.unbind(target);
			Object.DestroyImmediate(target);

		}else{
			//Debug.Log(hp);
			target.GetComponent<HP>().hp=hp;
		}
		//hastraget=false;
		c=1000f;
		
	}
	private void change_is_move(GameObject g){
		g.GetComponent<Attack>().isAttacking=g.GetComponent<Attack>().hastarget;

	}
	private bool attack_cd(GameObject v){
		float b=v.GetComponent<Attack>().startpoint;
		float a=v.GetComponent<Attack>().frequency;
		if(a<b){
			v.GetComponent<Attack>().startpoint=0;
			return true;
		}else{
			v.GetComponent<Attack>().startpoint=b+1;
			return false;
		}
	}
	private void add_nutrition(GameObject v){
		GameObject target=v.GetComponent<Attack>().target;
		if(target.CompareTag("cellule")){
			int actuelle=v.GetComponent<Nutrition>().nut_actuelle;
			int nut=min_v(v.GetComponent<Attack>().baseDamage,target.GetComponent<HP>().hp);
			v.GetComponent<Nutrition>().nut_actuelle=actuelle+nut;
		}

	}
	private int min_v(int i,int j){
		if (i<j){
			return i;
		}else{
			return j;
		}
	}
	
}