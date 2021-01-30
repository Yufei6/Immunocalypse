using UnityEngine;
using FYFY;

public class SwitchSystem : FSystem {
	private Family page = FamilyManager.getFamily(new AllOfComponents(typeof(Page)));
	private int id_cam=0;
	private int cam_max;
	private Family cam = FamilyManager.getFamily(new AllOfComponents(typeof(Id_camera)));
	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameObject controller;
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.

	public SwitchSystem(){
		controller = FamilyController.First();
		cam_max=0;
		foreach(GameObject c in cam){
			if(c.GetComponent<Id_camera>().id_c==id_cam){
				c.SetActive(true);
			}
			else{
				c.SetActive(false);
			}
			cam_max=cam_max+1;
		}
		//test
		//PlayerPrefs.SetInt("poliovirus", 1);
		//PlayerPrefs.SetInt("bordetella", 1);
		//PlayerPrefs.SetInt("clostridiumTetani", 1);
		//PlayerPrefs.SetInt("covid19",1);
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach(GameObject c in cam){
			if(c.GetComponent<Id_camera>().id_c==id_cam){
				c.SetActive(true);
				c.GetComponent<Id_camera>().block.SetActive(
					PlayerPrefs.GetInt(c.GetComponent<Id_camera>().virusname)==0);
				Debug.Log(PlayerPrefs.GetInt(c.GetComponent<Id_camera>().virusname));
			}
			else{
				c.SetActive(false);
			}
		}
	}
	public void next(){
		id_cam=id_cam+1;
		if (id_cam >= cam_max){
			id_cam=id_cam - cam_max;
		}
	}
	public void previous(){
		id_cam=id_cam-1;
		if(id_cam < 0){
			id_cam=cam_max+id_cam;
		}
	}
	public void back(){
		GameObjectManager.dontDestroyOnLoadAndRebind(controller);
		GameObjectManager.loadScene("MainMenuScene");
	}
		
}