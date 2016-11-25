using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    public class SortExpressionCollection:List<SortExpression>
    {
        public static SortExpressionCollection Parse(string sortExpression)
        {
            SortExpressionCollection result = new SortExpressionCollection();
            string[] arrSortExpression = sortExpression.Split(new char[] { ',' });
            try
            {
                foreach (string item in arrSortExpression)                
                    result.Add(SortExpression.Parse(item));                
            }
            catch
            {
                throw new ArgumentException();
            }
            return result;
        }
    }
}
