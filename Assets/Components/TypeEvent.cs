using UnityEngine;

public class Type : MonoBehaviour {
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
	public const int NoEvent = 0;
	public const int AskWashHand = 1;
	public const int UseAlcool = 2;
	public const int AskVaccin = 3;

	public int typeEvent;

}