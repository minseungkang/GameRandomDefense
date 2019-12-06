using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform EnemySpawnPoint;

	public Transform Tier1TowerPrefab;
	public Transform Tier2TowerPrefab;
	public Transform Tier3TowerPrefab;
	public Transform TowerSpawnPoint;

	public float timeBetweenWaves = 10f;

	public Text waveCountdownText;
	public Text hpText;
	public Text moneyText;

	private int hp;
	private int money;
	private float countdown = 1f;

	private int waveNumber = 1;

	private int[] numOfEnemies = new int[10] {10, 10, 10, 10, 10, 10, 10, 10, 10, 10};


	void Start()
	{
		hp = 0;
		money = 0;
		HpChange(20);
		MoneyChange(40);
		GameObject.Find("GameoverUI").GetComponent<Canvas>().enabled = false;
	}

	void Update()
	{
		checkCountDown();
	}

	void checkCountDown()
	{
		if (countdown <= 0f)
		{
			// 남아있는 enemy가 몇 개인지 세고, 모든 enemy를 없앤다.
			GameObject[] remainedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
			int remainedEnemiesCount = remainedEnemies.Length;
			
			for (int i=0;i<remainedEnemiesCount;i++){
				Destroy(remainedEnemies[i]);
			}

			// 그만큼 체력을 깎는다.
			HpChange(-remainedEnemiesCount);

			// 체력이 0이하로 떨어지면 게임 오버가 되도록 한다.
			if (hp <= 0 )
			{
				GameObject.Find("GameoverUI").GetComponent<Canvas>().enabled = true;
				GameObject.Find("UI").GetComponent<Canvas>().enabled = false;
			}
			// 체력이 남아있다면 게임이 지속된다.
			else 
			{
				StartCoroutine(SpawnWave());	
				countdown = timeBetweenWaves;
			}
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

	public void Buy3TierTower()
	{
		if (money>=30)
		{
			MoneyChange(-30);
			Transform new_tower = Instantiate(Tier3TowerPrefab, TowerSpawnPoint.position, TowerSpawnPoint.rotation);
		}
		else
		{
			return;
		}
	}

}
