using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float attackRate;
    [SerializeField] bool useAttackRate;
    [SerializeField] float nextAttackTime;

    void Start()
    {
        if (useAttackRate)
        { nextAttackTime = attackRate; }
        else
        { nextAttackTime = 0; }
    }

    void Update()
    {
        if (useAttackRate && nextAttackTime > 0)
            nextAttackTime -= Time.deltaTime;
    }

    //Ony responsible for doing damage to target, not checking whether or not you can
    public void AttackTarget(Health target, float damage)
    {
        if (AttackRateTimerComplete())
        {
            target.TakeDamage(damage);
            Debug.Log(this.gameObject.name + " is attacking!");
            ResetAttackTimer();
        }
    }

    private bool AttackRateTimerComplete()
    {
        return nextAttackTime <= 0 ? true : false;
    }

    private void ResetAttackTimer()
    {
        if (useAttackRate)
            nextAttackTime = attackRate;
    }
}
