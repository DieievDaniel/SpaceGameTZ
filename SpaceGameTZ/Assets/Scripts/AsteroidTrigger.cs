using UnityEngine;

public class AsteroidTrigger : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float pushBackForce;
    [SerializeField] private float cameraShakeDuration;
    [SerializeField] private float cameraShakeMagnitude;

    [SerializeField] private float pushBackDuration; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            ShipHealth shipHealth = other.GetComponent<ShipHealth>();
            if (shipHealth != null)
            {
                shipHealth.TakeDamage(damageAmount);
                GameUI.Instance.ShowDamage(damageAmount); 

                Rigidbody shipRigidbody = other.GetComponent<Rigidbody>();
                if (shipRigidbody != null)
                {
                    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                    shipRigidbody.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);

                    StartCoroutine(StopShipMovement(shipRigidbody, pushBackDuration));
                }

                StartCoroutine(CameraShake.Instance.ShakeCamera(cameraShakeDuration, cameraShakeMagnitude));
            }
        }
    }

    private System.Collections.IEnumerator StopShipMovement(Rigidbody shipRigidbody, float duration)
    {
        yield return new WaitForSeconds(duration);

        shipRigidbody.velocity = Vector3.zero;
        shipRigidbody.angularVelocity = Vector3.zero;
    }
}
