using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	private float speed;

	private float slow_rate;		// slow_rate는 백분율로 속도를 감소시키게 함.
	private float slow_duration;

	private Transform target;
	private int wavepointIndex = 0;

	public float hp;
	private int waveNum;

	public Slider hpbar;

	void Start()
	{
		target = Waypoints.points[0];
		hpbar = transform.Find("Canvas/Slider").GetComponent<Slider>();
		setHp(50f * waveNum);
		setSpeed(30f);
		resetSlow();
	}

	void Update()
	{
		float current_speed;
		if (slow_duration >= 0f) // 둔화가 적용중일 경우
		{
			current_speed = speed * (1 - slow_rate);
		}
		else
		{
			current_speed = speed;
		}

		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * current_speed * Time.deltaTime, Space.World);
	
		if (Vector3.Distance(transform.position, target.position) <= 0.3f)
		{
			GetNextWaypoint();
		}

		if (slow_rate > 0f){
			slow_duration -= Time.deltaTime;
			if (slow_duration <= 0f) // 둔화 지속시간이 종료되면
			{
				resetSlow();
			}
		}
	}

	public void setWaveNum(int num)
	{
		waveNum = num;
	}

	void GetNextWaypoint()
	{
		wavepointIndex++;
		if (wavepointIndex>=Waypoints.points.Length){
			wavepointIndex = 0;
		}
		target = Waypoints.points[wavepointIndex];
	}

	public void getSlowed(float rate, float duration)
	{
		if (slow_rate <= rate) // 새로운 둔화가 감소율이 더 세면 새로운 것으로 갱신하기 위함.
		{
			slow_rate = rate;
			slow_duration = duration;
		} 
		else // 새로운 둔화가 감소율이 더 약하면 감소율을 평균내고 지속시간을 긴 것으로 갱신한다.
		{
			slow_rate = (slow_rate + rate) / 2;
			slow_duration = (slow_duration > duration) ? slow_duration : duration;
		}
	}

	void resetSlow()
	{
		slow_rate = 0f;
		slow_duration = 0f;
	}

	public void setSpeed(float set_speed)
	{
		speed = set_speed;
	}

	public void setHp(float set_hp)
	{
		hp = set_hp;
		hpbar.minValue = 0;
		hpbar.maxValue = hp;
		hpbar.value = hp;
	}

	public void Damaged(float damage)
	{
		hp = hp - damage;
		hpbar.value = hp;
		if (hp <= 0f)
		{
			GameObject GM = GameObject.Find("GameMaster");
			GM.GetComponent<GameMaster>().MoneyChange(1);
			Destroy(gameObject);
		}
	}

}
