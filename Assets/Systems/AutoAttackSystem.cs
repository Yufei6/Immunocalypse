using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;

public class AutoAttackSystem : FSystem {
	private Family virus_bacterie_anticorp = FamilyManager.getFamily(new AllOfComponents(typeof(Attack),typeof(Move)));
	private Family lym_T_macro = FamilyManager.getFamily(new AllOfComponents(typeof(Attack)),new NoneOfComponents(typeof(Move)));
	private float c=1000f;
	private bool hastraget=false;

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
			Triggered2D vbt= vb.GetComponent<Triggered2D> ();
			foreach(GameObject target in vbt){
				d=Vector2.Distance(vbt.transform.position, vb.transform.position);
				if (d<c){
					c=d;
					target=vbt;
					hastraget=true;
				}
			}
			if(hastraget==true){
				v_attack(target,vb);
			}
		}

		foreach(GameObject vb in lym_T_macro){
			Triggered2D vbt= vb.GetComponent<Triggered2D> ();
			foreach(GameObject target in vbt){
				d=Vector2.Distance(vbt.transform.position, vb.transform.position);
				if (d<c){
					c=d;
					target=vbt;
					hastraget=true;
				}
			}
			if(hastraget==true){
				attack(target,vb);
			}
		}
	}
	private void attack(GameObject target,GameObject att){
		hp= target.GetComponent<HP>().hp;
		bd= att.GetComponent<Attack>().baseDamage;
		hp=hp-bd;
		if(hp<0){
			GameObjectManager.unbind(target);
			Object.Destroy(targer);
		}
		target.GetComponent<HP>().hp=hp;
	}
	private void v_attack(GameObject target,GameObject att){
		hp=0;
	}
}