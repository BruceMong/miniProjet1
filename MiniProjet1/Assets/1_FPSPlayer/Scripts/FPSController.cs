using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class FPSController : MonoBehaviour
{
    #region public
    [Header("Player values")]
    public float WalkSpeed = 7.5f;
    public float RunningSpeed = 11.5f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;
    [Header("Camera values")]
    public float LookSpeed = 2.0f;
    public float LookXLimit = 45.0f;
    #endregion


    #region private
    private Vector3 m_v3MoveDirection = Vector3.zero;
    private float m_fRotationX = 0;
    private CharacterController m_characterController;
    private Camera m_cPlayerCamera;
    public bool m_bCanMove = true;
    private Vector2 m_v2Look;
    private Vector2 m_v2Move;
    private Vector2 m_v2Rotation;
    private bool m_bIsJump = false;
    private bool m_bIsRunning = false;
    #endregion
    
    

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_cPlayerCamera = GetComponentInChildren<Camera>();

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_v2Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_v2Move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                m_bIsJump = true;
                break;
            case InputActionPhase.Canceled:
                m_bIsJump = false;
                break;
        }
    }
    public void OnRunning(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                m_bIsRunning = true;
                break;
            case InputActionPhase.Canceled:
                m_bIsRunning = false;
                break;
        }
    }
    
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        float curSpeedX = m_bCanMove ? (m_bIsRunning ? RunningSpeed: WalkSpeed) * m_v2Move.y : 0;
        float curSpeedY = m_bCanMove ? (m_bIsRunning ? RunningSpeed: WalkSpeed) * m_v2Move.x : 0;
        float movementDirectionY = m_v3MoveDirection.y;
        m_v3MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (m_bIsJump && m_bCanMove && m_characterController.isGrounded)
        {
            m_v3MoveDirection.y = JumpSpeed;
        }
        else
        {
            m_v3MoveDirection.y = movementDirectionY;
        }
        
        if (!m_characterController.isGrounded)
        {
            m_v3MoveDirection.y -= Gravity * Time.deltaTime;
        }

        m_characterController.Move(m_v3MoveDirection * Time.deltaTime);
        
        if (m_bCanMove)
        {
            m_fRotationX += -m_v2Look.y * LookSpeed;
            m_fRotationX = Mathf.Clamp(m_fRotationX, -LookXLimit, LookXLimit);
            m_cPlayerCamera.transform.localRotation = Quaternion.Euler(m_fRotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, m_v2Look.x * LookSpeed, 0);
        }
    }
    
}
