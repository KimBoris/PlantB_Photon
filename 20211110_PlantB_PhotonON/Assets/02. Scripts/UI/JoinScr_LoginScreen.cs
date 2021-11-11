using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JoinScr_LoginScreen : MonoBehaviour
{


    public void LogInSuccess()
    {
        SceneManager.LoadScene("Lobby");
    }
}
