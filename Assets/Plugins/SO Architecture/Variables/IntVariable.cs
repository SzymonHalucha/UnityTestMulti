[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + "Int")]
public class IntVariable : StructVariable<int>
{
    public void Add(int value) => Value += value;
    public void Add(IntVariable variable) => Add(variable.Value);
    public void Add(IntInstance instance) => Add(instance.Value);

    public void Subtract(int value) => Value -= value;
    public void Subtract(IntVariable variable) => Subtract(variable.Value);
    public void Subtract(IntInstance instance) => Subtract(instance.Value);

    public void Multiply(int value) => Value *= value;
    public void Multiply(IntVariable variable) => Multiply(variable.Value);
    public void Multiply(IntInstance instance) => Multiply(instance.Value);

    public void Divide(int value) => Value /= value;
    public void Divide(IntVariable variable) => Divide(variable.Value);
    public void Divide(IntInstance instance) => Divide(instance.Value);

    public string ValueToString() => Value.ToString();

    public static IntVariable operator +(IntVariable variable, int value)
    {
        variable.Add(value);
        return variable;
    }

    public static IntVariable operator -(IntVariable variable, int value)
    {
        variable.Subtract(value);
        return variable;
    }

    public static IntVariable operator *(IntVariable variable, int value)
    {
        variable.Multiply(value);
        return variable;
    }

    public static IntVariable operator /(IntVariable variable, int value)
    {
        variable.Divide(value);
        return variable;
    }
}