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
            rotateSpeed *= -1f;
            AudioManager.instance.PlaySound(moveClip);
        }
//#if UNITY_ANDROID
//        {
//            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//            {
//                rotateSpeed *= -1f;
//                AudioManager.instance.PlaySound(moveClip);
//                Debug.Log("Touch detected!");
//            }
//        }
//#else
//        {
//            if (Input.GetMouseButtonDown(0))
//            {
//                rotateSpeed *= -1f;
//                AudioManager.instance.PlaySound(moveClip);
//                Debug.Log("Mouse click detected!");
//            }
//        }
//#endif
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
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