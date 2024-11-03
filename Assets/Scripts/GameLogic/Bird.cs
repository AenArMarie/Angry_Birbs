using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float _minImpactSpeed = 5f;
    [SerializeField] private AudioClip[] _impactSoundClips;
    private void OnCollisionEnter2D(Collision2D collision) //звук удара при нужной силе соприкосновения
    {
        if (collision.relativeVelocity.magnitude >= _minImpactSpeed)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
            {
                SoundFXHandler.instance.PlayRandomSoundFXClip(_impactSoundClips, transform, 1f);
            }
        }
    }
}
