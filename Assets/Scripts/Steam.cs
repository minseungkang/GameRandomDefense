using System.Collections;
using UnityEngine;

public class Steam : MonoBehaviour
{
    private float speed;

    private int startingPointIndex = 0;
    private int transparency = 0;

    private float attackPower = 100f;

    void OnTriggerEnter(Collider col)
    {
        col.GetComponent<Enemy>().Damaged(attackPower);
    }
}
