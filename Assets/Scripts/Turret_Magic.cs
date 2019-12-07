using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Magic : Turret
{
	[Header("Attributes")]
	protected float fireCountdown = 0f;

	public GameObject MagicAttackPrefab;


    // Start is called before the first frame update
    void Start()
    {
    	init_turret();
    	setTurretPreference(Color.blue, 20f, 20f, 7f, 0.5f);
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

    	if (fireCountdown <= 0f)
        {
            Attack();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Attack()
    {
    	Vector3 magicAttackPosition = target.position;
    	magicAttackPosition.y = 2.5f;
    	GameObject magicAttackGO = (GameObject)Instantiate(MagicAttackPrefab, magicAttackPosition, target.rotation);
    	MagicAttack magicAttack = magicAttackGO.GetComponent<MagicAttack>();
    	magicAttack.setAttackPower(attackPower * skillMultiplier1 * skillMultiplier2 * skillMultiplier3);

    	Destroy(magicAttackGO, 0.2f);
    }	


}
