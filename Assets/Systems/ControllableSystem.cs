using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;

public class ControllableSystem : FSystem {
	public const int MACROPHAGE = 0;
	public const int IMPHOCYTET = 1;
	public const int IMPHOCYTEB = 2;

	public int currentTowerType;
	private Family pointerOverFamily = FamilyManager.getFamily (new AllOfComponents (typeof (PointerOver)));
	private Family towerFacFamily = FamilyManager.getFamily(new AllOfComponents(typeof(BuildTower)));
	private Family normalCellF = FamilyManager.getFamily(new AllOfComponents(typeof(HP)), new AllOfComponents(typeof(NormalCell)));
	private Family caseTowerF = FamilyManager.getFamily(new AllOfComponents(typeof(TypeCase)));
	private BuildTower towerFac;
	private GameObject tower;


	public ControllableSystem()
	{
		currentTowerType = -1;
		towerFac = towerFacFamily.First().GetComponent<BuildTower>();
	}


	public void SelectTower(int towerType)
	{
		currentTowerType = towerType;
	}

	public void ShowInformation(GameObject go)
	{
		Debug.Log(go.transform+"Hello");

	}

	public Vector3 mousePos2worldPos(Vector3 mousePos)
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		Debug.Log("Before"+mousePos+"After"+mouseWorldPos);
 		return mouseWorldPos;
	}

	private bool insideInObject(GameObject go, Vector3 pos)
	{
		float diffX = pos.x - go.transform.position.x;
		float diffY = pos.y - go.transform.position.y;
		diffX = diffX >= 0 ? diffX : -diffX;
		diffY = diffY >= 0 ? diffY : -diffX;
		if ((diffX>go.transform.localScale.x) || (diffY>go.transform.localScale.y))
		{
			return false;
		}
		return true;
	}

	private GameObject getObjectClick(Vector3 pos)
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

	public void BuildTower(Vector3 pos, int towerType)
	{
		switch(towerType)
		{
			case 0:
				tower = Object.Instantiate<GameObject>(towerFac.macrophage);
				break;
			case 1:
				tower = Object.Instantiate<GameObject>(towerFac.cellT);
				break;
			case 2:
				tower = Object.Instantiate<GameObject>(towerFac.cellB);
				break;
			default:
				Debug.Log("Unknow type of tower!(Yufei)");
				break;
		}
		GameObjectManager.bind(tower);
		tower.transform.position = pos;
		
	}
	

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		foreach (GameObject go in pointerOverFamily)
		{
			ShowInformation(go);
		} 
		if (currentTowerType > -1)
		{
			if (Input.GetMouseButton(0))
			{
				BuildTower(mousePos2worldPos(Input.mousePosition), currentTowerType);
				currentTowerType = -1;
			}

		}
	}
}