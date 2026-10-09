using System;
using UnityEngine;

public class SitDetector : MonoBehaviour
{
    public delegate void SitAction(Vector3 exactPosition, Vector3 exactForward);
    public event SitAction OnSitPositionDetected;

    // THÊM BIẾN NÀY: Để "nhớ" cái ghế vừa chạm vào
    public Transform currentSitPoint { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SitPoint"))
        {
            currentSitPoint = other.transform;
            OnSitPositionDetected?.Invoke(other.transform.position, other.transform.forward);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SitPoint") && currentSitPoint == other.transform)
        {
            currentSitPoint = null; // Quên cái ghế đi khi đứng lên rời đi
        }
    }
    public void ClearSitPoint()
    {
        currentSitPoint = null;
    }
}
