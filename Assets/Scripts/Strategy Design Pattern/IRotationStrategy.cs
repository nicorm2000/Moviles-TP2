using UnityEngine;

public interface IRotationStrategy
{
    void Rotate(Transform transform, float rotateSpeed, float deltaTime);
}