using UnityEngine;

public class Skill : MonoBehaviour {
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
	public const int NoSkill = 0;
	public const int Explore = 1;
	// ... Need to add some skills in the futur...

	public int skillType;
}