using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private AudioClip jumpClip;


    private Camera _mainCamera;

    private Rigidbody _rb;

    // For left, right movement
    private float _movementX;

    // For forward, backward movement
    private float _movementY;

    // This we would use to check whether the ball touches the ground
    private int _groundContact = 0;

    private bool _isJumping = false;    

    private bool IsGrounded => _groundContact > 0;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
        {
            Debug.LogError("Rigid body not added. Ensure child has rigid body");
        }

        if (jumpClip == null)
        {
            Debug.LogError("Add jump audio clip");
        }

        _mainCamera = Camera.main;
    }

    private void OnJump(InputValue jump)
    {

        if (jump.isPressed)
        {            
            _isJumping = true;
        }
    }
    
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Came in contact with ground
        if (collision.gameObject.CompareTag("Ground"))
        {            
            _groundContact++;
        }
    }    

    private void OnCollisionExit(Collision collision)
    {
        // Leaving ground contact
        if (collision.gameObject.CompareTag("Ground"))
        {            
            _groundContact--;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickable pickable = other.GetComponent<IPickable>();

        if (pickable != null) {
            pickable.OnPick();
        }
    }

    private void FixedUpdate()
    {
        // To make the ball move where the camera is facing
        Vector3 cameraForward = _mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = _mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        _moveDirection = (cameraForward * _movementY) + (cameraRight * _movementX);

        // We are only moving left, right, fwd, bckwrd
        _rb.AddForce(_moveDirection * moveSpeed);

        // jump and was grounded
        if (_isJumping && IsGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (jumpClip != null)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            }
        }

        _isJumping = false;
        
    }





}
