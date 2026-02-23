using GameFlow.Core;
using UnityEngine;

namespace GameFlow.Features
{
    public class MultiStepFeature : FeatureBase
    {
        private int currentStep;
        private int totalSteps = 3;

        private float stepDuration = 1.0f;
        private float stepTimer;

        public MultiStepFeature()
        {
            cooldownDuration = 2.0f;
        }

        protected override void OnActivate()
        {
            currentStep = 0;
            stepTimer = stepDuration;

            State = FeatureState.Active;

            Debug.Log("MultiStepFeature activated");
        }

        protected override void OnActiveTick(float deltaTime)
        {
            stepTimer -= deltaTime;

            if (stepTimer <= 0f)
            {
                AdvanceStep();
            }
        }

        private void AdvanceStep()
        {
            currentStep++;
            Debug.Log($"MultiStepFeature step {currentStep}");

            if (currentStep >= totalSteps)
            {
                Resolve();
                return;
            }

            stepTimer = stepDuration;
        }

        protected override void OnResolve()
        {
            Debug.Log("MultiStepFeature resolving");

            base.OnResolve();
        }

        protected override void OnCooldownComplete()
        {
            Debug.Log("MultiStepFeature cooldown complete");
        }
    }
}