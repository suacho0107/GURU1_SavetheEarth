using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class DebrisDispenser : MonoBehaviour
{
    public GameObject debrisParticle;
    public GameObject debrisMark;

    public Color debrisParticleColor;
    public Color debrisMarkColor;

    [MinMaxSlider(0, 100)]
    public Vector2 debrisGroundVelocityRange;
    [MinMaxSlider(0, 100)]
    public Vector2 debrisVerticalVelocityRange;

    [MinMaxSlider(1, 12)]
    public Vector2 debrisParticleSizeRange;
    [MinMaxSlider(1, 32)]
    public Vector2 debrisMarkSizeRange;

    [MinMaxSlider(0, 30)]
    public Vector2Int debrisAmountRange;

    public int debrisDispenseDegrees;

    public Vector2 moveDirection;
    public Vector2 lastPosition;
    public Vector2 debrisDirection = new Vector2();

    private void Update()
    {
        moveDirection = ((Vector2)transform.position - lastPosition).normalized;
    }

    private void LateUpdate()
    {
        lastPosition = transform.position;
    }

    public void DispenseDebris()
    {
        int debrisAmount = Random.Range(debrisAmountRange.x, debrisAmountRange.y);

        if (debrisMark != null)
        {
            GameObject instantiateDebrisMark = Instantiate(debrisMark, transform.position, Quaternion.identity);
            instantiateDebrisMark.GetComponent<SpriteRenderer>().color = debrisMarkColor;
            instantiateDebrisMark.transform.localScale = Vector3.one * Random.Range(debrisMarkSizeRange.x, debrisMarkSizeRange.y);
        }

        for (int i = 0; i < debrisAmount; i++)
        {
            FakeHeightObject instantiateDebrisPartcle = Instantiate(debrisParticle, transform.position, Quaternion.identity).GetComponent<FakeHeightObject>();
            instantiateDebrisPartcle.transform.localScale = Vector3.one * Random.Range(debrisParticleSizeRange.x, debrisParticleSizeRange.y);
            instantiateDebrisPartcle.trnsBody.GetComponent<SpriteRenderer>().color = debrisParticleColor;

            float randomizedDirectionAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) + (Random.Range(-debrisDispenseDegrees / 2, debrisDispenseDegrees / 2) * Mathf.Deg2Rad);
            debrisDirection.x = Mathf.Cos(randomizedDirectionAngle);
            debrisDirection.y = Mathf.Sin(randomizedDirectionAngle);
            debrisDirection.Normalize();

            instantiateDebrisPartcle.Initialize(debrisDirection * Random.insideUnitCircle * Random.Range(debrisGroundVelocityRange.x, debrisGroundVelocityRange.y),
               Random.Range(debrisVerticalVelocityRange.x, debrisVerticalVelocityRange.y));
        }
    }
}
