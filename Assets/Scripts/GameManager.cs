using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int currency;

    [SerializeField]
    private Text currencyText;

    

    public TowerButton ClickedBtn
    {
        get;

        private set;
    }

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyText.text = value.ToString() + " <color=lime>$</color>";
        }
    }


    // Use this for initialization
    void Start () {
        Currency = 5;
	}
	
	// Update is called once per frame
	void Update () {
        HandleEscape();
	}

    public void PickTower(TowerButton towerBtn)
    {
        if (Currency >= towerBtn.Price) {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }
    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
            ClickedBtn = null;
        }

        
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
