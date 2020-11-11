using UnityEngine;
using FYFY;

public class AutoAttackSystem : FSystem {
	private Family virus_bacterie_f = FamilyManager.getFamily(new AllOfComponents(typeof(Attack),typeof(Move),typeof(Nutrition)));
	private Family lym_T_f = FamilyManager.getFamily(new AllOfComponents(typeof(Attack)),new NoneOfComponents(typeof(Move)));
	private Family anticorp = FamilyManager.getFamily(new AllOfComponents(typeof(Attack),typeof(parent)));


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
		
	}
}