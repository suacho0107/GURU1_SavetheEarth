using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] Sprite defaultButton;
    [SerializeField] Sprite pressedButton;

    private SpriteRenderer sr;
    private Animator doorAnimator;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = defaultButton;

        Transform doorTransform = transform.GetChild(0);
        doorAnimator = doorTransform.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Press());
            doorAnimator.SetBool("isPress", true);
        }
    }

    IEnumerator Press()
    {
        sr.sprite = pressedButton;

        yield return new WaitForSeconds(0.3f);
        sr.sprite = defaultButton;
    }
}
