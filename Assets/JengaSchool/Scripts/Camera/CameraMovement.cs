using System;
using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float zoomSensitivity = 15f;
    [SerializeField] private int zoomOutMin;
    [SerializeField] private int zoomOutMax;

    private bool movementLocked;
    private float defaultFieldOfView;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 _center = Vector3.zero;
    public Vector3 Center
    {
        get => _center;
        set
        {
            bool changed = _center != value ||
                           transform.rotation != defaultRotation ||
                           ReferenceManager.Instance.MainCamera.fieldOfView != defaultFieldOfView;
            _center = value;
            
            if (changed)
            {
                StopAllCoroutines();
                StartCoroutine(MoveSmoothly(new Vector3(defaultPosition.x, defaultPosition.y, _center.z)));
            }
        }
    }

    private void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultFieldOfView = ReferenceManager.Instance.MainCamera.fieldOfView;
    }

    private void Update()
    {
        if (!movementLocked && Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(Center, Vector3.up, mouseX);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity);
        }
    }

    private void Zoom(float increment)
    {
        Camera mainCamera = ReferenceManager.Instance.MainCamera;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - increment, zoomOutMin, zoomOutMax);
    }

    private IEnumerator MoveSmoothly(Vector3 targetPosition)
    {
        movementLocked = true;
        float transitionDuration = 0.5f;
        float moveTimer = 0;
        while (true)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > transitionDuration)
            {
                transform.position = targetPosition;
                defaultRotation = transform.rotation;
                ReferenceManager.Instance.MainCamera.fieldOfView = defaultFieldOfView;
                break;
            }

            float step = moveTimer / transitionDuration;
            transform.position = Vector3.Lerp(transform.position, targetPosition, step);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, step);
            ReferenceManager.Instance.MainCamera.fieldOfView =
                Mathf.Lerp(ReferenceManager.Instance.MainCamera.fieldOfView, defaultFieldOfView, step);
            yield return null;
        }
        movementLocked = false;
    }
}