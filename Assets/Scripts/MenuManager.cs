using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AM
{
    public class MenuManager : MonoBehaviour
    {

        
        public void Tutorial()
        {
            SceneManager.LoadScene("testing ground");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }


        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
