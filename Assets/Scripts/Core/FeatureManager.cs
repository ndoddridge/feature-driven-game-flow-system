using GameFlow.Features;
using UnityEngine;

namespace GameFlow.FeatureSystem
{
    public class FeatureManager : MonoBehaviour
    {
        private FeatureBase[] features;

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
        }

        public void TryActivateFeature(int index)
        {
            if (features == null)
                return;

            if (index < 0 || index >= features.Length)
                return;

            features[index]?.TryActivate();
        }
    }
}