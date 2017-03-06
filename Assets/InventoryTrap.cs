using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTrap : MonoBehaviour {

	public GameObject trapButtonPrefab;
	public GameObject trapButton;
	public GameObject panelObject;

	// Use this for initialization
	void Start () {
		Sprite t1 =  Resources.Load <Sprite>("evilRadio");
		Sprite[] ts = new Sprite[] {t1};
		trapButtonPrefab.GetComponent<Image>().sprite = ts[0];
		trapButton = (GameObject) Instantiate(trapButtonPrefab, transform.position, Quaternion.identity);
		trapButton.transform.parent = panelObject.transform;
		trapButton.transform.localPosition = new Vector3 (0, 260, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
