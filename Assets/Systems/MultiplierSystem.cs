using UnityEngine;
using FYFY;

public class MultiplierSystem : FSystem {
	private Family nut=FamilyManager.getFamily(new AllOfComponents(typeof(Nutrition)));
	private int cible,actuelle;
	private Move mo;
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
		GameObject go=Object.Instantiate<GameObject>(prefab);
		GameObjectManager.bind(go);
	}
}