﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Register : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject confirmPassword;
    public GameObject currentPanel;
    public GameObject nextPanel;
    public GameObject failedPanel;
    public Button enter;
    private string Username;
    private string Email;
    private string Password;
    private string ConfirmPassword;
    private string form;
    private bool Valid = false;


    //Update is called once per frame
    public void ToggleOnClick()
    {
        Button toUpdate = enter.GetComponent<Button>();
        if (password.GetComponent<InputField>().text != "" &&
            email.GetComponent<InputField>().text != "" &&
            confirmPassword.GetComponent<InputField>().text != "")
        {
            Valid = true;
        }

        if (password.GetComponent<InputField>().text != confirmPassword.GetComponent<InputField>().text) {
            Valid = false;
        }

        if (Valid == true)
        {
            Email = email.GetComponent<InputField>().text;
            Password = password.GetComponent<InputField>().text;
            ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);

            UserInfo userInfo = new UserInfo();
            userInfo.newUserInfo(Email, Password);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/userInfo.data");

            bf.Serialize(file, userInfo);
            file.Close();
        }
        else
        {
            currentPanel.SetActive(false);
            failedPanel.SetActive(true);
        }
    }

        

}



[Serializable]
class UserInfo
{
    public Dictionary<string, string> userData;

    public UserInfo()
    {
        this.userData = new Dictionary<string, string>();
    }

    public void newUserInfo(string username, string password)
    {
        this.userData.Add(username, password);
    }
}



