[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + "Float")]
public class FloatVariable : StructVariable<float>
{
    public void Add(float value) => Value += value;
    public void Add(FloatVariable variable) => Add(variable.Value);
    public void Add(FloatInstance instance) => Add(instance.Value);

    public void Subtract(float value) => Value -= value;
    public void Subtract(FloatVariable variable) => Subtract(variable.Value);
    public void Subtract(FloatInstance instance) => Subtract(instance.Value);

    public void Multiply(float value) => Value *= value;
    public void Multiply(FloatVariable variable) => Multiply(variable.Value);
    public void Multiply(FloatInstance instance) => Multiply(instance.Value);

    public void Divide(float value) => Value /= value;
    public void Divide(FloatVariable variable) => Divide(variable.Value);
    public void Divide(FloatInstance instance) => Divide(instance.Value);

    public string ValueToString() => Value.ToString();

    public static FloatVariable operator +(FloatVariable variable, int value)
    {
        variable.Add(value);
        return variable;
    }

    public static FloatVariable operator -(FloatVariable variable, int value)
    {
        variable.Subtract(value);
        return variable;
    }

    public static FloatVariable operator *(FloatVariable variable, int value)
    {
        variable.Multiply(value);
        return variable;
    }

    public static FloatVariable operator /(FloatVariable variable, int value)
    {
        variable.Divide(value);
        return variable;
    }
}