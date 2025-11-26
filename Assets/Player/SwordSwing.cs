using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    public Animator animator;
    public float swingCooldown = 0.8f;
    private bool canSwing = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSwing)
        {
            StartCoroutine(SwingOnce());
        }
    }

    private IEnumerator SwingOnce()
    {
        canSwing = false;
        animator.SetTrigger("Swing");
        yield return new WaitForSeconds(swingCooldown);
        canSwing = true;
    }
}
