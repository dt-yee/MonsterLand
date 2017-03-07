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

    //[SerializeField]
    //private GameObject[] waypoints;


    private int currency;

    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyText;
    [SerializeField]
    private GameObject waveBtn;

    private Tower selectedTower;

    List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }


    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

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

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
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

    public void StartWave()
    {
        wave++;
        waveTxt.text = string.Format("Wave : <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < wave*5; i++)
        {

            //depends on monster kinds
            int monsterIndex = Random.Range(0, 3);
            string type = string.Empty;
            switch (monsterIndex)
            {
                //monsters' names
                case 0:
                    type = "robot";
                    break;
                case 1:
                    type = "rhinoceros";
                    break;
                case 2:
                    type = "treeEvil";
                    break;
            }
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.GetComponent<SpriteRenderer>().sortingOrder = 10;
            monster.Spawn();
            activeMonsters.Add(monster);
            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);
        if (!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }


    public void SelectTower(Tower tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }

}
