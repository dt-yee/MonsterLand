using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTower : MonoBehaviour {

	public GameObject towerButtonPrefab;
	public GameObject panelObject;
	private int p = 260;

	// Use this for initialization
	void Start () {
		Sprite t1 =  Resources.Load <Sprite>("cannonIcon");
		Sprite t2 =  Resources.Load <Sprite>("flameThrowerIcon"); 
		Sprite t3 =  Resources.Load <Sprite>("teslaIcon"); 
		Sprite t4 =  Resources.Load <Sprite>("BowlingIcon"); 
		Sprite[] ts = new Sprite[] {t1, t2, t3, t4 };

		for (int i = 0; i < 4; i++) {
			GameObject towerButton;
			towerButtonPrefab.GetComponent<Image>().sprite = ts[i];
			towerButton = (GameObject) Instantiate(towerButtonPrefab, transform.position, Quaternion.identity);
			towerButton.transform.parent = panelObject.transform;
			towerButton.transform.localPosition = new Vector3 (0, p, 0);
			p -= 50;

		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
