using System.Collections;
using UnityEngine;

public class Steam : MonoBehaviour
{
    private float speed = 20f;
    private float attackPower = 100f;

    private float duration_default = 3.5f;
    private float duration;

    private Vector3 direction;

    void OnTriggerEnter(Collider col)
    {
        var enemy = col.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damaged(attackPower);
        }
    }

    void Update()
    {
        if (duration < 0f)
        {
            Destroy(gameObject);
        }
        duration -= Time.deltaTime;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void InitSteam(Vector3 dir)
    {
        duration = duration_default;
        direction = dir;
    }
}
