using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour
{
    [SerializeField] private Transform originalPosition;

    private GameObject player;
    private GameObject coin;

    LineRenderer lineRenderer;

    bool isHooking;
    bool wasCoinHooked;

    float hookDistance;
    private bool isDeactivating;

    Rigidbody rb;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lineRenderer = GetComponent<LineRenderer>();
        isHooking = false;
        wasCoinHooked = false;
        hookDistance = 0;
        rb = GetComponent<Rigidbody>();
        isDeactivating = false;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, originalPosition.position);
        lineRenderer.SetPosition(1, transform.position);

        if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire2")) && !isHooking && !wasCoinHooked)
        {
            StartHooking();
        }

        ReturnHook();
        BringCoinTowardsOriginalPosition();
    }

    private void StartHooking()
    {
        isHooking = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * Constants.HOOK_SPEED);
    }

    private void ReturnHook()
    {
        if (isHooking)
        {
            hookDistance = Vector3.Distance(transform.position, originalPosition.position);
            if (hookDistance > Constants.MAX_HOOK_DISTANCE || wasCoinHooked)
            {
                rb.isKinematic = true;
                transform.position = originalPosition.position;
                isHooking = false;
            }
        }
    }

    private void BringCoinTowardsOriginalPosition()
    {
        if (wasCoinHooked && coin != null)
        {
            Vector3 targetPosition = originalPosition.position;
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, targetPosition, Constants.HOOK_SPEED * Time.deltaTime);

            if (Vector3.Distance(coin.transform.position, targetPosition) < 0.1f && !isDeactivating)
            {
                StartCoroutine(DeactivateCoinAfterDelay());
            }
        }
    }

    private IEnumerator DeactivateCoinAfterDelay()
    {
        isDeactivating = true;
        yield return new WaitForSeconds(0.5f);
        CoinManager.Instance.AddCoins(1);
        coin.SetActive(false);
        wasCoinHooked = false;
        coin = null;
        isDeactivating = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            wasCoinHooked = true;
            coin = collider.gameObject;
        }
    }
}
