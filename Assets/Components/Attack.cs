using UnityEngine;

public class Attack : MonoBehaviour {
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
	public int baseDamage ;
	public float frequency ; 
	public float startpoint ;
	public bool isAttacking;
	public bool hastarget;
	public GameObject target;
	//test code
	//public bool isAttack = false;
}