using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] Transform cameraTarget;
    [SerializeField] MonoBehaviour[] localOnlyScripts;
    [SerializeField] CharacterController cc;

    public bool isLocal { get { return isLocalPlayer; } }
    [HideInInspector] public bool cursorLocked = true;

    private void Start()
    {
        if (!isLocal)
        {
            for (int i = 0; i < localOnlyScripts.Length; i++)
            {
                Destroy(localOnlyScripts[i]);
            }

            Destroy(cc);
        }
        else
        {
            CameraSetter.getInstance.SetTarget(cameraTarget);
        }
    }

    private void Update()
    {
        if (!isLocal)
        {
            return;
        }

        UpdateCursor(cursorLocked);
    }

    private void UpdateCursor(bool _lock)
    {
        Cursor.visible = !_lock;
        Cursor.lockState = _lock ? CursorLockMode.Confined : CursorLockMode.None;
    }
}