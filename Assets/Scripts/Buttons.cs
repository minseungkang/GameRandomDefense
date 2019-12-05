using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
	public GameObject GM;


	public void OnClickBuy1TierTower()
	{
		GM.GetComponent<GameMaster>().Buy1TierTower();
	}

	public void OnClickBuy2TierTower()
	{
		GM.GetComponent<GameMaster>().Buy2TierTower();
	}
}
