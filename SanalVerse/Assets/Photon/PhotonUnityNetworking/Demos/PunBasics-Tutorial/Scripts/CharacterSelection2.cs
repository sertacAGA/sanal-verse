using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection2 : MonoBehaviour
{
    public List<GameObject> maleStudents;
    public List<GameObject> femaleStudents;
    public List<GameObject> maleTeachers;
    public List<GameObject> femaleTeachers;

    private List<GameObject> currentCharacterList;
    private int index;
    private string selectedGender;
    private bool isStudent;

    private void Start()
    {
        selectedGender = PlayerPrefs.GetString("SelectedGender", "Male");
        isStudent = PlayerPrefs.GetInt("IsStudent", 1) == 1; // 1 ise öðrenci, 0 ise öðretmen
        index = PlayerPrefs.GetInt("CharacterSelected", 0);

        UpdateCharacterList();
        ShowSelectedCharacter();
    }

    public void SetGender(string gender)
    {
        selectedGender = gender;
        PlayerPrefs.SetString("SelectedGender", gender);
        index = 0;
        PlayerPrefs.SetInt("CharacterSelected", index);

        UpdateCharacterList();
        ShowSelectedCharacter();
    }

    private void UpdateCharacterList()
    {
        if (isStudent)
        {
            currentCharacterList = selectedGender == "Male" ? maleStudents : femaleStudents;
        }
        else
        {
            currentCharacterList = selectedGender == "Male" ? maleTeachers : femaleTeachers;
        }

        // Tüm karakterleri deaktif et
        DeactivateAllCharacters();

        // Seçili kategorideki karakterleri aktif et
        if (currentCharacterList.Count > 0)
        {
            currentCharacterList[index].SetActive(true);
        }
    }

    private void DeactivateAllCharacters()
    {
        foreach (GameObject character in maleStudents)
        {
            character.SetActive(false);
        }
        foreach (GameObject character in femaleStudents)
        {
            character.SetActive(false);
        }
        foreach (GameObject character in maleTeachers)
        {
            character.SetActive(false);
        }
        foreach (GameObject character in femaleTeachers)
        {
            character.SetActive(false);
        }
    }

    private void ShowSelectedCharacter()
    {
        foreach (GameObject character in currentCharacterList)
        {
            character.SetActive(false);
        }
        if (currentCharacterList.Count > 0)
        {
            currentCharacterList[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        currentCharacterList[index].SetActive(false);
        index = (index - 1 + currentCharacterList.Count) % currentCharacterList.Count;
        currentCharacterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        currentCharacterList[index].SetActive(false);
        index = (index + 1) % currentCharacterList.Count;
        currentCharacterList[index].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        // SceneManager.LoadScene("Oyun");
    }
}
