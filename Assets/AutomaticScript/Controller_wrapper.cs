using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class Controller_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void StartGame(System.Int32 level)
	{
		MainLoop.callAppropriateSystemMethod ("Controller", "StartGame", level);
	}

	public void Pauss()
	{
		MainLoop.callAppropriateSystemMethod ("Controller", "Pauss", null);
	}

}