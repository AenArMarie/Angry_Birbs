using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float deathDelay = 1f;
    public float minImpactSpeed = 5f;
    [SerializeField] private AudioClip _deathSoundClip;
    private bool _isDying = false;

    private EventHandler _eventHandler;

    private void Start()
    {
        _eventHandler = FindObjectOfType<EventHandler>(); // Find EventHandler in the scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= minImpactSpeed)
        {
            if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall")) && !_isDying)
            {
                _isDying = true;
                GetComponent<Animator>().speed = 0;
                StartCoroutine(DieAfterDelay());
            }
        }
    }

    private IEnumerator DieAfterDelay() //умирает после задержкки (можно добавить эффекты смерти)
    {
        yield return new WaitForSeconds(deathDelay);
        SoundFXHandler.instance.PlaySoundFXClip(_deathSoundClip, transform, 1f);
        _eventHandler.DecreaseEnemyCount();
        Destroy(gameObject);
    }
}
