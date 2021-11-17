using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Damageable
{
    [SerializeField] protected float knockbackForce;
    [SerializeField] protected float knockbackTime;
    [SerializeField] protected float decaySpeed;                  //decay speed of the laser when stop shooting


    [Range(0, 1)] protected float currentWidthPercent = 1;
    protected float maxWidth;

    protected LineRenderer lineRenderer;

    // Start is called before the first frame update
    protected void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        maxWidth = lineRenderer.widthMultiplier;
    }

    protected IEnumerator Decay()
    {
        while (currentWidthPercent > 0)
        {
            currentWidthPercent -= decaySpeed * Time.deltaTime;
            lineRenderer.widthMultiplier = Mathf.Lerp(0, maxWidth, currentWidthPercent);
            yield return null;
        }

        Destroy(gameObject);
    }
}
