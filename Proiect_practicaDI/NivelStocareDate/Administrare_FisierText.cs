using LibrarieClase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NivelStocareDate
{
    public class Administrare_FisierText
    {
        private const int NR_MAX = 50;
        private string numeFisier;



        /// CONSTRUCTOR LINII FISIER
        public Administrare_FisierText(string numeFisier)
        {
            this.numeFisier= numeFisier;
            Stream streamFisier = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisier.Close();
        }

        ///ADAUGARE UTILIZATOR IN FISIER 
        public void AddUtilizator(Utilizator utilizator)
        {
            using (StreamWriter streamwriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamwriterFisierText.WriteLine(utilizator.Conversie_PentruFisier());
            }
        }

        ///VERIFICA EXISTENTA UTILIZATORULUI IN FISIER PENTRU A EVITA SCRIEREA UNEI COPII
        public bool UtilizatorExista(string nume, string numar)
        {
            if (!File.Exists(numeFisier))
                return false;

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = streamReader.ReadLine()) != null)
                {
                    Utilizator utilizator = new Utilizator(linie);
                    if (utilizator.Nume == nume && utilizator.Numar == numar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        ///STOCHEAZA UTILIZATORII DIN FISIER INTR-UN TABLOU DE OBIECTE 
        public Utilizator[] GetUtilizatori(out int nrUtilizatori)
        {
            Utilizator[] utilizatori = new Utilizator[NR_MAX];
            using (StreamReader streamrdr=new StreamReader(numeFisier))
            {
                string linieFisier;
                nrUtilizatori = 0;
                while ((linieFisier = streamrdr.ReadLine()) != null)
                {
                    utilizatori[nrUtilizatori++]=new Utilizator(linieFisier);

                }
                Array.Resize(ref utilizatori, nrUtilizatori);

            }
            return utilizatori;

        }
        
        public Utilizator[] CautaUtilizator(string criteriu)
        {
            int nrUtilizatori = 0;
            Utilizator[] utilizatori = GetUtilizatori(out nrUtilizatori);
            List <Utilizator> utilizatorigasiti= new List<Utilizator>();
            foreach (Utilizator utilizator in utilizatori)
            {
                if (utilizator != null && utilizator.Nume != null && utilizator.Nume.Contains(criteriu) )
                {
                    utilizatorigasiti.Add(utilizator);
                }
            }
            return utilizatorigasiti.ToArray();
        }
    }
    }

