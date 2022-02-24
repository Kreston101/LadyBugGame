using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugMobile : MonoBehaviour
{
    public int ladyBugNum;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool destroyingBug;

    private Rigidbody2D rb2d;
    private bool running;
    private GameManager GmScript;

    void Update()
    {
        if (grounded == true && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                rb2d.AddForce(transform.up * 0.1f, ForceMode2D.Impulse);
                StopCoroutine(Rotate());
            }
            else
            {
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0f;
                if (running == false)
                {
                    StartCoroutine(Rotate());
                }
            }
        }
        else
        {
            rb2d.velocity = Vector3.zero;
            rb2d.angularVelocity = 0f;
        }
    }

    IEnumerator Rotate()
    {
        running = true;
        transform.Rotate(0, 0, -45f, Space.Self);
        yield return new WaitForSecondsRealtime(0.5f);
        running = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyingBug == false)
        {
            grounded = false;
        }
    }
}
