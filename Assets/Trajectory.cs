using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.003f, 0.1f)] float dotMinScale;
    [SerializeField] [Range(0.05f, 0.3f)] float dotMaxScale;

    Transform[] dotsList;

    Vector2 pos;

    float timeStamp;    // dot pos

    void Start()
    {
        HIde();
        PrepareDots();
     }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
        }
    }

    public void UpdateDots(Vector3 weaponPos, Vector3 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (weaponPos.x + forceApplied.x * timeStamp); ;
            pos.y = (weaponPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f; ;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void HIde()
    {
        dotsParent.SetActive(false);
    }
}
