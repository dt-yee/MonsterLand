using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Monster target;

    private Tower parent;

    public Point GridPosition { set; get; }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveToTarget();
        if (gameObject.transform.position.Equals(parent.Target.transform.position))
        {
            if(target != null)
            {
                Transform healthBarTransform = parent.Target.transform.FindChild("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(parent.Damage);
                if(healthBar.currentHealth <= 0)
                {
                    //Destroy(parent.Target);
                    parent.Target.GetComponent<Monster>().Release();
                    GameManager.Instance.Currency += 5;
                }
            }
            //Destroy(gameObject);
            Release();
        }

	}

    private void Release()
    {
        gameObject.SetActive(false);
        //GridPosition = new parent.transform.position
    }

    public void Initialize(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }

    public void MoveToTarget()
    {
        if(target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);

            Vector2 dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
            
        }
    }
}
