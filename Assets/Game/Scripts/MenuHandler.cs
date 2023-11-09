using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public void GoToScene( string sceneName ) {

        SceneManager.LoadScene( sceneName );

    }

    public void QuitGame( ) {

        Application.Quit( );

    }

}
