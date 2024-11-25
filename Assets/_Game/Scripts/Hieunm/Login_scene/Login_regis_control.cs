using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login_regis_control : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject forgotPanel;
    public GameObject confirmPanel;
    public GameObject homePanel;
    public GameObject settingsPanel;





    void Start()
    {
        ShowLogin();
    }

    public void ShowLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(false);
        confirmPanel.SetActive(false);
        homePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ShowForgot()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(true);
    }

    public void ShowConfirm()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        forgotPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void ShowHome()
    {
        homePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        homePanel.SetActive(true);
    }
}
