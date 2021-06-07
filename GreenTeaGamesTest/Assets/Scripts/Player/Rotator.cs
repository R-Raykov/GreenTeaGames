using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Movement _movement;
    private IEnumerator _rotationCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _movement.OnPress += Rotate;
    }
    private void OnDisable()
    {
        _movement.OnPress -= Rotate;
    }

    /// <summary>
    /// Do the falling rotation
    /// </summary>
    private void Rotate()
    {
        // If the rotating coroutine is active we disable it
        if(_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);

        // Set the rotation to point upwards
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.x = -25;

        transform.rotation = Quaternion.Euler(newRotation);

        // Set the desired final rotation and start the coroutine
        newRotation.x = 90;
        _rotationCoroutine = DoRotation(newRotation, 1.0f);

        StartCoroutine(_rotationCoroutine);
    }

    /// <summary>
    /// Perform the smooth falling rotation
    /// </summary>
    /// <param name="pEndValue"> The end rotation of the player </param>
    /// <param name="pDuration"> The time it takes to get to that end rotation </param>
    /// <returns></returns>
    private IEnumerator DoRotation(Vector3 pEndValue, float pDuration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;
        
        // Wait before starting to fall
        yield return new WaitForSeconds(0.5f);

        while (time < pDuration)
        {
            // Spherically interpolate, feels much better than linear interpolation in this case
            transform.rotation = Quaternion.Slerp(startValue, Quaternion.Euler(pEndValue), time / pDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Make sure the rotation is set to the final value
        transform.rotation = Quaternion.Euler(pEndValue);
    }
}


