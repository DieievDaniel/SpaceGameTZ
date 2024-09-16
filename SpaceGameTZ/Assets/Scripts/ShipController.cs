using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float hoverSpeed;
    [SerializeField] private float forwardAcceleration;
    [SerializeField] private float strafeAcceleration;
    [SerializeField] private float hoverAcceleration;

    [Header("Look Settings")]
    [SerializeField] private float lookRateSpeed;

    [Header("Roll Settings")]
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollAcceleration;

    private Vector2 screenCenter;
    private Vector2 mouseDistance;

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float rollInput;

    private void Start()
    {
        InitializeScreenCenter();
        ConfigureCursor();
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        ApplyMovement();
    }

    private void InitializeScreenCenter()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void ConfigureCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void HandleLook()
    {
        Vector2 mousePosition = Input.mousePosition;
        mouseDistance.x = (mousePosition.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (mousePosition.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        float joystickLookX = Input.GetAxis("Mouse X") + Input.GetAxis("Right Stick Horizontal");
        float joystickLookY = Input.GetAxis("Mouse Y") + Input.GetAxis("Right Stick Vertical");

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll") + Input.GetAxis("Right Stick Roll"), rollAcceleration * Time.deltaTime);

        float rotationX = (mouseDistance.y + joystickLookY) * lookRateSpeed * Time.deltaTime;
        float rotationY = (mouseDistance.x + joystickLookX) * lookRateSpeed * Time.deltaTime;
        float rotationZ = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(-rotationX, rotationY, rotationZ, Space.Self);
    }


    private void HandleMovement()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");
        float hoverInput = Input.GetAxisRaw("Hover");

        float joystickForwardInput = Input.GetAxis("Left Stick Vertical");
        float joystickStrafeInput = Input.GetAxis("Left Stick Horizontal");
        float joystickHoverInput = Input.GetAxis("Right Trigger") - Input.GetAxis("Left Trigger");

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardInput * forwardSpeed + joystickForwardInput * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, strafeInput * strafeSpeed + joystickStrafeInput * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, hoverInput * hoverSpeed + joystickHoverInput * hoverSpeed, hoverAcceleration * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        Vector3 forwardMovement = transform.forward * activeForwardSpeed * Time.deltaTime;
        Vector3 strafeMovement = transform.right * activeStrafeSpeed * Time.deltaTime;
        Vector3 hoverMovement = transform.up * activeHoverSpeed * Time.deltaTime;

        transform.position += forwardMovement + strafeMovement + hoverMovement;
    }
}
