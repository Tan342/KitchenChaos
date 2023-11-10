using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    private Animator m_Animator;

    [SerializeField] private Player player;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(IS_WALKING, false);
    }

    private void Update()
    {
        m_Animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
