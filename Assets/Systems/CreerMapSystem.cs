using UnityEngine;
using FYFY;

public class CreerMapSystem : FSystem {
	private Family map = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Map),typeof(ObjetMap)
			));
	private ObjetMap objetmap;
	private Map map1;
	public CreerMapSystem(){
		objetmap=map.First().GetComponent<ObjetMap>();
		map1=map.First().GetComponent<Map>();
		for(int i=0;i<12;i++){
			Vector3 n0= new Vector3(-6+i,3,0);
			creerObjet(objetmap,map1.rout1[i],n0);
			Vector3 n1= new Vector3(-6+i,2,0);
			creerObjet(objetmap,map1.rout2[i],n1);
			Vector3 n2= new Vector3(-6+i,1,0);
			creerObjet(objetmap,map1.rout3[i],n2);
			Vector3 n3= new Vector3(-6+i,0,0);
			creerObjet(objetmap,map1.rout4[i],n3);
			Vector3 n4= new Vector3(-6+i,-1,0);
			creerObjet(objetmap,map1.rout5[i],n4);
			Vector3 n5= new Vector3(-6+i,-2,0);
			creerObjet(objetmap,map1.rout6[i],n5);
		}
	}
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
	}
	private void creerObjet(ObjetMap ob,int i,Vector3 p){
		if(i==0){
			GameObject go=Object.Instantiate<GameObject>(ob.caseenemy);
			go.transform.position=p;
			GameObjectManager.bind(go);
		}
		if(i==1){
			GameObject go=Object.Instantiate<GameObject>(ob.casebase);
			go.transform.position=p;
			GameObjectManager.bind(go);
		}
		if(i==2){
			GameObject go=Object.Instantiate<GameObject>(ob.casetower);
			go.transform.position=p;
			GameObjectManager.bind(go);
		}
		if(i==3){
			GameObject go=Object.Instantiate<GameObject>(ob.caseroute);
			go.transform.position=p;
			GameObjectManager.bind(go);
		}
		if(i==4){
			GameObject go=Object.Instantiate<GameObject>(ob.caseuseless);
			go.transform.position=p;
			GameObjectManager.bind(go);
		}
		

	}
}