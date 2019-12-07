using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SkillRail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform steamLogoPrefab;
    private bool selected = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GetComponent<MeshRenderer>().material.color = new Color(1f, 0.15f, 0f, 0.40f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.33f);
    }

    void OnMouseDown()
    {
        //Instantiate(steamLogoPrefab, transform.position, transform.rotation);
        selected = true;
    }

    public bool IsSelected()
    {
        return selected;
    }

    public void Reset()
    {
        selected = false;
    }
}
