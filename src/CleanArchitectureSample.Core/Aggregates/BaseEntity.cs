namespace CleanArchitectureSample.Core.Aggregates
{
    public abstract class BaseEntity
    {
        public DateTime TimeStamp { get; set; } = default!;
    }
}
