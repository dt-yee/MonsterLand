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
        HandleEscape();
	}

    public void PickTower(TowerButton towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }
    public void BuyTower()
    {
        Hover.Instance.Deactivate();
        ClickedBtn = null;
    }

    private  void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
            this.ClickedBtn = null;
        }
    }
}
