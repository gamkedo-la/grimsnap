using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public bool isDead = false;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage" + transform.name);
        health -= damage;
        if (GetComponent<EnemyController>() != null)
        {
            GetComponent<EnemyController>().WanderRadius = GetComponent<EnemyController>().WR;

        }

        if (gameObject.tag == "Enemy" && health <= 0)
        {
            Kill();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }
}
