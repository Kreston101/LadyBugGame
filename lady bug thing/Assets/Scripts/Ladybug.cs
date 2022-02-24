using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    public int ladyBugNum;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool destroyingBug;

    private Rigidbody2D rb2d;
    private bool running;
    private GameManager GmScript;

    void Start()
    {
        GmScript = FindObjectOfType(typeof(GameManager)) as GameManager;
        running = false;
        grounded = true;
        destroyingBug = false;
        rb2d = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, RandRotate());
    }

    void FixedUpdate()
    {
        //if (grounded == true)
        //{
        //    if (Input.GetKey("space"))
        //    {
        //        rb2d.AddForce(transform.up * 0.1f, ForceMode2D.Impulse);
        //        StopCoroutine(Rotate());
        //    }
        //    else
        //    {
        //        rb2d.velocity = Vector3.zero;
        //        rb2d.angularVelocity = 0f;
        //        if (running == false)
        //        {
        //            StartCoroutine(Rotate());
        //        }
        //    }
        //}
        //else
        //{
        //    rb2d.velocity = Vector3.zero;
        //    rb2d.angularVelocity = 0f;
        //}

        if (grounded == true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    rb2d.AddForce(transform.up, ForceMode2D.Impulse);
                    StopCoroutine(Rotate());
                }
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

    private int RandRotate()
    {
        int randNum = Random.Range(1, 8);
        int rotateTo = 0;
        switch (randNum)
        {
            case 1:
                rotateTo = 45;
                break;
            case 2:
                rotateTo = 90;
                break;
            case 3:
                rotateTo = 135;
                break;
            case 4:
                rotateTo = 180;
                break;
            case 5:
                rotateTo = 225;
                break;
            case 6:
                rotateTo = 270;
                break;
            case 7:
                rotateTo = 315;
                break;
            case 8:
                rotateTo = 360;
                break;
        }
        return rotateTo;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Ladybug>().ladyBugNum < ladyBugNum)
        {
            GmScript.removeThisLadyBug(other.gameObject.GetComponent<Ladybug>().ladyBugNum);
            other.gameObject.GetComponent<Ladybug>().destroyingBug = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyingBug == false)
        {
            grounded = false;
            GmScript.LadyBugEscaped();
        }
    }
}