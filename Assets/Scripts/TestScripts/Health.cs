using GrimSnapAudio;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public bool isDead = false;

    public float armorModifier = 1;

    public GameObject player;

    [SerializeField] AudioCharacter characterAudioprofile;
    internal IAudioActions audioAction;

    private void Awake()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

        if (characterAudioprofile is IAudioActions)
            audioAction = GetComponent<IAudioActions>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage" + transform.name);
        health -= damage * armorModifier;
        

        if (audioAction != null)
            audioAction.TakeDamageAudio();
        else
            Debug.LogError("Character Audio Profile Null" + transform.name);

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

        player.GetComponent<PlayerLevel>().GainEXP(GetComponent<EnemyController>().EXPGiven);
        Destroy(this.gameObject);
    }
}
