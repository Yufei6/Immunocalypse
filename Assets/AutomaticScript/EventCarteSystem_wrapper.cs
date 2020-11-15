using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class EventCarteSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void WashHand(System.Int32 want)
	{
		MainLoop.callAppropriateSystemMethod ("EventCarteSystem", "WashHand", want);
	}

}