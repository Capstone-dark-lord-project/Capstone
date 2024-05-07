using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swaying : MonoBehaviour
{
    public float swayAmount = 0.1f;
    public float swaySpeed = 3.0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        transform.position = originalPosition + new Vector3(0, sway, 0);
    }
}
