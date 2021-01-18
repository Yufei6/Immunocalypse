using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.CoreModule;
using System;
using FYFY;

public class RefreshSystem : FSystem {

	//get the family using refresh
	private Family _controllerR = FamilyManager.getFamily(new AllOfComponents(typeof(Amount)));

	private const float Increase = 1f;
	private float timer = 0f;
	private float _amount = 0f;

	private Family _cdFamily = FamilyManager.getFamily(new AllOfComponents(typeof(cdTower)));
	private int currentType = -1;
	private int cpt = 0;

	private Family _maskFamily = FamilyManager.getFamily(new AllOfComponents(typeof(Mask)));
	//private float cd_timer = 0f;



	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	public void SetDisabled(int towerType)
	{
		currentType = towerType;	
	}

	//modidication 11/21
	/*
	public RefreshSystem(){
		foreach (GameObject c in _cdFamily){
			cdTower cdt = c.GetComponent<cdTower>();
			string tower_id = cdt.Tower_id.ToString();
			string enemyCible = "Enemy"+tower_id;

			if(PlayerPrefs.HasKey(enemyCible)){
				Debug.Log("I have met this enemy");
				//c.GetComponent<Button>().interactable = (_amount>=cdt.ressource);
				cdt.timer = cdt.new_cd;
				//diminuer le temps
			}
		}

	}
	*/


	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		if(_controllerR!=null){
			//Debug.Log("Refresh found");
		}
		foreach (GameObject r in _controllerR){
			//for every 1 second, the money increases a certain amount
			timer += Time.deltaTime;
			if(timer>=1f){
				r.GetComponent<Amount>().amount += Increase;
				timer = 0f;
			}
			_amount = r.GetComponent<Amount>().amount;
			//Debug.Log(r.GetComponent<Amount>().amount);
		}


		foreach (GameObject c in _cdFamily){
			cdTower cdt = c.GetComponent<cdTower>();
			//string tower_id = cdt.Tower_id.ToString();
			//string enemyCible = "Enemy"+tower_id;
			string type = cdt.towerType;
			int id = cdt.id;

			//if(PlayerPrefs.HasKey(enemyCible)){
			if(PlayerPrefs.GetInt(type)==1){

				//Debug.Log("new_cd"+cdt.new_cd.ToString());
				c.GetComponent<Button>().interactable = (_amount>=cdt.ressource)&&(cdt.timer>=cdt.new_cd);
				cdt.timer += Time.deltaTime;
				foreach (GameObject go_mask in _maskFamily){
					if (go_mask.GetComponent<Mask>().type == id){
						Image img = go_mask.GetComponent<Image>();
						img.fillAmount = 1 - cdt.timer/cdt.new_cd;
					}
				}
			}
			else{
				c.GetComponent<Button>().interactable = (_amount>=cdt.ressource)&&(cdt.timer>=cdt.cd);
				cdt.timer += Time.deltaTime;
				foreach (GameObject go_mask in _maskFamily){
					if (go_mask.GetComponent<Mask>().type == id){
						Image img = go_mask.GetComponent<Image>();
						img.fillAmount = 1 - cdt.timer/cdt.cd;
					}
				}

			}
			
			
		}
		

	}
}