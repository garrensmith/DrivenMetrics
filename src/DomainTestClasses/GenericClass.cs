namespace DomainTestClasses
{
    public class GenericClass<T>
    {
        private readonly T[] _ts;

        public GenericClass(T[] source)
        {
            _ts = source;
        }

        public override string ToString ()
        {
            return _ts.Length.ToString ();
        }
    }
}
