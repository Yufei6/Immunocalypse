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
	
	private int current_state = 0;



	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
		current_state = 0;
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
		current_state = 0;
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		current_state = 0;
	}
}