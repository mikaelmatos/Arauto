using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arauto.Util
{
    internal class ConversorNumeros
    {
        public static string SubstituirNumerosPorExtenso(string texto)
        {
            return Regex.Replace(texto, @"\b\d+\b", match =>
            {
                if (long.TryParse(match.Value, out long numero))
                {
                    return NumeroPorExtenso(numero);
                }
                return match.Value;
            });
        }

        private static string NumeroPorExtenso(long numero)
        {
            var cultura = new CultureInfo("pt-BR");
            return cultura.TextInfo.ToTitleCase(
                NumberToWords(numero).ToLower()
            );
        }

        // Conversor básico de números para extenso (0 a 9999+)
        private static string NumberToWords(long number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "menos " + NumberToWords(Math.Abs(number));

            string[] unidades = { "", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove" };
            string[] especiais = { "dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove" };
            string[] dezenas = { "", "", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };
            string[] centenas = { "", "cem", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos" };

            string resultado = "";

            if (number >= 1000)
            {
                long milhar = number / 1000;
                resultado += milhar == 1 ? "mil" : NumberToWords(milhar) + " mil";
                number %= 1000;
                if (number > 0) resultado += " e ";
            }

            if (number >= 100)
            {
                long centena = number / 100;
                resultado += (centena == 1 && number % 100 == 0) ? "cem" : centenas[centena];
                number %= 100;
                if (number > 0) resultado += " e ";
            }

            if (number >= 20)
            {
                resultado += dezenas[number / 10];
                number %= 10;
                if (number > 0) resultado += " e ";
            }

            if (number >= 10 && number <= 19)
            {
                resultado += especiais[number - 10];
            }
            else if (number > 0 && number < 10)
            {
                resultado += unidades[number];
            }

            return resultado;
        }
    }
}
