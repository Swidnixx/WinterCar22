using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera vcam;

    int activePosition = 0;

    private void Start()
    {
        if (positions.Length == 0) return;

        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
            = positions[activePosition];
    }

    private void Update()
    {
        if (positions.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            activePosition++;
            activePosition = activePosition % positions.Length;
            vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
                = positions[activePosition];
        }
    }
}
