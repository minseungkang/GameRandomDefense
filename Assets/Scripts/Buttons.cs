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

	public void OnClickBuy3TierTOwer()
	{
		GM.GetComponent<GameMaster>().Buy3TierTower();
	}

    public void OnClickGacha1TierTower()
    {
        GM.GetComponent<GameMaster>().GachaTowerRank(1);
    }
}
