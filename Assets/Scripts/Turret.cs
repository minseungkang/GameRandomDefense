using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public Color hoverColor;

	private Renderer rend;
	private Color startColor;

	private Transform target;

	[Header("Attributes")]

	public float range = 20f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
    	InvokeRepeating("UpdateTarget", 0f, 0.1f);
    	GameObject body = transform.Find("RotatePart").gameObject.transform.Find("Tower_Body").gameObject;
    	rend = body.GetComponent<Renderer>();
    	startColor = rend.material.color;
    }



    void UpdateTarget()
    {
    	if (target==null){
    		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    		float shortestDistance = Mathf.Infinity;
    		GameObject nearestEnemy = null;
    		foreach (GameObject enemy in enemies)
    		{
    			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    			if (distanceToEnemy < shortestDistance)
    			{
    				shortestDistance = distanceToEnemy;
    				nearestEnemy = enemy;
    			}
    		}
	
	    	if (nearestEnemy != null && shortestDistance <= range) 
	    	{
	    		target = nearestEnemy.transform;
	    	}
	        else
	        {
	        	target = null;
        	}
    	}
    	else 
    	{
    		float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    		if (distanceToTarget > range)
    			target = null;
    	}
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        	return;
        else {
        	Vector3 dir = target.position - transform.position;
        	Quaternion lookRotation = Quaternion.LookRotation(dir);
        	Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        	partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (fireCountdown <= 0f)
        {
        	Shoot();
        	fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
    	GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    	Bullet bullet = bulletGO.GetComponent<Bullet>();

    	if (bullet != null)
    		bullet.Seek(target);
    }


    void OnDrawGizmosSelected ()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnMouseDown()
    {
    	Debug.Log("Mouse Enter");
    }

    void OnMouseExit()
    {
    	Debug.Log("Mouse Exit");
    }

}
