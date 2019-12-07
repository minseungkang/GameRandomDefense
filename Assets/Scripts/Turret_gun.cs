using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_gun : Turret
{
	[Header("Attributes")]
	protected float fireCountdown = 0f;

	public GameObject bulletPrefab;
	public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
    	init_turret();
    	setTurretPreference(Color.blue, 20f, 20f, 10f, 2f);
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
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setBulletPower(attackPower * skillMultiplier1 * skillMultiplier2 * skillMultiplier3);

        if (bullet != null)
            bullet.Seek(target);
    }

}
