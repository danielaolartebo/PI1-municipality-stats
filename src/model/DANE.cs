/// Daniela Olarte Borja || David Montaño Tamayo
using System.Windows;
namespace stats_s1.model
{
    public class DANE
    {
        // Attributes
        public int CodDpto { get; }
        public int CodMcpio { get; }
        public string NameDpto { get; }
        public string NameMcpio { get; }
        public string Type { get; }

        // Methods
        public DANE(int codDpto, int codMcpio, string nameDpto, string nameMcpio, string type)
        {
            CodDpto = codDpto;
            CodMcpio = codMcpio;
            NameDpto = nameDpto;
            NameMcpio = nameMcpio;
            Type = type;
        }
    }
}