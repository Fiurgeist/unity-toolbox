using UnityEngine;

namespace Localization {
    public class BombTimeProvider: AbstractVariableTextContent {
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
            UpdateTime();
        }

        void Init() {
            bomb.timerEvent.AddListener(UpdateTime);
        }

        void RemoveListeners() {
            bomb.timerEvent.RemoveListener(UpdateTime);
        }

        public void UpdateTime() {
            string newValue = $"{(int)Mathf.Ceil(bomb.time)}";
            if(value != newValue) {
                value = newValue;
                OnChanged();
            }
        }
    }
}

