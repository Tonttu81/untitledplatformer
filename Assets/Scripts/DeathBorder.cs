using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeathBorder : MonoBehaviour
{
    public CanvasGroup deathScreen;
    public PostProcessVolume volume;
    Sounds sounds;
    DepthOfField dof;

    private void Start()
    {
        volume.profile.TryGetSettings(out dof);
        sounds = GetComponent<Sounds>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        sounds.PlayDeathSound();
        dof.focusDistance.value = 1f;
        deathScreen.alpha = 1f;
        deathScreen.interactable = true;
    }
}
