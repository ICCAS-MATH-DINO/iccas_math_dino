using UnityEngine;

public class QuizOptionCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<QuizManager>().OnOptionCollision(this.gameObject);
        }
    }
}
