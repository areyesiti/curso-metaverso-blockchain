using System;

namespace CadenaBloques
{
    class Program
    {
        static void Main(string[] args)
        {
            Cadena cadenaBloques = new Cadena();
            // Vamos a minar 10 bloques!!!
            for (int i = 0; i < 10; i++)
            {
                Bloque bloque = cadenaBloques.AnyadirBloque($"Block {i}");
                Console.WriteLine(bloque.ToString());
            }
        }
    }
}
