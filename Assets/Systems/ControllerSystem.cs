using UnityEngine;
using FYFY;
using UnityEngine.SceneManagement;

public class Controller : FSystem {
	public const int MAINMENU = 0;
	public const int PLAYING = 1;
	public const int PAUSE = 2;
	public const int EVENTCHOICE = 3;
	public const int OPTIONMENU = 4;
	public const int SHOWCOLLECTION = 5;
	public const int LOST = 6;
	public const int WIN = 7;
	

	private Family FamilyController = FamilyManager.getFamily (new AllOfComponents (typeof (GameState)));
	private GameState gs;
	private GameLevel gl;
	private bool stateChange;
	private int currentState;
	private int currentLevel;

	public Controller ()
	{
		gs = FamilyController.First().GetComponent<GameState>();
		gl = FamilyController.First().GetComponent<GameLevel>();
		currentState = 0;
		currentLevel = 1;
		stateChange = false;

	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) 
	{
		stateChange = currentState == gs.currentState ? false : true ;
		currentState = gs.currentState;
		currentLevel = gl.currentLevel;
		switch (currentState)
		{
			case 0:
				Debug.Log("STATE0");
				if (stateChange){
					Debug.Log("Hello");
				}

				break;
			case 1:
				Debug.Log("STATE1");
				break;
			case 2:
				Debug.Log("STATE2");
				break;
			case 3:
				Debug.Log("STATE3");
				break;
			case 4:
				Debug.Log("STATE4");
				break;
			case 5:
				Debug.Log("STATE5");
				break;
			case 6:
				Debug.Log("STATE6");
				break;
			case 7:
				Debug.Log("STATE7");
				break;
			case 8:
				Debug.Log("STATE8");
				break;
			case 9:
				Debug.Log("STATE9");
				break;
			default :
				Debug.Log("Error : Unknown STATE!");
				break;
			
		}
	}
}