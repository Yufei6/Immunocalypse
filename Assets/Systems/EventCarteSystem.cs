using UnityEngine;
using FYFY;

public class EventCarteSystem : FSystem {
	public const int eventWashHand = 1;
	public const int eventSport = 2;
	public const int eventVaccinum = 3;

	private Family timelineF = FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine), typeof(TimelineEvent)));
	private Family canvasEvent1F = FamilyManager.getFamily(new AllOfComponents(typeof(CanvasEvent1)));
	private Family canvasEvent2F = FamilyManager.getFamily(new AllOfComponents(typeof(CanvasEvent2)));
	private Family canvasEvent3F = FamilyManager.getFamily(new AllOfComponents(typeof(CanvasEvent3)));
	
	private TimeLine tl ;
	private int frame_compteur;
	private int event_compteur;
	private GameObject ce1;
	private GameObject ce2;
	private GameObject ce3;

	public EventCarteSystem()
	{
		tl=timelineF.First().GetComponent<TimeLine>();
		frame_compteur = 0 ;
		frame_compteur = 0 ;
		ce1 = canvasEvent1F.First();
		ce2 = canvasEvent2F.First();
		ce3 = canvasEvent3F.First();
	}

	public void WashHand(int want)
	{
		if (want>0){
			int i = 11;
			// TODO
		}
		ce1.SetActive(false);
		Time.timeScale = 1;
	}

	public void DoSport(int want)
	{
		if (want>0){
			int i = 11;
			// TODO
		}
		ce2.SetActive(false);
		Time.timeScale = 1;
	}

	public void Vaccine(int want)
	{
		if (want>0){
			int i = 11;
			// TODO
		}
		ce3.SetActive(false);
		Time.timeScale = 1;
	}


	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		frame_compteur += 1;
		// Debug.Log("counter:"+frame_compteur+" frame:"+tl.frame[event_compteur]);

		if(frame_compteur==tl.frame[event_compteur]){
			// Debug.Log("Hello event");
			if(Random.value < 1)
			{				
				Time.timeScale = 0;
				int typeEvent = tl.type_event[event_compteur];
				switch (typeEvent)
				{
					case 1:
						ce1.SetActive(true);
						break;
					case 2:
						ce2.SetActive(true);
						break;
					case 3:
						ce2.SetActive(true);
						break;
					default:
						Debug.Log("Unknown Event(Yufei)");
						break;
				}
			}
			if (event_compteur != (tl.frame.Length-1))
			{
				event_compteur += 1;
			}
		}
	}

}