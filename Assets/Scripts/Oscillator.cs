using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPosition;

    [SerializeField] private Vector3 movementVector;

    [SerializeField] [Range(0,1)] private float movementFactor;

    [SerializeField] private float period = 2f;

    [SerializeField] private float sinFactor = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        var cycles = Time.time / period;
        
        const float tau = Mathf.PI * 2;

        var rawSinWave = Mathf.Sin(cycles * tau);
        
        movementFactor = rawSinWave * sinFactor;
        
        Vector3 offset = movementVector * movementFactor;
        transform.localPosition += offset;
    }
}
