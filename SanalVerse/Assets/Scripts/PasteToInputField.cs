using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasteToInputFields : MonoBehaviour
{
    public List<InputField> inputFields; // Birden fazla InputField'ý tutacak liste

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
            string clipboardText = GUIUtility.systemCopyBuffer;

            // Eðer panoda metin varsa, her InputField'a yapýþtýr
            if (!string.IsNullOrEmpty(clipboardText))
            {
                foreach (InputField inputField in inputFields)
                {
                    inputField.text = clipboardText;
                }
            }
        }
    }
}
