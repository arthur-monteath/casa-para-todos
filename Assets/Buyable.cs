using UnityEngine;
using UnityEngine.UI;

public class Buyable : MonoBehaviour
{
    public Text label;
    GameManager gameManager;

    public Slot.State state;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SetState(Slot.State state)
    {
        this.state = state;
    }

    public void Buy()
    {
        GetComponentInParent<Slot>().TryToBuy(state);
    }

    // Update is called once per frame
    void Update()
    {
        label.text = gameManager.priceDictionary[state].ToString();
    }
}
