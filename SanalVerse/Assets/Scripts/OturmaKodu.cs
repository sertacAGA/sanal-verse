using UnityEngine; 
using System.Collections;

public class OturmaKodu : MonoBehaviour
{
    public GameObject chair;
    private bool isSitting = false;

    void Update()
    {
        // Check if the character is not sitting
        if (!isSitting)
        {
            // Move the character towards the chair when the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == chair)
                    {
                        // Move the character towards the chair
                        Vector3 chairPosition = new Vector3(chair.transform.position.x, transform.position.y, chair.transform.position.z);
                        transform.LookAt(chairPosition);
                        transform.position = Vector3.MoveTowards(transform.position, chairPosition, 1f * Time.deltaTime);
                    }
                }
            }
        }
        // Check if the character is sitting
        else
        {
            // Make the character stand up when the left mouse button is clicked outside the chair
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != chair)
                    {
                        // Move the character away from the chair
                        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
                        transform.position = newPosition;
                        isSitting = false;
                    }
                }
            }
        }
    }

    // This function is called when the character reaches the chair
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == chair)
        {
            // Make the character sit on the chair
            transform.position = new Vector3(chair.transform.position.x, chair.transform.position.y + 0.5f, chair.transform.position.z);
            transform.rotation = chair.transform.rotation;
            isSitting = true;
        }
    }
}