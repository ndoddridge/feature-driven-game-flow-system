using GameFlow.Core;
using UnityEngine;

namespace GameFlow.Features
{
    public abstract class FeatureBase
    {
        public FeatureState State 
        { 
            get; 
            protected set; 
        } = FeatureState.Inactive;

        protected GameObject owner;

        protected float cooldownDuration;
        protected float cooldownRemaining;

        public void Initialize(GameObject owner)
        {
            this.owner = owner;
        }

        public virtual bool CanActivate()
        {
            return State == FeatureState.Inactive;
        }

        public void TryActivate()
        {
            if (!CanActivate())
                return;

            State = FeatureState.Activating;
            OnActivate();
        }

        protected abstract void OnActivate();

        public virtual void Tick(float deltaTime)
        {
            switch (State)
            {
                case FeatureState.Active:
                    OnActiveTick(deltaTime);
                    break;

                case FeatureState.Cooldown:
                    cooldownRemaining -= deltaTime;
                    if (cooldownRemaining <= 0f)
                    {
                        cooldownRemaining = 0f;
                        State = FeatureState.Inactive;
                        OnCooldownComplete();
                    }
                    break;
            }
        }

        protected virtual void OnActiveTick(float deltaTime) { }

        protected void Resolve()
        {
            State = FeatureState.Resolving;
            OnResolve();
        }

        protected virtual void OnResolve()
        {
            State = FeatureState.Cooldown;
            cooldownRemaining = cooldownDuration;
        }

        protected virtual void OnCooldownComplete() { }

        public virtual bool CanBeInterrupted()
        {
            return State == FeatureState.Active || State == FeatureState.Activating;
        }

        public void Interrupt()
        {
            if (!CanBeInterrupted())
                return;

            State = FeatureState.Inactive;
            OnInterrupted();
        }

        protected virtual void OnInterrupted() { }
    }
}