using System;

public class CurrentTime
{
    private bool value;

    public event Action<bool> onValueChanged;

    public CurrentTime(bool _value)
    {
        value = _value;
    }

    public bool Value
    { get => value;
      set
        {
            if (this.value != value)
            {
                this.value = value;
                onValueChanged?.Invoke(this.value);
            }
        }
    
    }
}
