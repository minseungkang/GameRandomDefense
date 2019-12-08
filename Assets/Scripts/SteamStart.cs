using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SteamStart : MonoBehaviour
{
    public GameObject GM;
    public Transform steamLogoPrefab;
    public static Transform[] startingPoints;
    public static GameObject[] skillRails;
    public bool activated = false;

    void Awake()
    {
        //startingPoints = new Transform[4];
        //for (int i=0; i<4; i++)
        //{
        //    transform.GetChild(i);
        //}

        skillRails = GameObject.FindGameObjectsWithTag("Rail");
        
        foreach (GameObject sr in skillRails)
        {
            sr.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void UseSteam()
    {
        activated = true;
        foreach (GameObject sr in skillRails)
        {
            MeshRenderer srm = sr.GetComponent<MeshRenderer>(); //.enabled = true;
            srm.enabled = true;
            srm.material.color = new Color(1f, 1f, 1f, 0.33f);
        }
    }

    void Update()
    {
        if (activated)
        {
            checkRails();
        }
    }

    void checkRails()
    {
        for (int i = 0; i < 4; i++)
        {
            SkillRail srs = skillRails[i].GetComponent<SkillRail>();
            MeshRenderer srm = skillRails[i].GetComponent<MeshRenderer>();
            if (srs.IsSelected())
            {
                srs.Reset();
                srm.enabled = false;

                Instantiate(steamLogoPrefab, Waypoints.points[(i)%4].position, srs.transform.rotation);
            }
        }
    }
}
