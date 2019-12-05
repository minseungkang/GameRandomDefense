using System.Collections;
using UnityEngine;


public class Turret : MonoBehaviour
{
    protected bool isSelected;
    protected bool isStopped;
    protected Camera maincam;

    protected Vector3 moveDirection;
    protected float moveSpeed;

	public Color hoverColor;

	protected Renderer rend;
	protected Color startColor;

	protected Transform target;

	[Header("Attributes")]

	public float range = 20f;
    protected float attackPower = 10f;

	[Header("Unity Setup Fields")]

	protected string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        /*
        maincam = Camera.main;
        isSelected = false;
        isStopped = true;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        GameObject body = transform.Find("RotatePart").gameObject.transform.Find("Tower_Body").gameObject;
        rend = body.GetComponent<Renderer>();
        startColor = rend.material.color;
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isSelected)
        {
            if (Input.GetMouseButtonUp(1))
            {
                MoveTurret(Input.mousePosition);
            }
        }

        if (isStopped)
        {
            if (target == null)
                return;
            else {
                rotateToTarget();
            }
            checkFireCountdown();
    
        }
        else
        {
            turretIsMoving();
        }
        */
    }

    public void init_turret()
    {
        maincam = Camera.main;
        isSelected = false;
        isStopped = true;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        GameObject body = transform.Find("RotatePart").gameObject.transform.Find("Tower_Body").gameObject;
        rend = body.GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void setTurretPreference(
        Color setColor,
        float setSpeed = 20f,
        float setRange = 20f,
        float setAttackPower = 10f
        )
    {
        moveSpeed = setSpeed;
        hoverColor = setColor;
        range = setRange;
        attackPower = setAttackPower;
    }


    public void MoveTurret(Vector3 mos){
        Ray cast = maincam.ScreenPointToRay(mos);

        RaycastHit hit;

        if(Physics.Raycast(cast, out hit)){
            //Debug.Log(hit.point.ToString());
            moveDirection = hit.point;
            moveDirection.y = 2.5f;
            isStopped = false;
        }
        isSelected = false;
        ChangeSelectStatus();
    }


    public void UpdateTarget()
    {
        if (isStopped)
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
        else {
            target = null;
        }
    }


    public void rotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    public void turretIsMoving()
    {
        Vector3 dir = moveDirection - transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            isStopped = true;
        }
        else
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnDrawGizmosSelected ()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnMouseDown()
    {
        isSelected = !isSelected;
        ChangeSelectStatus();
    }

    public void ChangeSelectStatus()
    {
        if (isSelected)
            rend.material.color = hoverColor;
        else
            rend.material.color = startColor;
    }

}
