using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimatorStateInfo currentState;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
    }

    public void PlayAnimatorClip(string clip)
    {
        animator.CrossFade(clip, 0);
    }

    public bool CheckCurrentClip(string name)
    {
        return currentState.IsName(name);
    }

    public float CurrentClipNormalize()
    {
        return currentState.normalizedTime;
    }
}
