using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadenaBloques
{
    public class Cadena
    {
        // Declaramos la cadena de bloques (lista)
        List<Bloque> cadena = new List<Bloque>();

        public Cadena()
        {
            // Añadimos el bloque génesis a la cadena al inicializar
            this.cadena.Add(Bloque.Genesis());
        }

        // Añade bloques a la cadena de bloques
        public Bloque AnyadirBloque(string data)
        {
            // Llamada a la función de minar bloque pasando los datos del nuevo bloque (data) y el bloque anterior
            Bloque bloqueMinado = Bloque.Minar(this.cadena.LastOrDefault(), data);
            // Cuando minamos un bloque lo añadimos a la cadena
            this.cadena.Add(bloqueMinado);
            return bloqueMinado;
        }
    }
}
