using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    //[SerializeField]
    //private GameObject[] waypoints;
    [SerializeField]
    private float speed;

    private Stack<Node> path;
    public Point GridPosition { set; get; }
    private Vector3 destination;

    public bool IsActive { get; set; }

    private void Update()
    {
        Move();
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.StartPortal.transform.position;
        StartCoroutine(Scale(new Vector3(0.1f,0.1f), new Vector3(1,1)));
        SetPath(LevelManager.Instance.Path);
    }


    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while(progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }
        transform.localScale = to;
        IsActive = true;
    }

    private void Move()
    {
        //Vector2 d2 = new Vector2(destination.x, destination.y);
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            //Debug.Log(Time.deltaTime);
            //Debug.Log(transform.position.x);
            //Debug.Log(transform.position.y);
            //Debug.Log(destination.x);
            //Debug.Log(destination.y);
            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if(newPath != null)
        {
            this.path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

	private void onTriggerEnter2D(Collider2D other) {
		if (other.tag == "EndPortal") {
			//StartCoroutine (Scale (new Vector3 (1, 1), new Vector3 (0.1f, 0.1f), true));

			GameManager.Instance.Lives--;
		}
	}
}
