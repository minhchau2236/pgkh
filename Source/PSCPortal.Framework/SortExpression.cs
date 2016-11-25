using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    public class SortExpression
    {
        public enum ORDER
        {
            ASC,
            DESC
        }
        private string _propertyName;
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
            }
        }
        private ORDER _order;
        public ORDER Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }
        public SortExpression(string propertyName, ORDER order)
        {
            _propertyName = propertyName;
            _order = order;
        }
        protected SortExpression()
        {
        }
        public static SortExpression Parse(string sortExpression)
        {
            SortExpression result = new SortExpression();
            string[]arrSortExpression = sortExpression.Split(new char[] { ' ' });
            try
            {
                result._propertyName = arrSortExpression[0];
                result._order = arrSortExpression[1] == "ASC" ? ORDER.ASC : ORDER.DESC;
            }
            catch
            {
                throw new ArgumentException();
            }
            return result;
        }        
    }
}
