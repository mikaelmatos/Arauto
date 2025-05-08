using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arauto.Util
{    public class HorarioExtractor
    {
        public static TimeSpan? ExtrairHorario(string texto)
        {
            // Regex para formatos HH:mm ou HH:mm:ss (24h)
            var regex = new Regex(@"\b([01]?[0-9]|2[0-3]):[0-5][0-9](:[0-5][0-9])?\b");

            var match = regex.Match(texto);
            if (match.Success)
            {
                if (TimeSpan.TryParse(match.Value, out var horario))
                    return horario;
            }

            return null; // Caso nenhum horário seja encontrado
        }
    }
}
