using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour {
    [SerializeField]
    private TileScript goal, start;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ClickTile();
	}

    private void ClickTile()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();
                if(tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        start.SpriteRenderer.color = new Color32(255, 132, 0, 255);
                        tmp = null;
                    }
                    else
                    {
                        if (goal == null)
                        {
                            goal = tmp;
                            goal.SpriteRenderer.color = new Color32(255, 0, 0, 255);
                        }
                    }
                    
                }
            }
        }
    }
}
