﻿using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;

public class ControllableSystem : FSystem {
	public const int NOTHING = -2;
	public const int DESTROY = -1;
	public const int MACROPHAGE = 0;
	public const int IMPHOCYTET1 = 1;
	public const int IMPHOCYTET2 = 2;
	public const int IMPHOCYTET3 = 3;
	public const int IMPHOCYTET4 = 4;
	public const int IMPHOCYTEB1 = 5;
	public const int IMPHOCYTEB2 = 6;
	public const int IMPHOCYTEB3 = 7;
	public const int IMPHOCYTEB4 = 8;

	public int currentTowerType;
	private Family pointerOverCaseFamily = FamilyManager.getFamily (new AllOfComponents (typeof (PointerOver), typeof(cdTower)));
	private Family towerFacFamily = FamilyManager.getFamily(new AllOfComponents(typeof(BuildTower)));
	private Family normalCellF = FamilyManager.getFamily(new AllOfComponents(typeof(HP), typeof(HasTower)));
	private Family caseTowerF = FamilyManager.getFamily(new AllOfComponents(typeof(TypeCase), typeof(HasTower)));
	private Family towerF = FamilyManager.getFamily(new AllOfComponents(typeof(Attack), typeof(HP)),
		new NoneOfComponents(typeof(Move)));

	private Family cursorTypeF = FamilyManager.getFamily(new AllOfComponents(typeof(TypeCursor)));
	private Family buttonTowerF = FamilyManager.getFamily(new AllOfComponents(typeof(cdTower)));
	private Family ressourcesF = FamilyManager.getFamily(new AllOfComponents(typeof(Amount)));


	private BuildTower towerFac;
	private GameObject tower;
	private TypeCursor tc;
	private Texture2D CursorNormal;
	private Texture2D CursorM;
	private Texture2D CursorT1, CursorT2, CursorT3, CursorT4;
	private Texture2D CursorB1, CursorB2, CursorB3, CursorB4;
	private Texture2D CursorDestroy;
	private Amount amount;
	private GameObject buttonTower;
	private cdTower cdt;
	private int drapTower; 
	private bool changeCursor, drapCursor;


	public ControllableSystem()
	{
		currentTowerType = -2;
		towerFac = towerFacFamily.First().GetComponent<BuildTower>();
		tc = cursorTypeF.First().GetComponent<TypeCursor>();
		CursorNormal = tc.CursorNormal;
		CursorM = tc.CursorM;
		CursorT1 = tc.CursorT1;
		CursorT2 = tc.CursorT2;
		CursorT3 = tc.CursorT3;
		CursorT4 = tc.CursorT4;
		CursorB1 = tc.CursorB1;
		CursorB2 = tc.CursorB2;
		CursorB3 = tc.CursorB3;
		CursorB4 = tc.CursorB4;
		CursorDestroy = tc.CursorDestroy;
		amount = ressourcesF.First().GetComponent<Amount>();
		drapTower = -2;
		changeCursor = false;
		drapCursor = false;
	}


	public void SelectTower(int towerType)
	{
		currentTowerType = towerType;
		changeCursor = true;
	}



