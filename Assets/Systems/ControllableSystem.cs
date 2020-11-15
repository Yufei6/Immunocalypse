using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;

public class ControllableSystem : FSystem {
	public const int NOTHING = -2;
	public const int DESTROY = -1;
	public const int MACROPHAGE = 0;
	public const int IMPHOCYTET = 1;
	public const int IMPHOCYTEB = 2;

	public int currentTowerType;
	private Family pointerOverCaseFamily = FamilyManager.getFamily (new AllOfComponents (typeof (PointerOver), typeof(HasTower)));
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
	private Texture2D CursorT;
	private Texture2D CursorB;
	private Texture2D CursorDestroy;
	private Amount amount;
	private GameObject buttonTower;
	private cdTower cdt;
	


	public ControllableSystem()
	{
		currentTowerType = -2;
		towerFac = towerFacFamily.First().GetComponent<BuildTower>();
		tc = cursorTypeF.First().GetComponent<TypeCursor>();
		CursorNormal = tc.CursorNormal;
		CursorM = tc.CursorM;
		CursorT = tc.CursorT;
		CursorB = tc.CursorB;
		CursorDestroy = tc.CursorDestroy;
		amount = ressourcesF.First().GetComponent<Amount>();
	}


	public void SelectTower(int towerType)
	{
		currentTowerType = towerType;
	}

	public void ShowInformation(GameObject go)
	{
		int i = 1;
		// Debug.Log(go.transform+"Hello");

	}

	public void ChangeCaseColor(GameObject go)
	{
		int i = 1;
		//TODO
		// go.GetComponent<MeshRenderer>().material.color = Color.green;
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
		// Debug.Log("OK, Object at"+go.transform.position+" ; your mouse at "+pos);
		// Debug.Log("Diff X="+diffX+" ; Diff Y= "+diffY);
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

	private GameObject getButtonTower()
	{
		foreach (GameObject go in buttonTowerF)
		{
			if (go.GetComponent<cdTower>().id == currentTowerType)
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
				buttonTower = getButtonTower();
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
							tower = Object.Instantiate<GameObject>(towerFac.cellT);
						}
						break;
					case 2:
						if (!isMacrophage)
						{
							tower = Object.Instantiate<GameObject>(towerFac.cellB);
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
			Object.Destroy(tower);
			case1.GetComponent<HasTower>().hasTower = false; 
		}
	}

	private void UpdateCursor()
	{
		switch (currentTowerType)
		{
			case NOTHING:
				Cursor.SetCursor(CursorNormal, Vector2.zero, CursorMode.Auto);
				break;
			case DESTROY:
				Cursor.SetCursor(CursorDestroy, Vector2.zero, CursorMode.Auto);
				break;
			case MACROPHAGE:
				Cursor.SetCursor(CursorM, Vector2.zero, CursorMode.Auto);
				break;
			case IMPHOCYTET:
				Cursor.SetCursor(CursorT, Vector2.zero, CursorMode.Auto);
				break;	
			case IMPHOCYTEB:
				Cursor.SetCursor(CursorB, Vector2.zero, CursorMode.Auto);
				break;
			default:
				Debug.Log("Unknow CursorType!!(Yufei)");
				break;
		}
	}
	

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		//UpdateCursor();
		foreach (GameObject go in pointerOverCaseFamily)
		{
			// Debug.Log("INNN");
			ChangeCaseColor(go);
			ShowInformation(go);
		}
		if (currentTowerType == -1){
			if (Input.GetMouseButton(0))
			{
				DestroyTower(mousePos2worldPos(Input.mousePosition));
				currentTowerType = -2;
			}
		} 
		else if (currentTowerType > -1)
		{
			if (Input.GetMouseButton(0))
			{
				BuildTower(mousePos2worldPos(Input.mousePosition), currentTowerType);
				//coldDown

				currentTowerType = -2;
			}
		}
	}
}