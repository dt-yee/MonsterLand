using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField]
    private GameObject[] tilePrefabs;
    [SerializeField]
    private CameraMovement cameraMovement;
    [SerializeField]
    private Transform map;

    private Point blueSpawn;
    private Point redSpawn;
    [SerializeField]
    private GameObject bluePortalPrefab;
    [SerializeField]
    private GameObject redPortalPrefab;

    public Dictionary<Point, TileScript> Tiles { set; get; }

    public Portal StartPortal { get; set; }

    private Stack<Node> path;

    public Stack<Node> Path
    {
        get
        {
            if(path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(path);
        }

    }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }

    }

    public Point BlueSpawn
    {
        get
        {
            return blueSpawn;
        }

    }

    // Use this for initialization
    void Start () {
        //Point p = new Point(2, 0);
        //Debug.Log(p.X);
        //TestValue(p);
        //Debug.Log(p.X);
        CreateLevel();
    }

    // Update is called once per frame
    void Update () {
		
	}

    

    //public void TestValue(Point p)
    //{
    //    p.X = 3;
    //}

    private void TestDictionary()
    {
        Dictionary<string, int> testDictionary = new Dictionary<string, int>();
        testDictionary.Add("age", 28);
        
    }



    private void CreateLevel()
    {
        //string[] mapData = new string[]
        //{
        //    "0000", "1111", "2222", "3333", "4444", "5555"
        //};
        Tiles = new Dictionary<Point, TileScript>();


        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for(int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for(int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(),y,x,worldStart);
            }
        }
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        //Debug.Log("camera");
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
        SpawnPortals();
    }

    private void PlaceTile(string tileType, int y, int x, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        //newTile.transform.position = new Vector3(worldStart.x + TileSize * x, worldStart.y - TileSize * y, 0);

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + TileSize * x, worldStart.y - TileSize * y, 0), map);

        //Tiles.Add(new Point(x,y), newTile);

        //return newTile.transform.position;
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);
        Vector2 b_xy = new Vector2(0,0);
        b_xy = Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition;
        Vector3 b_pos = new Vector3(b_xy.x, b_xy.y, Tiles[blueSpawn].GetComponent<TileScript>().transform.position.z - 1);
        //Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        GameObject tmp = (GameObject)Instantiate(bluePortalPrefab, b_pos, Quaternion.identity);
        StartPortal = tmp.GetComponent<Portal>();
        StartPortal.name = "StartPortal";

        redSpawn = new Point(11, 5);
        Vector2 r_xy = new Vector2(0, 0);
        r_xy = Tiles[redSpawn].GetComponent<TileScript>().WorldPosition;
        Vector3 r_pos = new Vector3(r_xy.x, r_xy.y, Tiles[redSpawn].GetComponent<TileScript>().transform.position.z - 1);
        Instantiate(redPortalPrefab, r_pos, Quaternion.identity);
    }

    public void GeneratePath()
    {
        path = new Stack<Node>();
        //Debug.Log(Tiles[new Point(0, 1)].transform.position);
        //Node n1 = new Node(Tiles[new Point(0, 1)]);
        //Debug.Log(n1.GridPosition.X);
        //Debug.Log(n1.GridPosition.Y);
        //path.Push(n1);
        
               
        path.Push(new Node(Tiles[new Point(1, 0)]));
        path.Push(new Node(Tiles[new Point(1, 5)]));
        path.Push(new Node(Tiles[new Point(3, 5)]));
        path.Push(new Node(Tiles[new Point(3, 0)]));
        path.Push(new Node(Tiles[new Point(5, 0)]));
        path.Push(new Node(Tiles[new Point(5, 5)]));
        path.Push(new Node(Tiles[new Point(11, 5)]));
    }
}
