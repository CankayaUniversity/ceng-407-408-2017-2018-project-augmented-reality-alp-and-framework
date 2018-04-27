using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject login;
    public GameObject warningText;
    public GameObject errorText;
    public Button btnLogin;

    protected PERSON user = new PERSON();

    private string myEmail;
    private string myPassword;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //btnLogin = login.GetComponent<Button>();
        //btnLogin.onClick.AddListener(validateLogin);
    }

    private bool validateLogin()
    {
        warningText.SetActive(false);
        errorText.SetActive(false);
        myEmail = email.GetComponent<InputField>().text;
        myPassword = password.GetComponent<InputField>().text;

        if (myEmail == "" || myPassword == "")
        {
            warningText.SetActive(true);
            return false;
        }
        else
        {
            DatabaseProcessor database = new DatabaseProcessor();
            DataTable userData;
            string loginQuery = user.generatePersonQuery(myEmail, myPassword);
            database.Connect();
            userData = database.GetData(loginQuery);
            if (userData.Rows.Count == 1)
            {
                user.PersonID1 = int.Parse(userData.Rows[0]["PersonID"].ToString());
                user.Name1 = userData.Rows[0]["Name"].ToString();
                user.Surname1 = userData.Rows[0]["Surname"].ToString();
                user.OutlookMail1 = userData.Rows[0]["OutlookMail"].ToString();
                user.Password1 = userData.Rows[0]["Password"].ToString();
                user.Title1 = userData.Rows[0]["Title"].ToString();
                user.Department1 = userData.Rows[0]["Department"].ToString();
                user.Team1 = userData.Rows[0]["Team"].ToString();
                user.Speciality1 = userData.Rows[0]["Speciality"].ToString();
                user.PersonalInfo1 = userData.Rows[0]["PersonalInfo"].ToString();
                user.ARFotoName1 = userData.Rows[0]["ARFotoName"].ToString();
                return true;
            }
            else
                email.GetComponent<InputField>().text = "";
                myPassword = password.GetComponent<InputField>().text = "";
                errorText.SetActive(true);
                return false;
        }
    }

    public void LoginButton()
    {
        bool success = validateLogin();
        if (success == true)
            SceneManager.LoadScene("MainMenu");
    }
}

