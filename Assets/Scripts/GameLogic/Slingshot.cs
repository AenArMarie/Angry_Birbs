using UnityEngine;
using Zenject;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Slingshot : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _startPosition;
    private string _state = "Inactive";
    private string _lastSavedState = "Active";
    private float _stopFollowVelocityThreshold = 0.1f;
    private float _stopFollowHeightThreshold = 2f;
    private float _maxDragRadius = 2f;
    private float _minDragRadius = 0.5f;
    private float _launchForceMultiplier = 10f;
    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    public void SetActive(Transform localTransform)
    {
        _state = "Active";
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
        _rb.isKinematic = true;
        _startPosition = localTransform.position;
    }

    public void SetFrozenStatus(bool status)
    {
        if (status)
        {
            _lastSavedState = _state;
            _state = "Frozen";
        }
        else { _state = _lastSavedState; }
    }

    private void Update()
    {
        if (_state == "Launched" && Time.frameCount % 10 == 0) 
        {
            if (_rb.velocity.sqrMagnitude <= _stopFollowVelocityThreshold * _stopFollowVelocityThreshold
                && transform.position.y <= _stopFollowHeightThreshold)
            {
                _eventHandler.NextBirdIsUp();
                _state = "Inactive";
                _animator.speed = 0;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchDown(touch);
                    break;
                case TouchPhase.Moved:
                    OnTouchDrag(touch);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnTouchUp(touch);
                    break;
            }
        }
    }

    private void OnTouchDown(Touch touch)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject && _state == "Active")
        {
            _state = "Dragging";
            _eventHandler.SetCameraStatus(false);
        }
    }

    private void OnTouchUp(Touch touch)
    {
        if (_state == "Dragging")
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 direction = touchPosition - _startPosition;

            if (direction.magnitude > _minDragRadius)
            {
                Vector2 releaseVector = _startPosition - (Vector2)transform.position;
                _rb.isKinematic = false;
                _rb.AddForce(releaseVector * _launchForceMultiplier, ForceMode2D.Impulse);
                _eventHandler.OnBirdLaunched(_rb);
                _state = "Launched";
                if (!_animator.GetBool("IsFlying"))
                {
                    _animator.SetBool("IsFlying", true);
                }
            }
            else
            {
                _eventHandler.SetCameraStatus(true);
                _state = "Returning";
                transform.DOMove(_startPosition, 1f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => _state = "Active");
            }
        }
    }

    private void OnTouchDrag(Touch touch)
    {
        if (_state == "Dragging")
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 direction = touchPosition - _startPosition;

            // Limit dragging within radius
            if (direction.magnitude > _maxDragRadius)
            {
                direction = direction.normalized * _maxDragRadius;
            }

            transform.position = _startPosition + direction;
        }
    }
}
