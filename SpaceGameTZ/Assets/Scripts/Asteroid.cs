using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    [SerializeField] private float rotationOffset;
    private Vector3 randomRotation;
    private void Start()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 newScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
        
        transform.localScale = new Vector3(originalScale.x * newScale.x, originalScale.y * newScale.y, originalScale.z * newScale.z);
        randomRotation = new Vector3(Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));
    }

    private void Update()
    {
        transform.Rotate(randomRotation * Time.deltaTime);
    }
}
