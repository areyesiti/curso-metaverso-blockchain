using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CadenaBloques
{
    public class Bloque
    {
        // Constantes para gestionar el algorito de minado.
        // Dificultad: Si se generan muchos bloques al mismo tiempo subimos la dificultad
        const int DIFICULTAD = 3;
        // Tasa de minado
        const int TASA_MINADO = 3;

        public double Tiempo { get; set; }
        public string PrevHas { get; set; }
        public string Hash { get; set; }
        public string Datos { get; set; }
        public int Nonce { get; set; }
        public int Dificultad { get; set; }

        // Constructor de la clase
        public Bloque(double tiempo, string prevHas, string hash, string datos, int nonce, int dificultad)
        {
            this.Tiempo = tiempo;
            this.PrevHas = prevHas;
            this.Hash = hash;
            this.Datos = datos;
            this.Nonce = nonce;
            this.Dificultad = dificultad;
        }

        // Genera el bloque génesis (Primer bloque de la cadena)
        public static Bloque Genesis()
        {
            return new Bloque(
                tiempo: ObtenerTimeStamp(new DateTime(2009, 3, 1)),
                prevHas: null,
                hash: "genesis_hash",
                datos: "Bloque génesis",
                nonce: 0,
                dificultad: DIFICULTAD
            );
        }

        public static Bloque Minar(Bloque prevBlock, string data)
        {
            double tiempo = 0;
            int nonce = 0;
            string hash = String.Empty;            
            string ceros = String.Empty;

            // Obtenemos la dificultad del bloque anterior
            int dificultad = prevBlock.Dificultad;

            do
            {
                // Aumentamos el nonce
                nonce = nonce + 1;
                tiempo = ObtenerTimeStamp(DateTime.Now);

                // Sumamos el timestamp del bloque anterior con la tasa de minado
                // La comparamos con el timespan del nuevo bloque
                if (prevBlock.Tiempo + TASA_MINADO > tiempo)
                {
                    if (dificultad < 64) dificultad++;
                } else
                {
                    if (dificultad > 0) dificultad--;
                }

                // Generamos el hash con la información del bloque
                hash = Sha256(prevBlock.Hash + tiempo + data + nonce + dificultad);

                // Repetimos el bucle hasta que la cantidad de ceros del hash sea igual que los ceros de la dificultad
            } while (hash.Substring(0, dificultad) != (ceros.PadLeft(dificultad, '0')));

            // Devolvemos el bloque minado
            return new Bloque(tiempo: tiempo, prevHas: prevBlock.Hash, hash: hash, datos: data, nonce: nonce, dificultad: dificultad);
        }

        // Escribe en consola la información del bloque
        public override string ToString()
        {
            return $"Time: { this.Tiempo }\r\nPreviusHash: { this.PrevHas }\r\nHash: { this.Hash }\r\nData: { this.Datos }\r\nNonce: { this.Nonce }\r\nDifficulty: { this.Dificultad }\r\n-------------------";
        }

        // Genera el timeStamp en base a una fecha
        public static double ObtenerTimeStamp(DateTime t)
        {
            return new DateTimeOffset(t).ToUnixTimeSeconds();
        }
        // Generar el código SHA-256 del string que se le pase
        public static string Sha256(string cadena)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(cadena));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