	public Vector3 mousePos2worldPos(Vector3 mousePos)
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		// Debug.Log("Before"+mousePos+"After"+mouseWorldPos);
		// Debug.Log("mousePos"+mouseWorldPos);
 		return mouseWorldPos;
	}

	private bool insideInObject(GameObject go, Vector3 pos)
	{
		float diffX = pos.x - go.transform.position.x;
		float diffY = pos.y - go.transform.position.y;
		diffX = diffX >= 0 ? diffX : -diffX;
		diffY = diffY >= 0 ? diffY : -diffY;
		if ((diffX > 0.5 * go.transform.localScale.x) || (diffY > 0.5f * go.transform.localScale.y))
		{
			return false;
		}
		return true;
	}

	private GameObject getCaseClick(Vector3 pos)
	{
		foreach (GameObject go in normalCellF)
		{
			if (insideInObject(go,pos))
			{
				return go;
			}
		}
		foreach (GameObject go in caseTowerF)
		{
			if (insideInObject(go,pos))
			{
				return go;
			}
		}
		return null;
	}
	
	private GameObject getTowerClick(Vector3 pos)
	{
		foreach (GameObject go in towerF)
		{
			if (insideInObject(go,pos))
			{
				return go;
			}
		}
		return null;
	}

	private GameObject getButtonTower(int towerType)
	{
		foreach (GameObject go in buttonTowerF)
		{
			if (go.GetComponent<cdTower>().id == towerType)
			{
				return go;
			}
		}
		return null;
	}

	private void BuildTower(Vector3 pos, int towerType)
	{
		GameObject go = getCaseClick(pos);
		if (go != null)
		{
			bool isMacrophage = go.GetComponent<HP>() == null ? false:true;
			if(!go.GetComponent<HasTower>().hasTower)
			{
				buttonTower = getButtonTower(towerType);
				cdt = buttonTower.GetComponent<cdTower>();
				switch(towerType)
				{
					case 0:
						if (isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.macrophage);
						}
						break;
					case 1:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellT1);
						}
						break;
					case 2:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellT2);
						}
						break;
					case 3:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellT3);
						}
						break;
					case 4:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellT4);
						}
						break;
					case 5:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellB1);
						}
						break;
					case 6:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellB2);
						}
						break;
					case 7:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellB3);
						}
						break;
					case 8:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellB4);
						}
						break;
					default:
						Debug.Log("Unknow type of tower!(Yufei)");
						break;
				}
			}
			if (tower != null){
				GameObjectManager.bind(tower);
				tower.transform.position = go.transform.position;
				tower.GetComponent<TowerCase>().towercase=go;
				go.GetComponent<HasTower>().hasTower = true; 
				cdt.timer = 0f;
				amount.amount -= cdt.ressource;
				tower = null;
			}
		}
		
	}

	private void DestroyTower(Vector3 pos){
		GameObject case1 = getCaseClick(pos);
		GameObject tower = getTowerClick(pos);
		if ((tower != null) && (case1 != null))
		{
			GameObjectManager.unbind(tower);
			Object.DestroyImmediate(tower);
			case1.GetComponent<HasTower>().hasTower = false; 
		}
	}

	private void UpdateCursor(int type)
	{
		switch (type)
		{
			case NOTHING:
				Cursor.SetCursor(CursorNormal, new Vector2(6, 6), CursorMode.Auto);
				break;
			case DESTROY:
				Cursor.SetCursor(CursorDestroy, new Vector2(10, 10), CursorMode.Auto);
				break;
			case MACROPHAGE:
				Cursor.SetCursor(CursorM, new Vector2(16, 16), CursorMode.Auto);
				break;
			case IMPHOCYTET1:
				Cursor.SetCursor(CursorT1, new Vector2(16, 16), CursorMode.Auto);
				break;
			case IMPHOCYTET2:
				Cursor.SetCursor(CursorT2, new Vector2(16, 16), CursorMode.Auto);
				break;	
			case IMPHOCYTET3:
				Cursor.SetCursor(CursorT3, new Vector2(16, 16), CursorMode.Auto);
				break;	
			case IMPHOCYTET4:
				Cursor.SetCursor(CursorT4, new Vector2(16, 16), CursorMode.Auto);
				break;		
			case IMPHOCYTEB1:
				Cursor.SetCursor(CursorB1, new Vector2(16, 16), CursorMode.Auto);
				break;
			case IMPHOCYTEB2:
				Cursor.SetCursor(CursorB2, new Vector2(16, 16), CursorMode.Auto);
				break;
			case IMPHOCYTEB3:
				Cursor.SetCursor(CursorB3, new Vector2(16, 16), CursorMode.Auto);
				break;
			case IMPHOCYTEB4:
				Cursor.SetCursor(CursorB4, new Vector2(16, 16), CursorMode.Auto);
				break;
			default:
				Debug.Log("Unknow CursorType!!(Yufei)");
				break;
		}
		changeCursor = false;
		drapCursor = false;
	}
	

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		if ((drapCursor) && !(changeCursor))
		{
			UpdateCursor(drapTower);
		}
		if (changeCursor)
		{
			UpdateCursor(currentTowerType);
		}
		foreach (GameObject go in pointerOverCaseFamily)
		{
			if (Input.GetMouseButtonDown(0)){
				cdTower cdt = go.GetComponent<cdTower>();
				if (cdt.timer>=cdt.cd){
					int type = cdt.id;
					drapTower = type;
					drapCursor = true;
				}
			}
		}
		if (currentTowerType>-2)
		{
			if (currentTowerType == -1){
				if (Input.GetMouseButton(0))
				{
					DestroyTower(mousePos2worldPos(Input.mousePosition));
					currentTowerType = -2;
					changeCursor = true;
				}
			} 
			else if (currentTowerType > -1)
			{
				if (Input.GetMouseButton(0))
				{
					BuildTower(mousePos2worldPos(Input.mousePosition), currentTowerType);
					//coldDown
					currentTowerType = -2;
					changeCursor = true;
				}
			}
		}
		else
		{
			if ((Input.GetMouseButtonUp(0))&&(drapTower>=0)){
				BuildTower(mousePos2worldPos(Input.mousePosition), drapTower);
				drapTower = -2;
				drapCursor = true;
			}
		}
		
	}
}