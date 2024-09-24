using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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

    private void ChangeState(State state)
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
    public Text custo;
    public void OnMouseDown()
    {
        switch (state)
        {
            case State.empty:
                custo.text = "6";
                UI.GetComponentInChildren<Image>().sprite = sprites[1];
                break;

            case State.precaria:
                custo.text = "8";
                UI.GetComponentInChildren<Image>().sprite = sprites[2];
                break;
        }

        UI.GetComponentInChildren<Image>().sprite = sprites[1];

        UI.SetActive(true);
    }
}
