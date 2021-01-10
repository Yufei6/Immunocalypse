using UnityEngine;
using FYFY;
using System.Collections.Generic;

public class CheckWinSystem : FSystem {

	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameObject controller;
	public CheckWinSystem(){
		controller = FamilyController.First();
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		GameObject tl=timeline.First();

		if(tl.GetComponent<TimeLine>().win_condtion <=0){
			int i=PlayerPrefs.GetInt("level");
			PlayerPrefs.SetInt("level",i+1);
			int d=i+1;
			var namevirus = new List<string>()
                    {
                        "tetanus",
                        "bordetella",
                        "poliovirus",
                        "covid19"                    
                    };
			string c=namevirus[i-1];
			PlayerPrefs.SetInt(c,1);
			if(i < 3){
				GameObjectManager.dontDestroyOnLoadAndRebind(controller);
				GameObjectManager.loadScene("ContinueScene"+d);
			}else{
				GameObjectManager.dontDestroyOnLoadAndRebind(controller);
				GameObjectManager.loadScene("WinScene");
			}
		}
		

	}
}