using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    public float jumpForce = 5f;
    public CharacterController characterController;
    public Transform cameraTransform;

    private Vector3 moveDirection;
    private float gravity = -9.81f;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        // Hareket giri�leri
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        if (moveInput.magnitude > 0.1f)
        {
            // Kamera y�n�ne g�re karakter hareket y�n�n� belirle
            Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
            moveDirection.y = 0; // Y eksenini s�f�rlayarak yere paralel hareket ettir

            // Karakterin d�n���n� yumu�ak bir �ekilde kamera y�n�ne �evir
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Hareketi uygula
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Z�plama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        // Yer�ekimi uygulama
        moveDirection.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}