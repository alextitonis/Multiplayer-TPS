using Cinemachine;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    public static CameraSetter getInstance;
    private void Awake()
    {
        getInstance = this;
    }

    [SerializeField] CinemachineVirtualCamera cam;

    public void SetTarget(Transform target)
    {
        cam.Follow = target;
    }
}