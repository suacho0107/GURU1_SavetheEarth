using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndSceneScript : MonoBehaviour
{
    public Image image;
    public Sprite[] images;
    public Text text;
    public float fadeDuration = 1f;
    public float changeInterval = 2f;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = images[currentIndex];
        StartCoroutine(FadeImages());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeImages()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(FadeOut());
            if(currentIndex < images.Length -1){
                currentIndex += 1;
            } else{
                text.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("StartScene");
            }
            image.sprite = images[currentIndex];
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.color = endColor;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = new Color(image.color.r, image.color.g, image.color.b, 0f);
        Color endColor = new Color(image.color.r, image.color.g, image.color.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.color = endColor;
    }
}
