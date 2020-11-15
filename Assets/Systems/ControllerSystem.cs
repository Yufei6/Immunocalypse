using UnityEngine;
using FYFY;

public class Controller : FSystem {
	public const int MAINMENU = 0;
	public const int PLAYING = 1;
	public const int PAUSE = 2;
	public const int EVENTCHOICE = 3;
	public const int OPTIONMENU = 4;
	public const int SHOWCOLLECTION = 5;
	public const int LOST = 6;
	public const int WIN = 7;
	public const int START = 8;
	

	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameState gs;
	private GameLevel gl;
	private bool stateChange;
	private int lastState;
	private int lastLevel;
	private GameObject controller;


	public Controller ()
	{
		gs = FamilyController.First().GetComponent<GameState>();
		gl = FamilyController.First().GetComponent<GameLevel>();
		lastState = 0;
		lastLevel = 1;
		stateChange = false;
		controller = FamilyController.First();
	}

	public void StartGame(int level=1)
	{
		gs.currentState = PLAYING;
		gl.currentLevel = level;
	}

	public void Pauss()
	{
		gs.currentState = PAUSE;
	}

	public void ShowCollection()
	{
		gs.currentState = SHOWCOLLECTION;
	}

	public void Quit()
	{
		Application.Quit();
	}




	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) 
	{
		stateChange = lastState == gs.currentState ? false : true ;
		if (stateChange && (lastState==PAUSE||lastState==EVENTCHOICE)) 
		{
			//Restart game
			Time.timeScale = 1;
		}
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
			// case 9:
			// 	Debug.Log("STATE9");
			// 	break;
			default :
				Debug.Log("Error : Unknown STATE!");
				break;
		}
		lastState = gs.currentState;
		lastLevel = gl.currentLevel;
	}
}