using GameFlow.Features;
using GameFlow.Core;
using UnityEngine;

namespace GameFlow.FeatureSystem
{
    public class FeatureManager : MonoBehaviour
    {
        private FeatureBase[] features;
        private readonly GameStateMachine gameState = new();

        public void RegisterFeatures(FeatureBase[] registeredFeatures)
        {
            features = registeredFeatures;

            foreach (var feature in features)
            {
                feature?.Initialize(gameObject);
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (features == null)
                return;

            foreach (var feature in features)
            {
                feature?.Tick(deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryActivateFeature(0);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                InterruptAll();
            }
        }

        public void TryActivateFeature(int index)
        {
            if (features == null) return;
            if (index < 0 || index >= features.Length) return;

            if (gameState.Has(GameState.FeatureLock))
                return;

            features[index]?.TryActivate();
        }

        private void Start()
        {
            RegisterFeatures(new FeatureBase[]
            {
                new MultiStepFeature()
            });
        }

        public void InterruptAll()
        {
            if (features == null) return;

            foreach (var feature in features)
            {
                feature?.Interrupt();
            }

            gameState.Add(GameState.Interrupted);
        }

        public void SetFeatureLock(bool locked)
        {
            if (locked)
                gameState.Add(GameState.FeatureLock);
            else
                gameState.Remove(GameState.FeatureLock);
        }
    }
}