using UnityEngine;

public class DugmeKodu : MonoBehaviour
{
    private ImgPlayer2[] imgPlayers; // Array to store ImgPlayer2 components

    void Start()
    {
        imgPlayers = FindObjectsOfType<ImgPlayer2>(); // Find all ImgPlayer2 objects in the scene
    }

    public void ForwardButton()
    {
        // Hide the currently active object (assuming they're initially active)
        foreach (ImgPlayer2 imgPlayer in imgPlayers)
        {
            imgPlayer.gameObject.SetActive(false);
        }

        // Find the next active ImgPlayer2 object (loop back to the first if necessary)
        int activeIndex = 0;
        while (activeIndex < imgPlayers.Length && !imgPlayers[activeIndex].gameObject.activeInHierarchy)
        {
            activeIndex++;
        }

        // Show the newly active object
        if (activeIndex < imgPlayers.Length)
        {
            imgPlayers[activeIndex].gameObject.SetActive(true);
        }
    }
}