using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData button_press)
    {
        if (button_press.button == PointerEventData.InputButton.Left)
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }
}
