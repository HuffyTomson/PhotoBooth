using UnityEngine;
using System.Collections;
using Huffy.Utilities;

public class TwilioManager : SingletonBehaviour<TwilioManager>
{
    string url = "api.twilio.com/2010-04-01/Accounts/";
    string service = "/Messages.json";
    public string from;
    public string to;
    public string account_sid;
    public string auth;
    public string body;

    public static void SendSMS()
    {
        WWWForm form = new WWWForm();
        form.AddField("To", Instance.to);
        form.AddField("From", Instance.from);
        //string bodyWithoutSpace = body.Replace (" ", "%20");//Twilio doesn't need this conversion
        form.AddField("Body", Instance.body);
        string completeurl = "https://" + Instance.account_sid + ":" + Instance.auth + "@" + Instance.url + Instance.account_sid + Instance.service;
        Debug.Log(completeurl);
        WWW www = new WWW(completeurl, form);
        Instance.StartCoroutine(Instance.WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok! SMS sent through Web API: " + www.text);
        }
        else {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}