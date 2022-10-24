using System;

namespace ooapi.v5.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortDefaultAttribute : Attribute
    {
        private bool _asc = true;

        public SortDefaultAttribute(bool asc = true)
        {
            this._asc = asc;
        }

        public virtual bool Asc
        {
            get { return _asc; }
        }
    }
}
