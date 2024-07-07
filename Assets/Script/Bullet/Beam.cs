using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ȯ�Ǹ� �ٱⰡ ���̴ٰ� �ѹ��� ������ ��
/// </summary>
public class Beam : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OffCollider();
    }

    private void OnDisable()
    {
        OffCollider();
    }

    // Animator Event
    public void ExitAnimation()
    {
        gameObject.SetActive(false);
    }

    public void OffCollider()
    {
        boxCollider.enabled = false;
    }

    public void OnCollider()
    {
        boxCollider.enabled = true;
    }
}
