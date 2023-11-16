using UnityEngine;

public class ClockwiseRotation : IRotationStrategy
{
    public void Rotate(Transform transform, float rotateSpeed, float deltaTime)
    {
        transform.Rotate(0, 0, rotateSpeed * deltaTime);
    }
}