using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb: MonoBehaviour {
    public float time;

    public UnityEvent timerEvent;

    void Awake() {
        if(timerEvent == null) {
            timerEvent = new UnityEvent();
        }
    }

    void Update() {
        float delta = Time.smoothDeltaTime;

        if(time > 0) {
            time -= delta;
            OnTimeChanged();
        }
    }

    void OnTimeChanged() {
        timerEvent.Invoke();
    }
}
