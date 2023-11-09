using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float minRotateSpeed, maxRotateSpeed;
    [SerializeField] private float minRotateTime, maxRotateTime;
    [SerializeField] private GameplayManager gameplayManager;

    private bool _hasGameFinished;
    private float _currentRotateSpeed;
    private float _currentRotateTime;
    private float _rotateTime;

    private void Awake()
    {
        _hasGameFinished = false;
        _currentRotateTime = 0;
    }

    private void Update()
    {
        if (gameplayManager.hasStarted)
        {
            _currentRotateTime += Time.deltaTime;
            if (_currentRotateTime > _rotateTime)
            {
                _currentRotateTime = 0;
                _currentRotateSpeed = minRotateSpeed + (maxRotateSpeed - minRotateSpeed) * Random.Range(0, 11) * 0.1f;
                _rotateTime = minRotateTime + (maxRotateTime - minRotateTime) * Random.Range(0, 11) * 0.1f;
                _currentRotateSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameplayManager.hasStarted)
        {
            if (_hasGameFinished)
        {
            return;
        }
        transform.Rotate(0, 0, _currentRotateSpeed * Time.fixedDeltaTime);
        }
    }
}