using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerScript playerScript;

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerScript.isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerScript.isGrounded = false;
    }
}
