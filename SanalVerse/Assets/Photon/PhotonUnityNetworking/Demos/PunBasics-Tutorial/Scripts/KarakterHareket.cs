using UnityEngine;

public class KarakterHareket : MonoBehaviour
{
    private Animator animator;
    private bool isSitting = false;
    private Transform sittingPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sittingPosition = GameObject.Find("SittingPosition").transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSitting)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Oturabilir"))
                {
                    animator.SetTrigger("Sit");
                    isSitting = true;

                    // Karakteri SittingPosition nesnesine hareket ettir
                    transform.position = sittingPosition.position;
                    transform.rotation = sittingPosition.rotation;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && isSitting)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Oturabilir"))
                {
                    animator.SetTrigger("StandUp");
                    isSitting = false;
                }
            }
        }
    }
}
