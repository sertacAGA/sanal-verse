using UnityEngine;
using UnityEngine.UI;

public class LinkInput : MonoBehaviour
{
    public InputField imageLinkInput;
    public InputField videoLinkInput;

    public void SaveLinks()
    {
        string imageLink = imageLinkInput.text;
        string videoLink = videoLinkInput.text;

        PlayerPrefs.SetString("ImageLink", imageLink);
        PlayerPrefs.SetString("VideoLink", videoLink);
        PlayerPrefs.Save();
    }
}
