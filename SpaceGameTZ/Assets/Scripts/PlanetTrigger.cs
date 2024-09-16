using UnityEngine;

public class PlanetTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject[] objectsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPopup.SetActive(true);
            camera.SetActive(true);
            DeactivateObjects();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void DeactivateObjects()
    {
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}
