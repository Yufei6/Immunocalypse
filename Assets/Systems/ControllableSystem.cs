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
	private Family normalCellF = FamilyManager.getFamily(new AllOfComponents(typeof(HP), typeof(NormalCell)));
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
		int i = 1;
		// Debug.Log(go.transform+"Hello");

	}

	public Vector3 mousePos2worldPos(Vector3 mousePos)
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		// Debug.Log("Before"+mousePos+"After"+mouseWorldPos);
		Debug.Log("mousePos"+mouseWorldPos);
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
		GameObject go = getObjectClick(pos);
		if (go != null)
		{
			bool isMacrophage = go.GetComponent<HP>() == null ? false:true;
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
			if (tower != null){
				GameObjectManager.bind(tower);
				tower.transform.position = go.transform.position;
				tower = null;
			}
		}
		
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