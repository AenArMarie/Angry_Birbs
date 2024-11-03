using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float _deathDelay = 1f;
    public float _minImpactSpeed = 5f;
    [SerializeField] private AudioClip _deathSoundClip;
    private bool _isDying = false;

    private EventHandler _eventHandler;

    private void Start()
    {
        _eventHandler = FindObjectOfType<EventHandler>(); // Find EventHandler in the scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= _minImpactSpeed)
        {
            if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall")) && !_isDying)
            {
                _isDying = true;
                GetComponent<Animator>().speed = 0;
                StartCoroutine(DieAfterDelay());
            }
        }
    }

    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(_deathDelay);
        SoundFXHandler.instance.PlaySoundFXClip(_deathSoundClip, transform, 1f);
        _eventHandler.DecreaseEnemyCount();
        Destroy(gameObject);
    }
}
