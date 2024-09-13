using UnityEngine;

public class HookShot : MonoBehaviour
{
    [SerializeField] private Transform originalPosition; 

    private GameObject player;
    private GameObject coin;

    LineRenderer lineRenderer;

    bool isHooking;
    bool wasCoinHooked;

    float hookDistance;

    Rigidbody rb;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lineRenderer = GetComponent<LineRenderer>();
        isHooking = false;
        wasCoinHooked = false;
        hookDistance = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, originalPosition.position);
        lineRenderer.SetPosition(1, transform.position);

        if (Input.GetMouseButtonDown(1) && !isHooking && !wasCoinHooked)
        {
            StartHooking();
        }
        ReturnHook();
        BringCoinTowardsPlayer();
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

    private void BringCoinTowardsPlayer()
    {
        if (wasCoinHooked)
        {
            Vector3 finalPosition = new Vector3(originalPosition.position.x, coin.transform.position.y, originalPosition.position.z + Constants.ENEMY_Z_OFFSET);
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, finalPosition, Constants.MAX_HOOK_DISTANCE);
            wasCoinHooked = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Coin"))
        {
            wasCoinHooked =true;
            coin = collider.gameObject;
        }
    }
}
