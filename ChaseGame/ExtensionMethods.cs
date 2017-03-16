using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaseGameNamespace
{
    public static class ExtensionMethods
    {
        public static string ListEveryElement(this List<Coordinates> coordinates)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < coordinates.Count; i++)
            {
                stringBuilder.Append(i + coordinates[i].ToString() + Environment.NewLine);
            }
            return stringBuilder.ToString();
        }
    }
}
