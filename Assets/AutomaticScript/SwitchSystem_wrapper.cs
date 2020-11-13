using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class SwitchSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void next()
	{
		MainLoop.callAppropriateSystemMethod ("SwitchSystem", "next", null);
	}

	public void previous()
	{
		MainLoop.callAppropriateSystemMethod ("SwitchSystem", "previous", null);
	}

	public void back()
	{
		MainLoop.callAppropriateSystemMethod ("SwitchSystem", "back", null);
	}

}