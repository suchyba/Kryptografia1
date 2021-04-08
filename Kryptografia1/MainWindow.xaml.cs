using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kryptografia1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void szyfrujRailFence_Click(object sender, RoutedEventArgs e)
        {
            int klucz = 0;
            int.TryParse(kluczRailFenceTextBox.Text, out klucz);

            if (klucz != 0)
            {
                // tablica zawierająca "szlaczek"
                char?[,] tab = new char?[tekstJawnyRailFenceTextBox.Text.Length, klucz];
                // zmienna przechowująca kierunek -> 1 - spadanie, -1 - unoszenie
                int dir = 1;
                // zmienna przechowująca poziom, na którym się znajdujemy
                int level = 0;
                // wygenerowanie wzoru
                for (int i = 0; i < tekstJawnyRailFenceTextBox.Text.Length; ++i)
                {
                    tab[i, level] = tekstJawnyRailFenceTextBox.Text[i];

                    // zmiana kierunku w przypadku wartości skrajnych
                    if (level == 0)
                        dir = 1;
                    else if (level == klucz - 1)
                        dir = -1;

                    level += dir;
                }
                StringBuilder wynik = new();
                // odczytanie wartości po wierszach
                for (int j = 0; j < klucz; ++j)
                {
                    for (int i = 0; i < tekstJawnyRailFenceTextBox.Text.Length; ++i)
                    {
                        if (tab[i, j] != null)
                        {
                            wynik.Append(tab[i, j]);
                        }
                    }
                }
                szyfrogramRailFenceTextBox.Text = wynik.ToString();
            }
        }

        private void deszyfrujRailFenceButton_Click(object sender, RoutedEventArgs e)
        {
            int klucz = 0;
            int.TryParse(kluczRailFenceTextBox.Text, out klucz);

            if (klucz != 0)
            {
                // tablica zawierająca "szlaczek"
                char?[,] tab = new char?[szyfrogramRailFenceTextBox.Text.Length, klucz];
                // zmienna przechowująca kierunek -> 1 - spadanie, -1 - unoszenie
                int dir = 1;
                // zmienna przechowująca poziom, na którym się znajdujemy
                int level = 0;
                // wygenerowanie wzoru, który posłuży do deszyfrowania
                for (int i = 0; i < szyfrogramRailFenceTextBox.Text.Length; ++i)
                {
                    tab[i, level] = '0';

                    // zmiana kierunku w przypadku wartości skrajnych
                    if (level == 0)
                        dir = 1;
                    else if (level == klucz - 1)
                        dir = -1;

                    level += dir;
                }
                // przejście po kolejnych wierszach wzoru i wstawianie kolejnych liter w miejscach gdzie nie ma pustego miejsca
                int obecnaLitera = 0;
                for (int j = 0; j < klucz; ++j)
                {
                    for (int i = 0; i < szyfrogramRailFenceTextBox.Text.Length; ++i)
                    {
                        if (tab[i, j] != null)
                        {
                            tab[i, j] = szyfrogramRailFenceTextBox.Text[obecnaLitera];
                            ++obecnaLitera;
                        }
                    }
                }

                // odczytanie wiadomości
                StringBuilder wynik = new();
                for (int i = 0; i < szyfrogramRailFenceTextBox.Text.Length; ++i)
                {
                    for (int j = 0; j < klucz; ++j)
                    {
                        if (tab[i, j] != null)
                        {
                            wynik.Append(tab[i, j]);
                            break;
                        }
                    }
                }

                tekstJawnyRailFenceTextBox.Text = wynik.ToString();
            }
        }

        private void szyfrujPrzestawieniaAButton_Click(object sender, RoutedEventArgs e)
        {
            // klucz bez separatorów
            string[] kluczString = kluczPrzestawieniaATextBox.Text.Split('-');
            // długość klucza
            int wielkoscKlucza = kluczString.Length;

            // konwertowanie wartości string na wartości int a także pomniejszanie ich by odpowiadały indeksowaniu tablicy
            int[] klucz = new int[wielkoscKlucza];
            for (int i = 0; i < wielkoscKlucza; ++i)
            {
                int.TryParse(kluczString[i], out klucz[i]);
                --klucz[i];
            }

            // ilość wierszy
            int wysokosc = (int)Math.Ceiling(tekstJawnyPrzestawieniaATextBox.Text.Length / (decimal)wielkoscKlucza);
            // macierz
            char?[,] tab = new char?[wielkoscKlucza, wysokosc];

            // ustawianie kolejnych liter słowa na odpowiednich pozycjach w macierzy
            int obecnaLitera = 0;
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < wielkoscKlucza; ++j)
                {
                    if (obecnaLitera >= tekstJawnyPrzestawieniaATextBox.Text.Length)
                        break;
                    tab[j, i] = tekstJawnyPrzestawieniaATextBox.Text[obecnaLitera++];
                }
            }
            // generowanie wyniku 
            StringBuilder wynik = new();
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < wielkoscKlucza; ++j)
                {
                    if (tab[klucz[j], i] != null)
                        wynik.Append(tab[klucz[j], i]);
                }
            }
            szyfrogramPrzestawieniaATextBox.Text = wynik.ToString();
        }

        private void deszyfrujPrzestawieniaAButton_Click(object sender, RoutedEventArgs e)
        {
            // klucz bez separatorów
            string[] kluczString = kluczPrzestawieniaATextBox.Text.Split('-');
            // długość klucza
            int wielkoscKlucza = kluczString.Length;

            // konwertowanie wartości string na wartości int a także pomniejszanie ich by odpowiadały indeksowaniu tablicy
            int[] klucz = new int[wielkoscKlucza];
            for (int i = 0; i < wielkoscKlucza; ++i)
            {
                int.TryParse(kluczString[i], out klucz[i]);
                --klucz[i];
            }

            // ilość wierszy
            int wysokosc = (int)Math.Ceiling(szyfrogramPrzestawieniaATextBox.Text.Length / (decimal)wielkoscKlucza);
            // macierz
            char?[,] tab = new char?[wielkoscKlucza, wysokosc];

            // blokowanie pól końcowych w przypadku kiedy tekst zostawia wolne miejsce w macierzy
            int zabronione = wielkoscKlucza * wysokosc - szyfrogramPrzestawieniaATextBox.Text.Length;
            for (int i = wielkoscKlucza - zabronione; i < wielkoscKlucza; ++i)
                tab[i, wysokosc - 1] = '0';

            // wypełnienie macierzy zgodnie z kluczem
            int obecnaLitera = 0;
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < wielkoscKlucza; ++j)
                {
                    if (obecnaLitera >= szyfrogramPrzestawieniaATextBox.Text.Length)
                        break;

                    // pomijanie pól zablokowanych by nie powstawały dziury
                    if (tab[klucz[j], i] != '0')
                        tab[klucz[j], i] = szyfrogramPrzestawieniaATextBox.Text[obecnaLitera++];
                }
            }

            // czytanie wierszami
            StringBuilder wynik = new();
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < wielkoscKlucza; ++j)
                {
                    if (tab[j, i] != null && tab[j, i] != '0')
                        wynik.Append(tab[j, i]);
                }
            }
            tekstJawnyPrzestawieniaATextBox.Text = wynik.ToString();
        }

        // metoda konwertująca słowo do kolejności kolumn (lista indeksów)
        private int[] slowoDoKluczaB(string slowo)
        {
            int?[] klucz = new int?[slowo.Length];

            int obecny = 0;
            while (obecny < klucz.Length)
            {
                // szukanie najmniejszej litery, która nie znajduje się na liście
                char? najmniejszy = null;
                int najmniejszyIndex = 0;
                for (int i = 0; i < slowo.Length; ++i)
                {
                    if ((najmniejszy == null || slowo[i] < najmniejszy) && !klucz.Contains(i))
                    {
                        najmniejszy = slowo[i];
                        najmniejszyIndex = i;
                    }
                }
                // dodanie kolejnej litery do listy
                klucz[obecny++] = najmniejszyIndex;
            }

            return klucz.Cast<int>().ToArray();
        }

        private void szyfrujPrzestawieniaBButton_Click(object sender, RoutedEventArgs e)
        {
            string tekstJawny = tekstJawnyPrzestawieniaBTextBox.Text.Replace(" ", null);

            // wielkość macierzy
            int szerokosc = kluczPrzestawieniaBTextBox.Text.Length;
            int wysokosc = (int)Math.Ceiling(tekstJawny.Length / (decimal)szerokosc);

            // macierz
            char?[,] tab = new char?[szerokosc, wysokosc];
            // wypełnianie macierzy tekstem
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < szerokosc; ++j)
                {
                    if (i * szerokosc + j < tekstJawny.Length)
                        tab[j, i] = tekstJawny[i * szerokosc + j];
                }
            }
            // generowanie klucza na podstawie wyrazu
            int[] klucz = slowoDoKluczaB(kluczPrzestawieniaBTextBox.Text);

            // odczytanie wyniku
            StringBuilder wynik = new();
            for (int i = 0; i < klucz.Length; ++i)
            {
                for (int j = 0; j < wysokosc; ++j)
                {
                    if (tab[klucz[i], j] != null)
                        wynik.Append(tab[klucz[i], j]);
                }
            }
            szyfrogramPrzestawieniaBTextBox.Text = wynik.ToString();
        }

        private void deszyfrujPrzestawieniaBButton_Click(object sender, RoutedEventArgs e)
        {
            // wielkość macierzy
            int szerokosc = kluczPrzestawieniaBTextBox.Text.Length;
            int wysokosc = (int)Math.Ceiling(szyfrogramPrzestawieniaBTextBox.Text.Length / (decimal)szerokosc);

            // macierz
            char?[,] tab = new char?[szerokosc, wysokosc];
            // generowanie klucza na podstawie wyrazu
            int[] klucz = slowoDoKluczaB(kluczPrzestawieniaBTextBox.Text);

            // blokowanie pól końcowych w przypadku kiedy tekst zostawia wolne miejsce w macierzy
            int zabronione = szerokosc * wysokosc - szyfrogramPrzestawieniaBTextBox.Text.Length;
            for (int i = szerokosc - zabronione; i < szerokosc; ++i)
                tab[i, wysokosc - 1] = '0';

            int obecnaLitera = 0;
            for (int i = 0; i < szerokosc; ++i)
            {
                for (int j = 0; j < wysokosc; ++j)
                {
                    if (tab[klucz[i], j] != '0')
                        tab[klucz[i], j] = szyfrogramPrzestawieniaBTextBox.Text[obecnaLitera++];
                }
            }

            StringBuilder wynik = new();
            for (int i = 0; i < wysokosc; ++i)
            {
                for (int j = 0; j < szerokosc; ++j)
                {
                    if (tab[j, i] != '0')
                        wynik.Append(tab[j, i]);
                }
            }
            tekstJawnyPrzestawieniaBTextBox.Text = wynik.ToString();
        }

        // metoda konwertująca słowo do kolejności kolumn (tablica pod każdym indeksem ma numer w kolejności)
        private int[] slowoDoKluczaC(string slowo)
        {
            int?[] klucz = new int?[slowo.Length];

            int obecny = 0;
            while (obecny < klucz.Length)
            {
                // szukanie najmniejszej litery, która nie znajduje się na liście
                char? najmniejszy = null;
                int najmniejszyIndex = 0;
                for (int i = 0; i < slowo.Length; ++i)
                {
                    if ((najmniejszy == null || slowo[i] < najmniejszy) && klucz[i] == null)
                    {
                        najmniejszy = slowo[i];
                        najmniejszyIndex = i;
                    }
                }
                // dodanie kolejnej litery do listy
                klucz[najmniejszyIndex] = obecny++;
            }

            return klucz.Cast<int>().ToArray();
        }

        private void szyfrujPrzestawieniaCButton_Click(object sender, RoutedEventArgs e)
        {
            string tekstJawny = tekstJawnyPrzestawieniaCTextBox.Text.Replace(" ", null);
            int[] klucz = slowoDoKluczaC(kluczPrzestawieniaCTextBox.Text);
            int[] kolejnosc = slowoDoKluczaB(kluczPrzestawieniaCTextBox.Text);
            List<char?[]> tab = new();
            tab.Add(new char?[klucz.Length]);

            int obecnaLitera = 0;
            int obecnyWiersz = 0;
            int iterator = 0;
            // wpisywanie liter w wiersze do momentu napotkania miejsca w którym znajduje się w słowie kluczu numer obecnego wiersza
            while (obecnaLitera < tekstJawny.Length)
            {
                tab[obecnyWiersz][iterator] = tekstJawny[obecnaLitera++];
                if (klucz[iterator] == obecnyWiersz && obecnaLitera < tekstJawny.Length)
                {
                    tab.Add(new char?[klucz.Length]);
                    ++obecnyWiersz;
                    iterator = 0;
                }
                else
                    ++iterator;
            }

            // odczytanie kolumnami w kolejności alfabetycznej liter w słowie kluczu
            StringBuilder wynik = new();
            for (int i = 0; i < klucz.Length; ++i)
            {
                for (int j = 0; j < tab.Count; ++j)
                {
                    if (tab[j][kolejnosc[i]] != null)
                        wynik.Append(tab[j][kolejnosc[i]]);
                }
            }

            szyfrogramPrzestawieniaCTextBox.Text = wynik.ToString();
        }

        private void deszyfrujPrzestawieniaCButton_Click(object sender, RoutedEventArgs e)
        {
            string szyfrogram = szyfrogramPrzestawieniaCTextBox.Text.Replace(" ", null);
            int[] klucz = slowoDoKluczaC(kluczPrzestawieniaCTextBox.Text);
            int[] kolejnosc = slowoDoKluczaB(kluczPrzestawieniaCTextBox.Text);
            List<char?[]> tab = new();
            tab.Add(new char?[klucz.Length]);

            int obecnaLitera = 0;
            int obecnyWiersz = 0;
            int iterator = 0;

            // wygenerowanie miejsc, gdzie będą litery
            while (obecnaLitera < szyfrogram.Length)
            {
                tab[obecnyWiersz][iterator] = '0';
                ++obecnaLitera;
                if (klucz[iterator] == obecnyWiersz && obecnaLitera < szyfrogram.Length)
                {
                    tab.Add(new char?[klucz.Length]);
                    ++obecnyWiersz;
                    iterator = 0;
                }
                else
                    ++iterator;
            }
            // wpisywanie liter kolumnami w kolejności odpowiadającej kolejności alfabetycznej liter w kluczu
            obecnaLitera = 0;
            for (int i = 0; i < klucz.Length; ++i)
            {
                for (int j = 0; j < tab.Count; ++j)
                {
                    if (tab[j][kolejnosc[i]] != null)
                        tab[j][kolejnosc[i]] = szyfrogram[obecnaLitera++];
                }
            }
            // odczytanie wierszami
            StringBuilder wynik = new();
            for (int i = 0; i < tab.Count; ++i)
            {
                for (int j = 0; j < klucz.Length; ++j)
                {
                    if (tab[i][j] != null)
                        wynik.Append(tab[i][j]);
                }
            }

            tekstJawnyPrzestawieniaCTextBox.Text = wynik.ToString();
        }
        private void szyfrujCezarButton_Click(object sender, RoutedEventArgs e)
        {
            int a = 0, b = 0, n = 0;
            int.TryParse(kluczACezarTextBox.Text, out a);
            int.TryParse(kluczBCezarTextBox.Text, out b);
            int.TryParse(kluczNCezarTextBox.Text, out n);

            if (a != 0 && b != 0 && n != 0)
            {
                StringBuilder wynik = new();
                for (int i = 0; i < tekstJawnyCezarTextBox.Text.Length; ++i)
                {
                    // (ax + b) mod n
                    wynik.Append((char)('A' + (((tekstJawnyCezarTextBox.Text[i] - 'A') * a + b) % n)));
                }
                szyfrogramCezarTextBox.Text = wynik.ToString();
            }

        }

        // szukanie odwrotności w pierścieniu
        int szukajOdwrotnosci(int a, int n)
        {
            for (int i = 0; i < n; ++i)
            {
                if ((i * a) % n == 1)
                    return i;
            }
            return -1;
        }

        private void deszyfrujCezarButton_Click(object sender, RoutedEventArgs e)
        {
            int a = 0, b = 0, n = 0;
            int.TryParse(kluczACezarTextBox.Text, out a);
            int.TryParse(kluczBCezarTextBox.Text, out b);
            int.TryParse(kluczNCezarTextBox.Text, out n);

            if (a != 0 && b != 0 && n != 0)
            {
                int odwrotnoscA = szukajOdwrotnosci(a, n);

                if (odwrotnoscA != -1)
                {
                    StringBuilder wynik = new();
                    for (int i = 0; i < szyfrogramCezarTextBox.Text.Length; ++i)
                    {
                        // ((c + (n - b)) * a^-1) mod n
                        wynik.Append((char)('A' + (((szyfrogramCezarTextBox.Text[i] - 'A') + (n - b)) * odwrotnoscA) % n));
                    }
                    tekstJawnyCezarTextBox.Text = wynik.ToString();
                }
            }
        }

        string szyfrujVigener(string tekstJawny, string klucz)
        {
            StringBuilder wynik = new();
            for (int i = 0; i < tekstJawny.Length; ++i)
            {
                int przesuniecie = tekstJawny[i] - 'A';
                int pozycja = klucz[i % klucz.Length] - 'A';
                char znak = (char)('A' + (przesuniecie + pozycja) % 26);
                wynik.Append(znak);
            }
            return wynik.ToString();
        }

        private void szyfrujVigenerButton_Click(object sender, RoutedEventArgs e)
        {
            string tekstJawny = tekstJawnyVigenerTextBox.Text;
            string klucz = kluczVigenerTextBox.Text;

            string wynik = szyfrujVigener(tekstJawny, klucz);

            szyfrogramVigenerTextBox.Text = wynik;
        }

        private void deszyfrujVigenerButton_Click(object sender, RoutedEventArgs e)
        {
            string klucz = kluczVigenerTextBox.Text;
            StringBuilder odwroconyKlucz = new();

            // wygenerowanie przeciwnych liter w kluczu, by odszyfrować
            for(int i = 0; i < klucz.Length; ++i)
                odwroconyKlucz.Append((char)('A' + ((26 - (klucz[i] - 'A')) % 26)));

            // syfrowanie z odwróconym kluczem
            string wynik = szyfrujVigener(szyfrogramVigenerTextBox.Text, odwroconyKlucz.ToString());
            tekstJawnyVigenerTextBox.Text = wynik;
        }
    }
}
