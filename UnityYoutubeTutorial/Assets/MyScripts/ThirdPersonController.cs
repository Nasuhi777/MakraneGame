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

        // Hareket giriþleri
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        if (moveInput.magnitude > 0.1f)
        {
            // Kamera yönüne göre karakter hareket yönünü belirle
            Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
            moveDirection.y = 0; // Y eksenini sýfýrlayarak yere paralel hareket ettir

            // Karakterin dönüþünü yumuþak bir þekilde kamera yönüne çevir
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Hareketi uygula
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Zýplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        // Yerçekimi uygulama
        moveDirection.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}