﻿using System;

namespace ooapi.v5.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FallBackAttribute : Attribute
    {
        private object _item;

        public FallBackAttribute(object item)
        {
            this._item = item;
        }

        public virtual object Value
        {
            get { return _item; }
        }

    }
}
