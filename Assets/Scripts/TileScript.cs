using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get;private set; }

    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255,118,118,255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

    private Tower myTower;

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
        }
    }

    public SpriteRenderer SpriteRenderer
    {
        get;

        set;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        
        //LevelManager lm = GameObject.FindObjectOfType<LevelManager>();

        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        //Debug.Log(GridPosition.X + ", " + GridPosition.Y);
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty)
            {
                ColorTile(emptyColor);
            }
            if(!IsEmpty) {
                ColorTile(fullColor);
            }
            
            else if (Input.GetMouseButtonDown(0))
            {

                PlaceTower();
                GameManager.Instance.DeselectTower();
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButton(0))
        {
            if(myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        //Debug.Log("Tower place");


        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
        IsEmpty = false;
        ColorTile(Color.white);
        GameManager.Instance.BuyTower();
    }


    private void ColorTile(Color32 newColor)
    {
        SpriteRenderer.color = newColor;
    }
}
