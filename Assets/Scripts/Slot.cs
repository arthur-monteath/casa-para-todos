using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public enum State
    {
        empty,
        precaria,
        reformando,
        pronta,
        parque
    }

    public Sprite[] sprites;
    private SpriteRenderer sr;

    State state = State.empty;

    private int progression = 0;

    private GameManager manager;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    public void StartRound()
    {
        if (state == State.reformando)
        {
            progression++;

            if(progression == 2)
            {
                ChangeState(State.pronta);
            }
        }
        else
        {
            progression = 0;
        }
    }

    public void TryToBuy(State state)
    {
        int price = manager.priceDictionary[state];

        if (manager.resources >= price)
        {
            manager.resources -= price;

            ChangeState(state);

            UI.SetActive(false);
        }
    }

    public void ChangeState(State state)
    {
        this.state = state;

        switch (state)
        {
            case State.empty:
                sr.sprite = sprites[0];
                break;

            case State.precaria:
                sr.sprite = sprites[1];
                break;

            case State.reformando:
                sr.sprite = sprites[2];
                break;

            case State.pronta:
                sr.sprite = sprites[3];
                break;

            case State.parque:
                sr.sprite = sprites[4];
                break;
        }
    }

    public State GetState()
    {
        return state;
    }

    public GameObject UI;
    public Buyable casa;
    public Text custo;
    public void OnMouseDown()
    {
        switch (state)
        {
            case State.empty:
                casa.SetState(State.precaria);
                casa.transform.GetChild(0).GetComponent<Image>().sprite = sprites[1];
                UI.SetActive(true);
                break;

            case State.precaria:
                casa.SetState(State.reformando);
                casa.transform.GetChild(0).GetComponent<Image>().sprite = sprites[2];
                UI.SetActive(true);
                break;
        }
    }
}
