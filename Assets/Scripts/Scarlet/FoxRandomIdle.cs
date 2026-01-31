using UnityEngine;

public class FoxRandomIdle : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float switchProbability = 0.3f;
    [SerializeField] private float blendDuration = 0.3f;
    [Range(0f, 1f)]
    [SerializeField] private float idleBias = 0.7f;  //0=idle2, 1=idle1

    private float targetIdleValue;
    private float currentIdleValue;
    private bool isTransitioning = false;
    private float transitionStartTime;
    private float transitionStartValue;
    private bool hasCheckedThisCycle = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetIdleValue = GetRandomIdleValue();
        currentIdleValue = targetIdleValue;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle Blend"))
        {
            float normalizedTime = stateInfo.normalizedTime % 1f;

            if (normalizedTime < 0.2f)
            {
                hasCheckedThisCycle = false;
            }

            if (normalizedTime >= 0.8f && !hasCheckedThisCycle && !isTransitioning && !animator.IsInTransition(0))
            {
                hasCheckedThisCycle = true;

                if (Random.value <= switchProbability)
                {
                    transitionStartValue = currentIdleValue;
                    targetIdleValue = GetRandomIdleValue();
                    isTransitioning = true;
                    transitionStartTime = Time.time;
                }
            }
        }

        if (isTransitioning)
        {
            float progress = Mathf.Clamp01((Time.time - transitionStartTime) / blendDuration);
            currentIdleValue = Mathf.Lerp(transitionStartValue, targetIdleValue, Mathf.SmoothStep(0f, 1f, progress));
            if (progress >= 1f) isTransitioning = false;
        }

        animator.SetFloat("IdleVariation", currentIdleValue);
        //Debug.Log(currentIdleValue);
    }

    float GetRandomIdleValue()
    {
        if (Random.value < idleBias)
        {
            return Random.Range(0.6f, 1f);
        }
        else
        {
            return Random.Range(0f, 0.4f);
        }
    }
}