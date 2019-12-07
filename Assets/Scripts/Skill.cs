using System;
using UnityEngine;


public class Skill : MonoBehaviour
{
    private float timeOverclock = 0f;
    private float timeRedbull = 0f;
    private float timeChilldown = 0f;

    private bool flagOverclock = false;
    private bool flagRedbull = false;
    private bool flagChilldown = false;


    void Start()
    {

    }

    void Update()
    {
        GameObject[] currentTurrets = GameObject.FindGameObjectsWithTag("Turret");
        float changed = Time.deltaTime;

        if (timeOverclock >= 0f)
        {
            timeOverclock -= changed;
        }
        else if (flagOverclock)
        {
            DeactOverclock(currentTurrets);
        }

        if (timeRedbull >= 0f)
        {
            timeRedbull -= changed;
        }
        else if (flagRedbull)
        {
            DeactRedbull(currentTurrets);
        }

        if (timeChilldown >= 0f)
        {
            timeChilldown -= changed;
        }
        else if (flagChilldown)
        {
            flagChilldown = false;
            DeactChilldown(currentTurrets);
        }

    }

    internal void UseOverclock(GameObject[] currentTurrets)
    {
        timeOverclock = 10f;
        flagOverclock = true;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.ActivateSkill1();
            x.FasterTurret(0.5f);
        }
    }

    void DeactOverclock(GameObject[] currentTurrets)
    {
        flagOverclock = false;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.DeactivateSkill1();
            x.SlowerTurret(0.5f);
        }
    }

    internal void UseRedbull(GameObject[] currentTurrets)
    {
        timeRedbull = 10f;
        flagRedbull = true;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.ActivateSkill2();
            x.FasterTurret(0.5f);
        }
    }

    void DeactRedbull(GameObject[] currentTurrets)
    {
        flagRedbull = false;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.DeactivateSkill2();
            x.SlowerTurret(0.5f);
        }
    }

    internal void UseChilldown(GameObject[] currentTurrets)
    {
        timeChilldown = 10f;
        flagChilldown = true;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.ActivateSkill3();
            x.SlowerTurret(0.5f);
        }
    }

    void DeactChilldown(GameObject[] currentTurrets)
    {
        flagChilldown = false;

        foreach (GameObject t in currentTurrets)
        {
            Turret x = t.GetComponent<Turret>();
            x.DeactivateSkill3();
            x.FasterTurret(0.5f);
        }
    }
}