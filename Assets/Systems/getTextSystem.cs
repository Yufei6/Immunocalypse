using UnityEngine;
using FYFY;
using UnityEngine.UI;

public class getTextSystem : FSystem {
	private Family input1 = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Text1)
			));
	private InputField inputf;
	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameObject controller;
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	public getTextSystem(){
		controller = FamilyController.First();
		inputf=input1.First().GetComponent<InputField>();
	}
	protected override void onPause(int currentFrame) {
	}
	public void gettextclick(){
		PlayerPrefs.SetString("path",inputf.text);
		GameObjectManager.dontDestroyOnLoadAndRebind(controller);
		GameObjectManager.loadScene("sceneselfmake");
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
	}
}