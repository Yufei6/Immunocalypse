using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class RefreshSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void SetDisabled(System.Int32 towerType)
	{
		MainLoop.callAppropriateSystemMethod ("RefreshSystem", "SetDisabled", towerType);
	}

}
