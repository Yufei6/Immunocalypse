using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;


public class AutoAttackSystem : FSystem {
	private Family virus_bacterie = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(Move),typeof(Nutrition)
			));
	private Family lym_T_macro = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack)),
		new NoneOfComponents(typeof(Move)));
	private Family anticorp = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Attack),typeof(Move)),
		new NoneOfComponents(typeof(Nutrition)));
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
		foreach(GameObject vb in virus_bacterie){
			
			Triggered2D vbt= vb.GetComponent<Triggered2D>();
			if (vbt!=null){
				foreach(GameObject target in vbt.Targets){	
					if(target.CompareTag("def")||target.CompareTag("cellule")){	
							d=Vector2.Distance(vbt.transform.position, vb.transform.position);
							if (d<c){
								c=d;
								t=target;
								hastraget=true;
								change_is_move(vb);
							}
					}
				}
			}
			if(hastraget==false){
				change_is_move(vb);
			}
			if(hastraget==true){
				if(attack_cd(vb)){
					add_nutrition(vb,t);
					attack(t,vb);
				}
			}


		}
		foreach(GameObject vb in anticorp){
			Triggered2D vbt= vb.GetComponent<Triggered2D> ();
			if (vbt!=null){
				foreach(GameObject target in vbt.Targets){
					if(target.CompareTag("enemy")){	
							d=Vector2.Distance(vbt.transform.position, vb.transform.position);
							if (d<c){
								c=d;
								t=target;
								hastraget=true;
								change_is_move(vb);
							}
						}
				}
			}
			if(hastraget==false){
				change_is_move(vb);
			}
			
			if(hastraget==true){
				if(attack_cd(vb)){
				attack(t,vb);
				}
			}
		}
		foreach(GameObject vb in lym_T_macro){
			Triggered2D vbt= vb.GetComponent<Triggered2D> ();
			foreach(GameObject target in vbt.Targets){
				if(target.CompareTag("enemy")){
					d=Vector2.Distance(vbt.transform.position, vb.transform.position);
					if (d<c){
						c=d;
						t=target;
						hastraget=true;
					}
				}
			}
			if(hastraget==true){
				if(attack_cd(vb)){
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
			//Debug.Log(hp);
			target.GetComponent<HP>().hp=hp;
		}
		hastraget=false;
		c=1000f;
		
	}
	private void change_is_move(GameObject g){
		g.GetComponent<Attack>().isAttacking=hastraget;

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
	private void add_nutrition(GameObject v,GameObject target){
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