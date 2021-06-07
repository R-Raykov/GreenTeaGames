using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool _triggeredOnce = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Thought abouot using a raycast based collision system but this seems faster than having a constant raycast check for up, down and forward

        if (collision.transform.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_triggeredOnce && other.transform.CompareTag("ScoreArea"))
        {
            ScoreManager.Instance.UpdateScore();
            _triggeredOnce = true;
            StartCoroutine(IgnoreSecondTrigger());
        }
    }

    /// <summary>
    /// Ignores the second trigger on the gameobject, I use 2 triggers to improve the feeling of the collisions. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator IgnoreSecondTrigger()
    {
        yield return new WaitForSeconds(1.0f);
        _triggeredOnce = false;
    }
}
