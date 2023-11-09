using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private AudioClip moveClip, loseClip;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private GameObject explosion;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameplayManager.hasStarted = true;
            rotateSpeed *= -1f;
            AudioManager.instance.PlaySound(moveClip);
        }
    }

    private void FixedUpdate()
    {
        if (gameplayManager.hasStarted)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Instantiate(explosion, transform.GetChild(0).position, Quaternion.identity);
            gameplayManager.GameEnded();
            Destroy(gameObject);
            return;
        }
    }
}