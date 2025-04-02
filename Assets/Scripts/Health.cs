using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    private bool _isCanBeChanged;

    public event Action HealthUpdate;
    public event Action HealthEnded;
    public event Action DamageTaken;

    public float MaxValue => _maxValue;
    public float CurrentValue { get; private set; }
    public float DefaultMaxValue { get; private set; }

    private void Awake()
    {
        CurrentValue = MaxValue;
        DefaultMaxValue = MaxValue;
        _isCanBeChanged = true;
    }

    private void OnEnable()
    {
        CurrentValue = MaxValue;
    }

    public void TakeHeal(float heal)
    {
        if (heal < 0)
            return;

        if (_isCanBeChanged == true)
        {
            CurrentValue += heal;

            if (CurrentValue > MaxValue)
                CurrentValue = MaxValue;

            HealthUpdate?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_isCanBeChanged == true)
        {
            CurrentValue -= damage;

            if (CurrentValue < 0)
            {
                CurrentValue = 0;
                HealthEnded?.Invoke();
            }

            HealthUpdate?.Invoke();
            DamageTaken?.Invoke();
        }
    }

    public void SetMaxHealth(float value)
    {
        _maxValue = value;
        CurrentValue = MaxValue;
        HealthUpdate?.Invoke();
    }

    public void Refresh() => CurrentValue = MaxValue;
}