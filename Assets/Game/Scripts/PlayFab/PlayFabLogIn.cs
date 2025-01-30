using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayFabLogIn : MonoBehaviour
{

    #region SignUp
    void SignUp()
    {
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
       
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = "asd@asd.com",
            Password = "qweasd123",
            //Username = Username,
            //DisplayName = Username, // Set the DisplayName
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnSignUpSuccess, OnLoginFailure);

    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion


    #region SignIn
    [ContextMenu("SignIn")]
    void LogIn()
    {
        var request = new LoginWithEmailAddressRequest { };
        request.Email = "asd@asd.com";
        request.Password = "qweasd123";

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login bien");
    }

    #endregion



    [ContextMenu("ChangeMail")]
    public void ChangeEmail()
    {
        var request = new AddOrUpdateContactEmailRequest { };
        request.EmailAddress = "qwe@qwe.com";

        PlayFabClientAPI.AddOrUpdateContactEmail(request, OnChangeMailSuccess, OnChangeMailFailed);
    }
    void OnChangeMailSuccess(AddOrUpdateContactEmailResult r)
    {
        Debug.Log("Bien");
    }
    void OnChangeMailFailed(PlayFabError r)
    {
        Debug.Log("Mal");
    }


}
