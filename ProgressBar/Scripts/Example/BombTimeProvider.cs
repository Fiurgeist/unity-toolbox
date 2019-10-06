using UnityEngine;

public class BombTimeProvider: AbstractProgressProvider {
    public Bomb bomb;

    void OnEnable() {
        if(bomb == null) {
            return;
        }
        Init();
    }

    void OnDisable() {
        if(bomb == null) {
            return;
        }
        RemoveListeners();
    }

    private void Start() {
        if(bomb == null) {
            return;
        }
        UpdateTime();
    }

    void Init() {
        maxValue = bomb.time;
        bomb.timerEvent.AddListener(UpdateTime);
    }

    void RemoveListeners() {
        bomb.timerEvent.RemoveListener(UpdateTime);
    }

    public void UpdateTime() {
        value = bomb.time;
        OnChanged();
    }
}

