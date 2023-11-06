using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = SOArchitectureDirectories.EVENT_SUBMENU + "Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> listeners = new();
    private readonly List<Action> actions = new();

    private List<GameEventListener> temporaryListeners = new();
    private List<Action> temporaryActions = new();

    [Sirenix.OdinInspector.Button("Raise")]
    public void Raise()
    {
        temporaryListeners.Clear();
        temporaryActions.Clear();

        temporaryListeners.AddRange(listeners);
        temporaryActions.AddRange(actions);

        for (int i = 0; i < temporaryListeners.Count; i++)
            temporaryListeners[i].OnEventRaised();

        for (int i = 0; i < temporaryActions.Count; i++)
            temporaryActions[i].Invoke();
    }

    public void AddListener(GameEventListener listener) => listeners.Add(listener);
    public void AddListener(Action action) => actions.Add(action);

    public void RemoveListener(GameEventListener listener) => listeners.Remove(listener);
    public void RemoveListener(Action action) => actions.Remove(action);
}