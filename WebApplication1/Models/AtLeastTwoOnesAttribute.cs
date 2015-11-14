using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AtLeastTwoOnesAttribute : DataTypeAttribute
    {
        public AtLeastTwoOnesAttribute() : base(DataType.Text)
        {
            this.ErrorMessage = "At least two '1's";
        }


        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);

            var num = (from p in str.ToArray()
                        where p == '1'
                        select p).Count();

            return (num == 2);
        }
    }
}