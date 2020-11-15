using UnityEngine;
using FYFY;

public class EventCarteSystem : FSystem {
	public const int eventWashHand = 1;
	public const int eventSport = 2;
	public const int eventVaccinum = 3;

	private Family timelineF = FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine), typeof(TimelineEvent)));
	private TimeLine tl ;
	private int frame_compteur;
	private int event_compteur;


	public EventCarteSystem()
	{
		tl=timelineF.First().GetComponent<TimeLine>();
		frame_compteur = 0 ;
		frame_compteur = 0 ;
	}

	public void WashHand(int want)
	{
		int i = 1;
	}


	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		frame_compteur += 1;
		// Debug.Log("counter:"+frame_compteur+" frame:"+tl.frame[event_compteur]);
		if(frame_compteur==tl.frame[event_compteur]){
			// Debug.Log("Hello event");
			if(Random.value < 1)
			{
				// Debug.Log("Hello event");
				Time.timeScale = 0;
			}
		}
	}

}