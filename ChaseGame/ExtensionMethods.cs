using System.Collections.Generic;
using System.Text;

namespace ChaseGameNamespace
{
    public static class ExtensionMethods
    {
        public static string ListEveryElement(this List<Coordinates> coordinates)
        {
            if(coordinates.Count==0)
            {
                return "List is empty";
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < coordinates.Count; i++)
            {
                stringBuilder.Append(i + ". " + coordinates[i].ToString());
                stringBuilder.Append(i < (coordinates.Count - 1) ? ", " : ".");
            }
            return stringBuilder.ToString();
        }
    }
}
