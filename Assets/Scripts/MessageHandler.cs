using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    public GameObject body;

    public Text title;
    public Text message;

    // Update is called once per frame
    void Update()
    {

    }

    public void HideMessage()
    {
        body.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void ShowMessage(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;

        body.SetActive(false);

        Time.timeScale = 1.0f;
    }
}
