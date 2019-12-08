using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public void OnClickBuy3TierTower()
	{
		GM.GetComponent<GameMaster>().Buy3TierTower();
	}

    public void OnClickSkillSteam()
    {
        GM.GetComponent<GameMaster>().UseSkillSteam();
    }

    public void OnClickSkillOverClock()
    {
        GM.GetComponent<GameMaster>().UseSkillOverClock();
    }

    public void OnClickSkillOverworked()
    {
        GM.GetComponent<GameMaster>().UseSkillOverworked();
    }

    public void OnClickSkillRedbull()
    {
        GM.GetComponent<GameMaster>().UseSkillRedbull();
    }

    public void OnClickSkillChilldown()
    {
        GM.GetComponent<GameMaster>().UseSkillChilldown();
    }
    
    public void OnClickGacha1TierTower()
    {
        GM.GetComponent<GameMaster>().GachaTowerRank(1);
    }

    public void OnClickGacha2TierTower()
    {
        GM.GetComponent<GameMaster>().GachaTowerRank(2);
    }

    public void OnClickGacha3TierTower()
    {
        GM.GetComponent<GameMaster>().GachaTowerRank(3);
    }

    public void OnClickSkipToNextRound()
    {
        GM.GetComponent<GameMaster>().SkipToNextRound();
    }

    public void OnClickReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
