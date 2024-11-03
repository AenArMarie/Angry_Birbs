using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    private float _followSpeed = 5f; 
    private float _dragSpeed = 0.5f; 

    private Rigidbody2D _targetRigidbody; // цель для слежения
    private bool _isFollowing = false; // следует ли камера за птицей
    private bool _isDragging = false; // перетаскивает ли игрок камеру
    private bool _cameraActive = true;
    private Vector3 _initialPosition; 
    private Vector3 _lastTouchPosition; // последняя записанная позиция соприкосновения для перетаскивания

    // границы камеры
    private float _minX ;
    private float _maxX;
    private float _minY;
    private float _maxY;

    [Inject]
    public void Construct(
        [Inject(Id = "cameraXNegativeEdge")] float minX,
        [Inject(Id = "cameraXPositiveEdge")] float maxX,
        [Inject(Id = "cameraYNegativeEdge")] float minY,
        [Inject(Id = "cameraYPositiveEdge")] float maxY)
    {
        _minX = minX;
        _maxX = maxX;
        _maxY = maxY;
        _minY = minY;
    }
    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public void SetTarget(Rigidbody2D targetRigidbody) //передача камере цели для слежения
    {
        _targetRigidbody = targetRigidbody;
        _cameraActive = true;
        _isFollowing = true; 
    }

    public void SetCameraActivity(bool status)
    {
        _cameraActive = status;
    }

    private void Update()
    {
        if (_cameraActive)
        {
            if (_isFollowing && _targetRigidbody != null)
            {
                FollowTarget();
            }
            else if (_isDragging)
            {
                HandleTouchDrag();
            }
            HandleTouchInput();
        }
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(_targetRigidbody.position.x, _targetRigidbody.position.y, transform.position.z);
        targetPosition = ClampPosition(targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime); //можно и через DoTween, но тогда будет спауниться твин при каждом передвижении
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _isDragging = true;
                _lastTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isDragging = false;
            }
        }
    }

    private void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 currentTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 touchDelta = _lastTouchPosition - currentTouchPosition;

                Vector3 newPosition = transform.position + new Vector3(touchDelta.x, touchDelta.y, 0) * _dragSpeed;
                transform.position = ClampPosition(newPosition);

                _lastTouchPosition = currentTouchPosition;
            }
        }
    }

    private Vector3 ClampPosition(Vector3 position) //ограничиваем позицию камеры по 4 осям
    {
        float clampedX = Mathf.Clamp(position.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(position.y, _minY, _maxY);
        return new Vector3(clampedX, clampedY, position.z);
    }

    public void StopFollowing()
    {
        _isFollowing = false;
        ReturnToInitialPosition();
    }

    private void ReturnToInitialPosition()
    {
        transform.DOMove(ClampPosition(_initialPosition), 1f).SetEase(Ease.OutQuad);
    }
}
