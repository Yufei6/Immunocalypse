using UnityEngine;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).

	public float speed = 3.0f;

	public bool isMove = true;

	public List<Vector3> routine;

	/* //test code
	void Awake(){
		routine = new List<Vector3>();
		routine.Add(new Vector3(-3,0,0));
		routine.Add(new Vector3(3,0,0));
	}
	*/

}