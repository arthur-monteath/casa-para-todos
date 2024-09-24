using UnityEngine;
using UnityEngine.SceneManagement;
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

    bool ending = false;
    public void HideMessage()
    {
        if(ending) SceneManager.LoadScene(0);

        body.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void ShowMessage(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;

        body.SetActive(true);

        Time.timeScale = 0f;
    }

    public void SendEnding(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;

        body.SetActive(true);

        ending = true;
    }

    
}
