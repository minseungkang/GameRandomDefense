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

    private Vector3[] posModifier = new Vector3[4] 
    {
        new Vector3(-1f, 0f, 4f),
        new Vector3(4f, 0f, 1f),
        new Vector3(1f, 0f, -4f),
        new Vector3(-4f, 0f, -1f)
    };

    void Awake()
    {
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

    public bool IsActive()
    {
        return activated;
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
                foreach (GameObject x in skillRails) {
                    x.GetComponent<SkillRail>().Reset();
                    x.GetComponent<MeshRenderer>().enabled = false;
                }

                GameObject sobj = Instantiate(steamLogoPrefab, Waypoints.points[(i)%4].position + posModifier[i], Quaternion.Euler(0, 90 * i, 0)).gameObject;
                Steam s = sobj.GetComponent<Steam>();
                s.InitSteam(new Vector3(-1f, 0f, 0f));

                activated = false;
                break;
            }
        }
    }
}
