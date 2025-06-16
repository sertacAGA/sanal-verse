using UnityEngine;

public class ChairInteraction : MonoBehaviour
{
    public GameObject character;
    public Transform sittingPosition;
    public Animator characterAnimator;

    private bool isSitting = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            character = GameObject.FindGameObjectWithTag("Player"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (Physics.Raycast(ray, out hit) && character != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    float distanceToChair = Vector3.Distance(character.transform.position, sittingPosition.position);

                    if (distanceToChair <= 1f) // Buradaki deðeri istediðiniz mesafeye ayarlayabilirsiniz
                    {
                        if (isSitting)
                        {
                            // Karakter sandalyeden kalkacak
                            StandUp();
                        }
                        else
                        {
                            // Karakter sandalyeye oturacak
                            SitDown();
                        }
                    }
                }
            }
        }
    }

    private void SitDown()
    {
        character.transform.position = sittingPosition.position;
        character.transform.rotation = sittingPosition.rotation;
        characterAnimator.SetBool("isSitting", true);
        isSitting = true;
    }

    private void StandUp()
    {
        characterAnimator.SetBool("isSitting", false);
        isSitting = false;
    }
}
