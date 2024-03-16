using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCam;
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float runSpeed = 7f;
    public float jumpPower = 7;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDir = Vector3.zero;
    float rotX = 0;

    public bool canMove = true;
    public bool isMoving;

    CharacterController characterController;

    [Header("Headbob")]
    public bool isCrouching;
    public bool isSprinting;
    public float walkBobSpeed;
    public float walkBobAmount;
    public float crouchBobSpeed;
    public float crouchBobAmount;
    public float sprintBobSpeed;
    public float sprintBobAmount;
    private float defaultYPos = 0;
    private float timer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHeadbob();

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isSprinting ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isSprinting ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float moveDirY = moveDir.y;
        moveDir = (forward * curSpeedX) + (right * curSpeedY);

        if (moveDir != Vector3.zero)
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDir.y = jumpPower;
        }

        else
        {
            moveDir.y = moveDirY;
        }

        if (!characterController.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * Time.deltaTime);

        if (canMove)
        {
            rotX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        if (Mathf.Abs(moveDir.x) > 0.1f || Mathf.Abs(moveDir.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCam.transform.localPosition = new Vector3(
                playerCam.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount),
                playerCam.transform.localPosition.z);
        }
    }
}
