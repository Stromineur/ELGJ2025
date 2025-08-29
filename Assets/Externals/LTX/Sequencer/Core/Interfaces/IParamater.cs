namespace LTX.Sequencing
{
    public interface IParameter
    {
        public string name{ get; }
        public object Value { get; }
    }

    public interface IParameter<T> : IParameter
    {
        new public T Value { get; }
    }
}