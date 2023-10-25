using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the camera movement and allows the user to switch focus between the 3 stacks.
/// </summary>
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform[] stacks;  // Array to hold the transforms of the 3 stacks
    public float orbitSpeed = 10f;  // Speed of the orbiting movement
    public float switchSpeed = .8f;  // Speed of the switching movement between stacks
    public Vector3 offset = new Vector3(0, 5, 10);  // Offset from the focused stack

    private Transform target;  // Current target stack
    private int targetIndex;  // Index of the current target stack
    private bool isSwitching;  // Flag to check if we are switching between stacks

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        targetIndex = 1;
        target = stacks[targetIndex];
        SwitchFocus(0);
    }

    void Update()
    {
        // Rotate around the focused stack on left mouse button drag
        if (Input.GetMouseButton(0) && !isSwitching)
        {
            float horizontal = Input.GetAxis("Mouse X") * orbitSpeed;
            transform.RotateAround(target.position, Vector3.up, horizontal);
        }

        // Switch focus between stacks on number key press
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchFocus(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchFocus(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchFocus(2);
        }
    }

    // Switch focus to the stack at the given index
    void SwitchFocus(int index)
    {
        if (index != targetIndex && index >= 0 && index < stacks.Length)
        {
            targetIndex = index;
            target = stacks[targetIndex];
            isSwitching = true;
            StopAllCoroutines();
            StartCoroutine(Switching());
        }
    }

    // Coroutine to move the camera to the new focused stack
    IEnumerator Switching()
    {
        while (Vector3.Distance(transform.position, target.position + offset) > 0.1f)
        {
            // Lerp the position and rotation for a smooth transition
            transform.position = Vector3.Lerp(transform.position, target.position + offset, switchSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, switchSpeed * Time.deltaTime);
            yield return null;
        }
        isSwitching = false;  // End switching
    }

    public int GetTargetIndex()
    {
        return targetIndex;
    }
}
