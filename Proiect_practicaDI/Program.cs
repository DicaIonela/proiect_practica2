using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NivelStocareDate;
using System.Configuration;
using LibrarieClase;

namespace Proiect_practicaDI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\Proiect_practicaDI\\" + numeFisier;
            Administrare_FisierText admin=new Administrare_FisierText(caleCompletaFisier);
            string optiune;
            Utilizator utilizatornou= new Utilizator();
            Utilizator utilizator = new Utilizator("Ion Popescu", "0737774955", "00::11::22::33::44::55");
            


            //MENIU
            Console.WriteLine("MENIU:");
            Console.WriteLine("C. Citire utilizator");
            Console.WriteLine("S. Salvare utilizator");
            Console.WriteLine("A. Afisare utilizatori din fisier.");
            do
            {
                Console.WriteLine("\nIntrodu optiunea dorita:");
                optiune = Console.ReadLine();
                switch (optiune)
                {
                    case "C":
                        utilizatornou = CitireUtilizatorTastatura();
                        break;
                    case "S":
                        
                        /// VERIFICARE NUMAR DE TELEFON VALID 
                       
                        if (utilizatornou.Numar.Length != 10)
                            Console.WriteLine("Numarul utilizatorului este invalid.");
                        else
                        {
                            ///DACA UTILIZATORUL NU EXISTA IN FISIER, VA FI ADAUGAT, IAR DACA EXISTA, SE VA AFISA UN MESAJ CORSPUNZATOR
                            
                            if (!admin.UtilizatorExista(utilizatornou.Nume, utilizatornou.Numar))
                            {
                                admin.AddUtilizator(utilizatornou);
                                Console.WriteLine("Utilizatorul a fost adaugat cu succes.");
                            }
                            else
                            {
                                Console.WriteLine("Utilizatorul este deja in fisier.");
                            }
                        }
                        break;
                    case "A":
                        ///CREAM UN TABLOU DE OBIECTE
                        
                        Utilizator[] utilizatori = admin.GetUtilizatori(out int nrUtilizatori);
                        AfisareUtilizatori(utilizatori, nrUtilizatori);
                        break;
                }

            } while (true);

            
            Console.ReadLine();//FOLOSIT PENTRU A FI PUS PROGRAMUL IN ASTEPTARE; EVITA INCHIDEREA APLICATIEI
        }
        public static void AfisareUtilizatori(Utilizator[]utilizatori, int nrUtilizatori)
        {
            Console.WriteLine("Utilizatorii salvati in fisier sunt:");

            /// SE PARCURGE TABLOUL DE OBIECTE SI SE AFISEAZA INFORMATIILE IN FORMATUL CORESPUNZATOR 
            
            for (int contor=0; contor<nrUtilizatori; contor++)
            {
                string infoLocuri = utilizatori[contor].Info();
                Console.WriteLine(infoLocuri);
             }
        }
        public static Utilizator CitireUtilizatorTastatura()
        {
            Console.WriteLine("Introduceti datele utilizatorului:");
            Console.WriteLine("Nume:");
            string nume= Console.ReadLine();

            Console.WriteLine("Numar:");
            string numar = Console.ReadLine();

            Console.WriteLine("Adresa MAC:");
            string adresamac = Console.ReadLine();

            Utilizator utilizator = new Utilizator(nume, numar, adresamac);
            return utilizator;
        }
    }
}
