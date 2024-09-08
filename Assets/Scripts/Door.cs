using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
    }
}
