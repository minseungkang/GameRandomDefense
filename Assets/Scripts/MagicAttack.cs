using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
	float attackPower;

	public void setAttackPower(float setPower)
	{
		attackPower = setPower;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy")
		{
			//Debug.Log(col.gameObject.ToString());
			col.GetComponent<Enemy>().Damaged(attackPower);
			col.GetComponent<Enemy>().getSlowed(0.2f, 3f);
		}
	}
}
