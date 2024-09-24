using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class SlotHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Dictionary<Slot.State, int> GetChildrenStates()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        Dictionary<Slot.State, int> amounts = new Dictionary<Slot.State, int>();

        foreach (var child in children)
        {
            amounts[child.GetComponent<Slot>().GetState()]++;
        }

        return amounts;
    }

    public void StartRound()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            child.GetComponent<Slot>().StartRound();
        }
    }

    public bool HasNoParks()
    {
        return !GetChildrenStates().ContainsKey(Slot.State.parque);
    }

    public float HouseParkRatio()
    {
        var states = GetChildrenStates();

        int amount = states[Slot.State.pronta] - states[Slot.State.parque];

        return amount / 10f;
    }
}
