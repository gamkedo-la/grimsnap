using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class HealthOrbTexture : MonoBehaviour
{
    public float animSpeed;
    private RawImage healthOrbImage;
    private Rect healthOrbImageRect;

    private void Start()
    {
        healthOrbImage = GetComponent<RawImage>();
        healthOrbImageRect = healthOrbImage.uvRect;
    }

    private void Update()
    {
        healthOrbImageRect.x += Time.deltaTime * animSpeed;
        healthOrbImage.uvRect = healthOrbImageRect;
    }
}
