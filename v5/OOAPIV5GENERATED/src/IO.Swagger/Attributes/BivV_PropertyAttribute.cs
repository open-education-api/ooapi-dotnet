using System;

namespace IO.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BivVAttribute : Attribute
    {
        private bool _laag = false;
        private bool _middel = false;

        public BivVAttribute(bool middel = false, bool laag = false)
        {
            this._middel = middel;
            this._laag = laag;
        }

        public virtual bool Middel
        {
            get { return _middel; }
        }

        public virtual bool Laag
        {
            get { return _laag; }
        }
    }
}
