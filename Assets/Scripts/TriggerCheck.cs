using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    PlayerScript playerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponentInParent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StickyPlatform")
        {
            playerScript.HitStickyPlatform();
            if (gameObject.name == "LeftTrigger" || gameObject.name == "RightTrigger")
            {
                playerScript.horizontalTrigger = true;
            }

            if (gameObject.name == "UpTrigger")
            {
                playerScript.upTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "StickyPlatform")
        {
            playerScript.SetGravity();
            if (gameObject.name == "LeftTrigger" || gameObject.name == "RightTrigger")
            {
                playerScript.horizontalTrigger = false;
            }

            if (gameObject.name == "UpTrigger")
            {
                playerScript.upTrigger = false;
            }
        }
    }
}
