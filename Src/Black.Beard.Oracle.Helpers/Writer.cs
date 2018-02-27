using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Helpers
{
    public class Writer
    {

        public Writer(Type type)
        {

            var properties = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            foreach (var item in properties)
            {
                Type _type = item.PropertyType;
                if (_type.IsClass && _type != typeof(string))
                {

                }
                else if (_type.IsPrimitive)
                {

                }
                else
                {

                }
            }
        }



    }

}
