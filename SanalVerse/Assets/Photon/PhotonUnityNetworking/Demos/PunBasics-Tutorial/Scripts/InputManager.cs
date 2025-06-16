using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public List<InputField> inputFields; // InputField'larýnýzý burada tutabilirsiniz.

    private InputField activeInputField; // Seçili olan InputField

    void Update()
    {
        // Ctrl + V tuþlarýna basýldýðýnda çalýþacak kod.
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
            PasteToActiveInputField(); // Yapýþtýrma iþlemi yalnýzca aktif input field'a yapýlacak
        }

        // Eðer bir InputField týklanýrsa, o input field'ý aktif yapýyoruz
        foreach (InputField inputField in inputFields)
        {
            if (inputField.isFocused)
            {
                activeInputField = inputField;
                break;
            }
        }
    }

    void PasteToActiveInputField()
    {
        if (activeInputField != null)
        {
            string clipboardText = GUIUtility.systemCopyBuffer; // Panodaki metni al

            // Eðer panoda bir þey varsa, aktif input field'a yapýþtýr
            if (!string.IsNullOrEmpty(clipboardText))
            {
                activeInputField.text = clipboardText; // Panodaki veriyi aktif input field'a yapýþtýr
            }
        }
    }
}
