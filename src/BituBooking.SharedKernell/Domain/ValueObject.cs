namespace BituBooking.SharedKernell.Domain;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityProperties();

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityProperties().SequenceEqual(valueObject.GetEqualityProperties());
    }

    public override int GetHashCode()
    {
        return GetEqualityProperties()
                .Aggregate(1, (current, value) =>
                {
                    unchecked
                    {
                        return (current * 37) + (value?.GetHashCode() ?? 0);
                    }
                });
    }

    public static bool operator ==(ValueObject valueA, ValueObject valueB)
    {
        if (valueA is null && valueB is null)
            return true;

        if (valueA is null || valueB is null)
            return false;

        return valueA.Equals(valueB);
    }

    public static bool operator !=(ValueObject valueA, ValueObject valueB)
    {
        return !(valueA == valueB);
    }
}

