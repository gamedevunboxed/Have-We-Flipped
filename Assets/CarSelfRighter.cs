using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarSelfRighter : MonoBehaviour
{
    [Tooltip("Time in seconds before car is auto reset after getting flipped")]
    [SerializeField]
    private float _autoResetTimer = 3.0f;

    private CarController _carController;

    private bool _isResetting = false;

    private const float SelfRightAngle = 180.0f;

    private void Start()
    {
        _carController = GetComponent<CarController>();
    }

    private void Update()
    {
        if(_carController.CurrentSpeed < 1.0f)
        {
            if(IsUpsideDown() && !_isResetting )
            {
                _isResetting = true;
                StartCoroutine(SelfRight());
            }
        }
    }

    private bool IsUpsideDown()
    {
        return (Vector3.Dot(transform.up, Vector3.up) < -0.9f   );
    }

    private IEnumerator SelfRight()
    {
        yield return new WaitForSeconds(_autoResetTimer);

        Quaternion zRotation = Quaternion.Euler(0.0f, 0.0f, SelfRightAngle);
        transform.rotation = transform.rotation * zRotation;
    }
}