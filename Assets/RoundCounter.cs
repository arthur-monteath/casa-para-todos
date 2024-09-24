using UnityEngine;
using UnityEngine.UI;

public class RoundCounter : MonoBehaviour
{
    GameManager manager;
    public Text label;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        label.text = "Rodada " + manager.GetCurrentRound();
    }
}
