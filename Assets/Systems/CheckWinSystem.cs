using UnityEngine;
using FYFY;

public class CheckWinSystem : FSystem {
	private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	
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
		GameObject tl=timeline.First();

		if(tl.GetComponent<TimeLine>().win_condtion <=0){
			int i=PlayerPrefs.GetInt("level");
			PlayerPrefs.SetInt("level",i+1);
			int d=i+1;
			string c="EnemyType"+d.ToString();
			PlayerPrefs.SetInt(c,0);
			if(i <2){
				GameObjectManager.loadScene("ContinueScene");
			}else{
				GameObjectManager.loadScene("WinScene");
			}
		}
		

	}
}