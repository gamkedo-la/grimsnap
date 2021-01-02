using GrimSnapAudio;
using UnityEngine;
public class Attack : MonoBehaviour
{
    [SerializeField] float attackRate;
    [SerializeField] bool useAttackRate;
    [SerializeField] float nextAttackTime;

    [SerializeField] AudioCharacter characterAudioProfile;
    internal IAudioActions audioAction;

    void Start()
    {
        if (useAttackRate)
        { nextAttackTime = attackRate; }
        else
        { nextAttackTime = 0; }

        if (characterAudioProfile is IAudioActions)
            audioAction = GetComponent<IAudioActions>();
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

            EnemyController enemyController = target.gameObject.GetComponent<EnemyController>();
            enemyController.KnockBack(target.transform.position - this.transform.position);

            if (audioAction != null)
                audioAction.AttackAudio();
            else
                Debug.LogWarning("Audio Attack Profile Null" + transform.name);

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
