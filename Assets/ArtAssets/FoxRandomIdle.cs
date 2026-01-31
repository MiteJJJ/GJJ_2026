using UnityEngine;

public class FoxRandomIdle : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float switchProbability = 0.3f;
    [SerializeField] private float blendDuration = 0.3f;
    private float targetIdleValue;
    private float currentIdleValue;
    private bool isTransitioning = false;
    private float transitionStartTime;
    private float transitionStartValue;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetIdleValue = Random.Range(0f, 1f);
        currentIdleValue = targetIdleValue;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle Blend"))
        {
            float normalizedTime = stateInfo.normalizedTime % 1f;
            if (normalizedTime >= 0.8f && normalizedTime <= 1.0f && !isTransitioning && !animator.IsInTransition(0))
            {
                if (Random.value <= switchProbability)
                {
                    transitionStartValue = currentIdleValue;
                    targetIdleValue = Random.Range(0f, 1f);
                    isTransitioning = true;
                    transitionStartTime = Time.time;
                }
            }
        }
        if (isTransitioning)
        {
            Debug.Log("Transitioning");
            float progress = Mathf.Clamp01((Time.time - transitionStartTime) / blendDuration);
            currentIdleValue = Mathf.Lerp(transitionStartValue, targetIdleValue, Mathf.SmoothStep(0f, 1f, progress));
            if (progress >= 1f) isTransitioning = false;
        }
        animator.SetFloat("IdleVariation", currentIdleValue);
    }
}