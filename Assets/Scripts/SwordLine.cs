using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLine : MonoBehaviour
{
	float countdown = 0.2f;
	float attackPower;

	public void setAttackPower(float setPower)
	{
		attackPower = setPower;
	}

	void OnTriggerEnter(Collider col)
	{
		if (countdown >= 0.0f){
			if (col.tag == "Enemy")
			{
				//Debug.Log(col.gameObject.ToString());
				col.GetComponent<Enemy>().Damaged(attackPower);
			}
			countdown -= Time.deltaTime;
		}
	}
}
