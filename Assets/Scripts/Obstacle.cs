using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float minRotateSpeed, maxRotateSpeed;
    [SerializeField] private float minRotateTime, maxRotateTime;
    [SerializeField] private int maxiumRange;
    [SerializeField] private int minimumRangeRotationStrategy;
    [SerializeField] private int maxiumRangeRotationStrategy;
    [SerializeField] private float reciprocalMultiplier;

    private bool _hasGameFinished;
    private float _currentRotateSpeed;
    private float _currentRotateTime;
    private float _rotateTime;
    private IRotationStrategy _rotationStrategy;

    private void Awake()
    {
        _hasGameFinished = false;
        _currentRotateTime = 0;
        _rotationStrategy = new ClockwiseRotation();
    }

    private void Update()
    {
        if (GameplayManager.hasStarted)
        {
            _currentRotateTime += Time.deltaTime;
            if (_currentRotateTime > _rotateTime)
            {
                _currentRotateTime = 0;
                _currentRotateSpeed = RandomRotateSpeed();
                _rotateTime = RandomRotateTime();
                _rotationStrategy = RandomRotateStrategy();
                Debug.Log("Enemy rotate speed:" + _currentRotateSpeed);
                Debug.Log("Enemy rotate direction:" + _rotationStrategy);
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameplayManager.hasStarted)
        {
            if (_hasGameFinished)
            {
                return;
            }
            _rotationStrategy.Rotate(transform, _currentRotateSpeed, Time.fixedDeltaTime);
            Debug.Log("Enemy rotated");
        }
    }

    private float RandomRotateSpeed()
    {
        return minRotateSpeed + (maxRotateSpeed - minRotateSpeed) * Random.Range(0, maxiumRange) * reciprocalMultiplier;
    }

    private float RandomRotateTime()
    {
        return minRotateTime + (maxRotateTime - minRotateTime) * Random.Range(0, maxiumRange) * reciprocalMultiplier;
    }

    private IRotationStrategy RandomRotateStrategy()
    {
        return Random.Range(minimumRangeRotationStrategy, maxiumRangeRotationStrategy) == 0 ? new ClockwiseRotation() : new CounterClockwiseRotation();
    }
}