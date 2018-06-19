using System;
using System.Reflection;

namespace VoiceMemo
{
    public class Utility
    {
        public Utility()
        {

        }

        public static void PrintProperties(object obj)
        {
            try
            {
                var type = obj.GetType();

                foreach (PropertyInfo p in type.GetProperties())
                {
                    object propertyValue = p.GetValue(obj, null);
                    Console.WriteLine(p.Name + ":- " + propertyValue);
                    //Console.WriteLine(p.Name + ":- " + p.GetValue(obj, null));

                    //if (p.PropertyType.GetProperties().Count() > 0)
                    //{
                    //    // what to pass in to recursive method
                    //    PrintProperties(propertyValue);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("    FAILED PrintProperties : " + e.Message);
            }
        }
    }
}