using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    //[SerializeField]
    //private GameObject towerPrefab;

    //public GameObject TowerPrefab
    //{
    //    get
    //    {
    //        return towerPrefab;
    //    }
    //}

    //private TowerButton clickedBtn;

    public TowerButton ClickedBtn
    {
        get;

        private set;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickTower(TowerButton towerBtn)
    {
        this.ClickedBtn = towerBtn;
    }
    public void BuyTower()
    {
        ClickedBtn = null;
    }
}
