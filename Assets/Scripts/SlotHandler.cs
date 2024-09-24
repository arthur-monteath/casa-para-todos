using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Enchente()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.GetComponent<Slot>() == null) continue;
            var key = child.GetComponent<Slot>().GetState();
            if (key == Slot.State.precaria) child.GetComponent<Slot>().ChangeState(Slot.State.empty);
        }
    }

    public Dictionary<Slot.State, int> GetChildrenStates()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        Dictionary<Slot.State, int> amounts = new Dictionary<Slot.State, int>();

        foreach (var child in children)
        {
            if (child.GetComponent<Slot>() == null) continue;
            var key = child.GetComponent<Slot>().GetState();
            if (amounts.ContainsKey(key))
                amounts[key]++;
            else
            {
                amounts[key] = 1;
            }
        }

        return amounts;
    }

    public void StartRound()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            child.GetComponent<Slot>()?.StartRound();
        }
    }

    public bool HasNoParks()
    {
        return !GetChildrenStates()?.ContainsKey(Slot.State.parque) ?? false;
    }

    public float HouseParkRatio()
    {
        var states = GetChildrenStates();

        int prontas = 0, parques = 0;

        if (states?.ContainsKey(Slot.State.pronta) ?? false)
            prontas = states[Slot.State.pronta];

        if (states?.ContainsKey(Slot.State.parque) ?? false)
            parques = states[Slot.State.parque];

        int amount = prontas - parques;

        return amount / 10f;
    }
}
