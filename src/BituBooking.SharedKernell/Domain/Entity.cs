namespace BituBooking.SharedKernell.Domain;

public class Entity
{
    public Entity(Guid? identifier)
    {
        Identifier = identifier ?? Guid.NewGuid();
    }

    public Guid Identifier { get; }

    public override bool Equals(object? entity)
    {
        if (entity is not Entity entityToBeCompared)
            return false;

        if (ReferenceEquals(this, entityToBeCompared))
            return true;

        if (GetType() != entityToBeCompared.GetType())
            return false;

        return Identifier == entityToBeCompared.Identifier;
    }

    public static bool operator ==(Entity entityA, Entity entityB)
    {
        if (entityA is null && entityB is null)
            return true;

        if (entityA is null || entityB is null)
            return false;

        return entityA.Equals(entityB);
    }

    public static bool operator !=(Entity entityA, Entity entityB)
    {
        return !(entityA == entityB);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Identifier).GetHashCode();
    }
}


