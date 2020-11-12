using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class MultiplierSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void se_multi(UnityEngine.GameObject prefab)
	{
		MainLoop.callAppropriateSystemMethod ("MultiplierSystem", "se_multi", prefab);
	}

}