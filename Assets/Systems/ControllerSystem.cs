using UnityEngine;
using System.Collections;
using FYFY;

public class ControllerSystem : FSystem {
	public const int MAINMENU = 0;
	public const int PLAYING = 1;
	public const int PAUSE = 2;
	public const int EVENTCHOICE = 3;
	public const int OPTIONMENU = 4;
	public const int SHOWCOLLECTION = 5;
	public const int LOST = 6;
	public const int WIN = 7;
	public const int START = 8;
	public const int MANUEL = 9;
	public const int INTROLEVEL1 = 10;
	public const int INTROLEVEL2 = 11;
	

	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameState gs;
	private GameLevel gl;
	private bool stateChange;
	private int lastState = 0;
	private int lastLevel = 0;
	private GameObject controller;

	//private GameObject Manuel1 = GameObject.FindGameObjectWithTag("Manuel1")[0];
	private Family FamilyManuel1 = FamilyManager.getFamily (new AllOfComponents (typeof (Manuel1)));
	private Family FamilyManuel2 = FamilyManager.getFamily (new AllOfComponents (typeof (Manuel2)));


	public ControllerSystem()
	{
		gs = FamilyController.First().GetComponent<GameState>();
		gl = FamilyController.First().GetComponent<GameLevel>();
		stateChange = false;
		controller = FamilyController.First();
	}

	public void StartGame(int level=1)
	{
		gs.currentState = PLAYING;
		gl.currentLevel = level;
		stateChange = true;
	}

	public void StartIntro()
	{
		gs.currentState = START;
		stateChange = true;
	}

	public void Pauss()
	{
		gs.currentState = PAUSE;
		stateChange = true;
	}

	public void ShowCollection()
	{
		gs.currentState = SHOWCOLLECTION;
		stateChange = true;
	}

	public void Manuel()
	{
		gs.currentState = MANUEL;
		stateChange = true;
	}

	public void UpdateManuel(){
		foreach(GameObject m1 in FamilyManuel1){
			m1.SetActive(false);
		}

		foreach(GameObject m2 in FamilyManuel2){
			m2.SetActive(true);
		}
	}

	public void IntroLevel1()
	{
		gs.currentState = INTROLEVEL1;
		stateChange = true;
	}

	public void IntroLevel2()
	{
		gs.currentState = INTROLEVEL2;
		stateChange = true;
	}

	public void Quit()
	{
		Application.Quit();
	}




	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) 
	{
		// stateChange = lastState == gs.currentState ? false : true ;
		// if(stateChange){
		// 	Debug.Log(lastState+"CHANGE"+gs.currentState);
		// }
		if (stateChange && (lastState==PAUSE||lastState==EVENTCHOICE)) 
		{
			//Restart game
			Time.timeScale = 1;
		}
		lastState = gs.currentState;
		// Debug.Log("lastStat"+lastState);
		lastLevel = gl.currentLevel;
		switch (gs.currentState)
		{
			case MAINMENU:
				// Debug.Log("STATE0");
				if (stateChange){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("MainMenuScene");
				}
				break;
			case PLAYING:
				// Debug.Log("STATE1");
				if ((stateChange) && (lastState!=PAUSE) && (lastState!=EVENTCHOICE)){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("level"+gl.currentLevel.ToString());
				}
				break;
			case PAUSE:
				// Debug.Log("STATE2");
				Time.timeScale = 0;
				break;
			case EVENTCHOICE:
				// Debug.Log("STATE3");
				Time.timeScale = 0;
				break;
			case OPTIONMENU:
				// Debug.Log("STATE4");
				//TODO
				int i = 1;
				break;
			case SHOWCOLLECTION:
				// Debug.Log("STATE5");
				if (stateChange){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("collection");
				}
				break;
			case LOST:
				// Debug.Log("STATE6");
				if (stateChange){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("LostScene");
				}
				break;
			case WIN:
				// Debug.Log("STATE7");
				if (stateChange){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("WinScene");
				}
				break;
			case START:
				if ((stateChange) && (lastState!=PAUSE) && (lastState!=EVENTCHOICE)){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("IntroductionScene");
				}
				break;
			case MANUEL:
				if ((stateChange) && (lastState!=PAUSE) && (lastState!=EVENTCHOICE)){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("ManuelScene");
				}
				break;
			case INTROLEVEL1:
				if ((stateChange) && (lastState!=PAUSE) && (lastState!=EVENTCHOICE)){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("IntroLevel1");
				}
				break;
			case INTROLEVEL2:
				if ((stateChange) && (lastState!=PAUSE) && (lastState!=EVENTCHOICE)){
					GameObjectManager.dontDestroyOnLoadAndRebind(controller);
					GameObjectManager.loadScene("IntroLevel2");
				}
				break;
			default :
				Debug.Log("Error : Unknown STATE!");
				break;
		}
		stateChange = false;
	}
}