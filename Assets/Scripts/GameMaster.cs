using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform EnemySpawnPoint;

	public Transform Tier1TowerPrefab;
	public Transform Tier2TowerPrefab;
	public Transform Tier3TowerPrefab;
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
    private int[] costs = new int[3] {10, 20, 30};
    public GachaManager gachaManager;
    private Dictionary<string, Transform> towerDictByType = new Dictionary<string, Transform>();


	void Start()
	{
		hp = 0;
		money = 0;
		HpChange(20);
		MoneyChange(40);
		GameObject.Find("GameoverUI").GetComponent<Canvas>().enabled = false;
        towerDictByType.Add(TowerAttributes.Types.Gun.ToString(), Tier1TowerPrefab);
        towerDictByType.Add(TowerAttributes.Types.Sword.ToString(), Tier2TowerPrefab);
        towerDictByType.Add(TowerAttributes.Types.Magic.ToString(), Tier3TowerPrefab);
	}

	void Update()
	{
		checkCountDown();
        //if (Input.GetMouseButtonDown(0))
        //    Debug.Log("Name: " + EventSystem.current.currentSelectedGameObject.name);
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
		Enemy enemy = Instantiate(enemyPrefab, EnemySpawnPoint.position, EnemySpawnPoint.rotation).gameObject.GetComponent<Enemy>();
		enemy.setWaveNum(waveNumber);
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

    public void UseSkillSteam()
    {
        if (money >= 25)
        {
            MoneyChange(-25);
            GetComponent<SteamStart>().UseSteam();
        }
    }

    public void UseSkillOverClock()
    {
        if (money >= 15)
        {
            MoneyChange(-15);
            GetComponent<Skill>().UseOverclock(GameObject.FindGameObjectsWithTag("Turret"));
        }
    }

    public void UseSkillOverworked()
    {
        if (hp > 10)
        {
            HpChange(-10);
            MoneyChange(10);
        }
    }

    public void UseSkillRedbull()
    {
        if (hp > 5)
        {
            HpChange(-5);
            GetComponent<Skill>().UseRedbull(GameObject.FindGameObjectsWithTag("Turret"));
        }
    }

    public void UseSkillChilldown()
    {
        HpChange(5);
        GetComponent<Skill>().UseChilldown(GameObject.FindGameObjectsWithTag("Turret"));
    }

    public void GachaTowerRank(int rank)
    {
        // rank range validation
        if (rank > TowerAttributes.MaxRank || rank < TowerAttributes.MinRank)
        {
            return;
        }

        // cost check
        if (money < costs[rank - 1])
        {
            return;
        }

        MoneyChange(-1 * costs[rank - 1]);

        var turretInfoDict = gachaManager.gacha(rank);
        Turret turret = Instantiate(towerDictByType[turretInfoDict["TowerType"]], TowerSpawnPoint.position, TowerSpawnPoint.rotation).gameObject.GetComponent<Turret>();
        turret.setTurretPreference(Color.blue, 20f, 20f, Convert.ToSingle(turretInfoDict["Atk"]), 1f);
        Debug.Log(turretInfoDict["Name"]);
    }

    public void SkipToNextRound()
    {
    	countdown = 1f;
    }

}
