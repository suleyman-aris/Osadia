using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("PlayerController")]
    [SerializeField] public Transform Camera;
    [SerializeField, Range(1, 10)] float walkingSpeed = 6.0f;
    [Range(0.1f, 5)] public float CrouchSpeed = 3.0f;
    [SerializeField, Range(2, 20)] float RuningSpeed = 9.0f;
    [SerializeField, Range(0, 20)] float jumpSpeed = 7.0f;
    [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
    [SerializeField, Range(10, 120)] float lookXLimit = 90.0f;
    
    [Space(20)]
    [Header("Advance")]
    [SerializeField] float RunningFOV = 80.0f;
    [SerializeField] float SpeedToFOV = 4.0f;
    [SerializeField] float CrouchHeight = 1.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] float timeToRunning = 2.0f;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool CanRunning = true;

    [Space(20)]
    [Header("WallRunning")]
    [SerializeField] bool CanWallRunning = true;
    [SerializeField, Range(1, 25)] float Speed = 2f;
    bool isWallRunning = false;

    [Space(20)]
    [Header("Input")]
    [SerializeField] KeyCode CrouchKey = KeyCode.LeftControl;

    [HideInInspector] public UnityEngine.CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    
    bool isCrouch = false;
    float InstallCrouchHeight;
    float rotationX = 0;
    
    [HideInInspector] public bool isRunning = false;
    
    Vector3 InstallCameraMovement;
    float InstallFOV;
    Camera cam;
    
    [HideInInspector] public bool Moving;
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float Lookvertical;
    [HideInInspector] public float Lookhorizontal;
    
    float RunningValue;
    float installGravity;
    bool WallDistance;
    
    [HideInInspector] public float WalkingValue;
    
    void Start()
    {
        characterController = GetComponent<UnityEngine.CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InstallCrouchHeight = characterController.height;
        InstallCameraMovement = Camera.localPosition;
        InstallFOV = cam.fieldOfView;
        RunningValue = RuningSpeed;
        installGravity = gravity;
        WalkingValue = walkingSpeed;
    }

    void Update()
    {
        RaycastHit CrouchCheck;
        RaycastHit ObjectCheck;

        if (!characterController.isGrounded && !isWallRunning)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        isRunning = !isCrouch ? CanRunning ? Input.GetKey(KeyCode.LeftShift) : false : false;
        vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
        horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;
        if (isRunning) RunningValue = Mathf.Lerp(RunningValue, RuningSpeed, timeToRunning * Time.deltaTime);
        else RunningValue = WalkingValue;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * vertical) + (right * horizontal);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isWallRunning)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

        if (Cursor.lockState == CursorLockMode.Locked && canMove)
        {
            Lookvertical = -Input.GetAxis("Mouse Y");
            Lookhorizontal = Input.GetAxis("Mouse X");

            rotationX += Lookvertical * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

            if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
            else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
        }

        if (Input.GetKey(CrouchKey))
        {
            isCrouch = true;
            float Height = Mathf.Lerp(characterController.height, CrouchHeight, 5 * Time.deltaTime);
            characterController.height = Height;
            WalkingValue = Mathf.Lerp(WalkingValue, CrouchSpeed, 6 * Time.deltaTime);

        }
        else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CrouchCheck, 0.8f, 1))
        {
            if (characterController.height != InstallCrouchHeight)
            {
                isCrouch = false;
                float Height = Mathf.Lerp(characterController.height, InstallCrouchHeight, 6 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" && CanWallRunning)
        {
            //
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall" && CanWallRunning)
        {
            //
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall" && CanWallRunning)
        {
            //
        }
    }

}