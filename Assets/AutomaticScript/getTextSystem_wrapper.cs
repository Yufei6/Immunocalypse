using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class getTextSystem_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void gettextclick()
	{
		MainLoop.callAppropriateSystemMethod ("getTextSystem", "gettextclick", null);
	}

}
