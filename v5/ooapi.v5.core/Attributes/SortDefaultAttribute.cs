namespace ooapi.v5.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SortDefaultAttribute : Attribute
    {
        private readonly bool _asc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asc"></param>
        public SortDefaultAttribute(bool asc = true)
        {
            _asc = asc;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Asc
        {
            get { return _asc; }
        }
    }
}
