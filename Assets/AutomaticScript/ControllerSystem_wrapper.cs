using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class ControllerSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void StartGame(System.Int32 level)
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "StartGame", level);
	}

	public void playoffmusic()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "playoffmusic", null);
	}

	public void StartIntro()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "StartIntro", null);
	}

	public void Pauss()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "Pauss", null);
	}

	public void ShowCollection()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "ShowCollection", null);
	}

	public void Manuel()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "Manuel", null);
	}

	public void UpdateManuel()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "UpdateManuel", null);
	}

	public void IntroLevel1()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "IntroLevel1", null);
	}

	public void IntroLevel2()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "IntroLevel2", null);
	}

	public void IntroLevel3()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "IntroLevel3", null);
	}

	public void IntroLevel4()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "IntroLevel4", null);
	}

	public void SelfMakeMap()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "SelfMakeMap", null);
	}

	public void Quit()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "Quit", null);
	}

	public void Continue()
	{
		MainLoop.callAppropriateSystemMethod ("ControllerSystem", "Continue", null);
	}

}
