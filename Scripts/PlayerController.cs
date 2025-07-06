using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("run");  // 처음엔 항상 달리기부터 시작
        }
    }

    void Update()
    {
        // 앞으로 이동 (Z+ 방향)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // 게임오버 시 애니메이션 상태를 바꾸는 함수 (GameManager에서 호출)
    public void PlaySadAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("GameOver");
        }
    }
}
