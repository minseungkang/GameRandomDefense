using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_sword : Turret
{
	[Header("Attributes")]
	protected float fireCountdown = 0f;

	public GameObject SwordLinePrefab;
	public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
    	init_turret();
    	setTurretPreference(Color.blue, 20f, 10f, 15f, 1f);
    	// setTurretPreference( 선택색깔, 이동속도, 사거리, 공격력, 공격속도 )
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void checkFireCountdown()
    {
    	float dir = Vector3.Distance(target.position, transform.position);

    	// 근접공격 타워는 적 인식은 10f 바깥에 있는 것부터 하지만 공격 자체는 7f 안에 들어와야만 한다
    	if (fireCountdown <= 0f && dir <= 7f)
        {
            Attack();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Attack()
    {
    	Vector3 swordlinePosition = target.position;
    	swordlinePosition.y = 2.5f;
    	GameObject swordLineGO = (GameObject)Instantiate(SwordLinePrefab, swordlinePosition, firePoint.rotation);
    	SwordLine swordLine = swordLineGO.GetComponent<SwordLine>();
    	swordLine.setAttackPower(attackPower * skillMultiplier1 * skillMultiplier2 * skillMultiplier3);

    	Destroy(swordLineGO, 0.2f);
    }	


}
