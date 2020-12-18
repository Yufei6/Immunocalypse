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

	public void ShowInformation(UnityEngine.GameObject go)
	{
		MainLoop.callAppropriateSystemMethod ("ControllableSystem", "ShowInformation", go);
	}

	public void ChangeCaseColor(UnityEngine.GameObject go)
	{
		MainLoop.callAppropriateSystemMethod ("ControllableSystem", "ChangeCaseColor", go);
	}

}
