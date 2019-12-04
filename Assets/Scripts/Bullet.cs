using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform target;

	public float speed = 50f;
	public GameObject impactEffect;

    private float bulletPower;

    // Turret.cs에서 접근하는 총알의 목표 설정
	public void Seek(Transform _target)
	{
		target = _target;
	}

    // Turret.cs에서 접근하는 총알의 세기 설정
    public void setBulletPower(float attackPower)
    {
        bulletPower = attackPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
        	Destroy(gameObject);
        	return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
        	HitTarget();
        	return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
    	GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
    	Destroy(effectIns, 2f);

        target.GetComponent<Enemy>().Damaged(bulletPower);
    	Destroy(gameObject);
    }
}
