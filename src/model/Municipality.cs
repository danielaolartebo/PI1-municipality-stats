/// Daniela Olarte Borja || David Montaño Tamayo
using System.Windows;

namespace stats_s1.model
{
    public class Municipality
    {
        // Attributes
        public string Type { get; }
        public int Count { get; set; }

        // Methods
        public Municipality(string type)
        {
            Type = type;
            Count = 1;
        }
    }
}

