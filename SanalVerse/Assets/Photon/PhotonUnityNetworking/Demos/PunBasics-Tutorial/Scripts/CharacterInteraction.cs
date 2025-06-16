using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float walkingSpeed = 2f; // Yürüme hýzý
    public Transform sittingPosition; // Oturma pozisyonu
    private Animator animator; // Animator referansý
    private bool isWalking = false; // Yürüme durumu
    private bool isSitting = false; // Oturma durumu

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isWalking)
        {
            // Hedefe doðru yürüme iþlemi
            Vector3 targetDirection = sittingPosition.position - transform.position;
            targetDirection.y = 0f; // Y yönünde hareket etmesini engelle
            transform.rotation = Quaternion.LookRotation(targetDirection);
            transform.position += targetDirection.normalized * walkingSpeed * Time.deltaTime;

            // Hedefe yaklaþtýðýnda oturma animasyonunu oynat
            if (Vector3.Distance(transform.position, sittingPosition.position) <= 0.1f)
            {
                isWalking = false;
                isSitting = true;
                animator.SetBool("IsSitting", true); // Oturma animasyonunu baþlat
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oturabilir") && !isWalking && !isSitting)
        {
            isWalking = true;
        }
    }
}