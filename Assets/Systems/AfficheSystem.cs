using UnityEngine;
using UnityEngine.UI;
using FYFY;

public class AfficheSystem : FSystem {
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function
	private Family amount_family = FamilyManager.getFamily(new AllOfComponents(typeof(Amount)));
	private Family hp_family = FamilyManager.getFamily(new AllOfComponents(typeof(HP)));

	//private float startAmount = 10f;

	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	/*
	void Awake(){
		//GetComponent<Text>().text = startAmount;
		foreach(GameObject a in amount_family){
			//float _amount = a.GetComponent<Amount>().amount;
			//a.GetComponent<Text>().text = startAmount.ToString();
		}
	}
	*/
	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach(GameObject a in amount_family){
			float _amount = a.GetComponent<Amount>().amount;
			a.GetComponent<Text>().text = _amount.ToString();
		}
		foreach(GameObject hp in hp_family){
			hp.GetComponent<HP>().barre.value= hp.GetComponent<HP>().hp;

		}

	}

}