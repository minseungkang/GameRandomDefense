using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform EnemySpawnPoint;

	public Transform Tier1TowerPrefab;
	public Transform Tier2TowerPrefab;
	public Transform TowerSpawnPoint;

	public float timeBetweenWaves = 50f;

	public Text waveCountdownText;
	public Text hpText;
	public Text moneyText;

	private int hp;
	private int money;
	private float countdown = 1f;

	private int waveNumber = 1;

	private int[] numOfEnemies = new int[10] {50, 50, 50, 50, 50, 50, 50, 50, 50, 50};


	void Start()
	{
		hp = 0;
		money = 0;
		HpChange(30);
		MoneyChange(40);
	}

	void Update()
	{
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;
		waveCountdownText.text = Mathf.Floor(countdown).ToString();
	}

	IEnumerator SpawnWave()
	{
		for(int i=0; i<numOfEnemies[waveNumber-1];i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.2f);
		}

		//Debug.Log("Wave Incoming!");
		waveNumber++;
	}

	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, EnemySpawnPoint.position, EnemySpawnPoint.rotation);
	}

	public void HpChange(int delta)
	{
		hp = hp + delta;
		hpText.text = hp.ToString();
	}

	public void MoneyChange(int delta)
	{
		money = money + delta;
		moneyText.text = money.ToString();
	}

	public void Buy1TierTower()
	{
		if (money>=10)
		{
			MoneyChange(-10);
			Transform new_towerT = Instantiate(Tier1TowerPrefab, TowerSpawnPoint.position, TowerSpawnPoint.rotation);
			
			Turret_gun new_tower = new_towerT.GetComponent<Turret_gun>();
			new_tower.test();
		}
		else
		{
			return;
		}
	}

	public void Buy2TierTower()
	{
		if (money>=20)
		{
			MoneyChange(-20);
			Transform new_tower = Instantiate(Tier2TowerPrefab, TowerSpawnPoint.position, TowerSpawnPoint.rotation);
		}
		else
		{
			return;
		}
	}

}
