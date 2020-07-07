namespace Shop.Models
{
    public class OverallResult<T>
    {
        public bool Success { get; set; }
        public T Objects { get; set; }
    }
}
