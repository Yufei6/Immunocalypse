using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class ControllableSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void SelectTower(System.Int32 towerType)
	{
		MainLoop.callAppropriateSystemMethod ("ControllableSystem", "SelectTower", towerType);
	}

}
