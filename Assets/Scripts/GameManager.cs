using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	private int lives;
	private bool gameOver = false;

    [SerializeField]
    private Text currencyText;

	[SerializeField]
	private GameObject gameOverMenu;

	[SerializeField]
	private Text livesText;

    public ObjectPool Pool { get; set; }

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

	public int Lives
	{
		get {
			return lives;
		}
		set {
			this.lives = value;

			if (lives <= 0) {
				this.lives = 0;
				GameOver ();
			}

			livesText.text = lives.ToString ();

		}
	}

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start () {
		Lives = 1;
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
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
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
        yield return new WaitForSeconds(2.5f);
    }

	public void GameOver() {
		if (!gameOver) {
			gameOver = true;
			gameOverMenu.SetActive (true);
		}
	}

	public void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); 
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
