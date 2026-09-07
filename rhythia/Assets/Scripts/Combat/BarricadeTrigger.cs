using UnityEngine;

public class BarricadeTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            GameManager.Instance.LoseLife();
            Destroy(other.gameObject);
        }
    }
}
