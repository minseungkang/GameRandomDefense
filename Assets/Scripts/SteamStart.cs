using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteamStart : MonoBehaviour
{
    public GameObject GM;
    public static Transform[] startingPoints;

    void awake()
    {
        startingPoints = new Transform[4];
        for (int i=0; i<4; i++)
        {
            transform.GetChild(i);
        }
    }

    public void OnClickGetValved()
    {
        GM.GetComponent<GameMaster>().UseSkillSteam();
    }
}
