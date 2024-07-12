using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    [SerializeField] private SceneAsset firstScene;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
     private GameObject currentMenu;
    // Start is called before the first frame update
    void Awake()
    {
        Reference.canvas = gameObject;
        currentMenu = mainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(currentMenu == mainMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu(mainMenu);
            }
        }
    }

    public void HideMenu()
    {
        if(currentMenu != null){
            currentMenu.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        currentMenu = menuToShow;
        if (menuToShow != null)
        {
           menuToShow.SetActive(true);
            Time.timeScale = 0;
         }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(firstScene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
