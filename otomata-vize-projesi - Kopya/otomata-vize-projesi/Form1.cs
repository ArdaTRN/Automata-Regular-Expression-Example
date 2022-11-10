using System;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace otomata_vize_projesi
{
    public partial class Form1 : Form
    {
        int yildizTekrarMiktari = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void uretBtn_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string alfabe = "";
            string model = "";
            int kelimeMiktar = 0;
            if (alfabeTextBox.Text != "" && RegexTextBox.Text != "" && kelimeMiktarTextBox.Text != "")
            {
                alfabe = alfabeTextBox.Text;
                model = RegexTextBox.Text;
                kelimeMiktar = Convert.ToInt32(kelimeMiktarTextBox.Text);
            }
            else
            {
                MessageBox.Show("Lütfen gereken alanlarý doldurun !");
            }

            // List<string> harfler = alfabeAyristir(alfabe);  // harfler uygun degilse null doner. 
            List<string> harfler = alfabe.Split(",").ToList();  // tum harfler kullanarak alfabe yazilabilir.


            Boolean modelUygunMu = true;
            foreach (char c in model)
            {
                Boolean harfVarMi = false;
                foreach (string harf in harfler)
                {
                    if (c.ToString() == harf || c == '*' || c == '(' || c == ')' || c == '+')   // duzenli ifade yani model alfabeye uygun mu kontrolu.
                    {
                        harfVarMi = true;
                    }
                }
                if (harfVarMi == false)
                {
                    modelUygunMu = false;
                }

            }

            if (modelUygunMu)
            {
                List<string> kelimeler = new List<string>();
                if (!model.Contains('('))
                {
                    kelimeler = kelimeUretParantezsiz(model, kelimeMiktar);
                }
                else
                {
                    kelimeler = kelimeUretParantezli(model, kelimeMiktar);
                }

                if (kelimeMiktar > kelimeler.Count())
                {
                    MessageBox.Show(kelimeMiktar + " kelime üretilemiyor. Üretilebilen " + kelimeler.Count() + " kelime listeleniyor...");
                }

                for (int i = 0; i < kelimeler.Count(); i++)
                {
                    listBox1.Items.Add(kelimeler[i]);
                }

                // yan yana yazdirma islemi yapar

                //string kelimelerText = "L={ ";
                //for (int i = 0; i < kelimeler.Count(); i++)
                //{
                //    if (i % 5 == 0 && i != 0)
                //    {
                //        listBox1.Items.Add(kelimelerText);
                //        kelimelerText = "";
                //    }

                //    if (kelimeler.Count() == (i + 1))
                //    {
                //        kelimelerText += kelimeler[i];
                //    }
                //    else
                //    {
                //        kelimelerText += kelimeler[i] + " , ";
                //    }
                //}
                //kelimelerText += " }";
                //listBox1.Items.Add(kelimelerText);

            }
            else
            {
                MessageBox.Show("Lütfen alfabeniz ile uyumlu bir düzenli ifade giriniz !");
            }
            //String kural = "";
            //int miktar = 0;
            //kelimeUretParantezsiz(kural, miktar);
        }
        
        // a*bc*d+ef+g
        private List<string> kelimeUretParantezsiz(string model, int kelimeMiktar)
        {
            List<string> kelimeler = new List<string>();
            int ifadeIndex = 0;
            int karakterTekrarMiktari = 0;
            int ifadeBoyut = -1;
            int artisizIfadeSayac = 0;
            for (int i = 0; i < kelimeMiktar; i++)
            {
                string kelime = "";
                if (model.Contains('+'))
                {
                    string[] ifadeler = model.Split('+');
                    string ifade = ifadeler[ifadeIndex];
                    ifadeBoyut = ifadeler.Length;
                    if (ifade.Contains('*'))
                    {
                        kelime += yildizliKelimeUret(ifade, karakterTekrarMiktari);
                        if (yildizTekrarMiktari == karakterTekrarMiktari)
                        {
                            karakterTekrarMiktari = 0;
                            ifadeIndex++;
                        }
                    }
                    else
                    {
                        kelime += yildizliKelimeUret(ifade, karakterTekrarMiktari);
                        ifadeIndex++;
                    }
                }
                else
                {
                    if (model.Contains('*'))
                    {
                        kelime += yildizliKelimeUret(model, karakterTekrarMiktari);
                        if (yildizTekrarMiktari == karakterTekrarMiktari)
                        {
                            return kelimeler;
                        }
                    }
                    else
                    {
                        kelime += yildizliKelimeUret(model, karakterTekrarMiktari);
                        kelimeler.Add(kelime);
                        return kelimeler;
                    }

                }

                kelimeler.Add(kelime);
                karakterTekrarMiktari++;

                if (ifadeBoyut == ifadeIndex)
                {
                    return kelimeler;
                }
            }
            return kelimeler;
        }

        private string yildizliKelimeUret(string ifade, int karakterTekrarMiktari)
        {
            string kelime = "";
            string geciciKarakter = "";
            for (int i = 0; i < ifade.Length; i++)
            {
                if (ifade[i] == '*')
                {
                    kelime += elemanTekrarEttir(geciciKarakter, karakterTekrarMiktari);
                    geciciKarakter = "";
                }
                else if (ifade.Length != (i + 1) && ifade[i + 1] == '*')
                {
                    geciciKarakter += ifade[i];
                }
                else
                {
                    kelime += ifade[i];
                }
            }
            return kelime;
        }

        private string elemanTekrarEttir(string karakter, int karakterTekrarMiktari)
        {
            string kelime = "";
            if (karakterTekrarMiktari == 0)
            {
                kelime = ".";
            }
            else
            {
                for (int i = 0; i < karakterTekrarMiktari; i++)
                {
                    kelime += karakter;
                }
            }

            return kelime;
        }

        /****************************************************************************************************************************************************/
        // a,b,c,d,e,f,g,h,j,k,l,m,n,o,p,r,s,t,u,x,y,z
        // a((b*+c)+d(ef+g*(h+j*)(kl+m)*n+x)*+o)p+r*(s*+t)*+u          a(b*+c(d*+ef+g)*)      h*((jk*l)m+n)*+o+p*       a(b*+c(d*+ef+g)*)+a*bc*d+ef+g     a*bc*d+ef+g+a()
        private List<string> kelimeUretParantezli(string model, int kelimeMiktar)
        {
            List<string> kelimeler = new List<string>();
            List<string> psizKelimeler = new List<string>();
            List<string> parantezliKelimeler = new List<string>();

            int psizKelimelerIndex = 0;
            int pliKelimelerIndex = 0;
            int disIfadelerIndex = 0;
            List<string> disIfadeler = parantezliModelAyirici(model);
            for (int i = 0; i < kelimeMiktar; i++)
            {
                int disIfadelerSayac = 0;
                int psizKelimelerBoyut = 0;
                int pliKelimelerBoyut = 0;
                bool parantezliKelimeSirasiMi = false;
                foreach (string ifade in disIfadeler)
                {
                    if (disIfadelerIndex == disIfadelerSayac)
                    {
                        if (ifade.Contains('('))
                        {
                            parantezliKelimeSirasiMi = true;
                            parantezliKelimeler = parantezliKelimeListeUretici(ifade, 999);
                            pliKelimelerBoyut = parantezliKelimeler.Count();
                            int sayac = 0;
                            foreach (string item in parantezliKelimeler)
                            {
                                if (pliKelimelerIndex == sayac)
                                {
                                    kelimeler.Add(item);
                                }
                                sayac++;
                            }
                        }
                        else
                        {
                            parantezliKelimeSirasiMi = false;
                            psizKelimeler = kelimeUretParantezsiz(ifade, 999);
                            psizKelimelerBoyut = psizKelimeler.Count();
                            int sayac = 0;
                            foreach (string item in psizKelimeler)
                            {
                                if (psizKelimelerIndex == sayac)
                                {
                                    kelimeler.Add(item);
                                }
                                sayac++;
                            }
                        }
                    }
                    disIfadelerSayac++;
                }
                if (parantezliKelimeSirasiMi)
                {
                    pliKelimelerIndex++;
                    if (pliKelimelerBoyut != 0)
                    {
                        if (pliKelimelerIndex >= pliKelimelerBoyut)
                        {
                            pliKelimelerIndex = 0;
                            disIfadelerIndex++;
                        }
                    }
                    else
                    {
                        disIfadelerIndex++;
                    }
                }
                else
                {
                    psizKelimelerIndex++;
                    if (psizKelimelerBoyut != 0)
                    {
                        if (psizKelimelerIndex >= psizKelimelerBoyut)
                        {
                            psizKelimelerIndex = 0;
                            disIfadelerIndex++;
                        }
                    }
                    else
                    {
                        disIfadelerIndex++;
                    }
                }
            }
            return kelimeler;
        }

        // a,b,c,d,e,f,g,h,j,k,l,m,n,o,p,r,s,t,u,x,y,z
        // a((b*+c)+d(ef+g*(h+j*)(kl+m)*n+x)*+o)p+r*(s*+t)*+u
        private List<string> parantezliKelimeListeUretici(string model, int kelimeMiktari)
        {
            List<string> kelimeler = new List<string>();
            string anaModel = model;
            int icPtezIfadeIndex = 0;
            int sonKelimeIndex = 0;
            for (int j = 0; j < kelimeMiktari; j++)
            {
                model = anaModel;
                string yeniModel = "";
                string icParantezModel = "";
                int icPtezTekrarMiktari = 0;
                int icPtezKelimelerBoyut = 0;
                int icParantezIndex = parantezIndexHesapla(model);
                while (icParantezIndex != -1)
                {
                    bool icParantezGirildiMi = false;
                    for (int i = 0; i < model.Length; i++)
                    {
                        if (icParantezIndex == i)
                        {
                            icParantezGirildiMi = true;
                        }
                        // a((b*+c)+d(ef+g*(h+j*)(kl+m)*n+x)*+o)p+r*(s*+t)*+u
                        // a(b*+c(d*+ef+g)*)+a*bc*d+ef+g
                        if (icParantezGirildiMi)
                        {
                            icParantezModel += model[i];
                            List<string> icPtezKelimeler = new List<string>();

                            if (model[i] == ')')
                            {
                                icParantezModel = icParantezModel.Replace("(", "");
                                icParantezModel = icParantezModel.Replace(")", "");
                                icPtezKelimeler = kelimeUretParantezsiz(icParantezModel, 999);
                                icPtezKelimelerBoyut = icPtezKelimeler.Count();
                                int sayac = 0;
                                foreach (string item in icPtezKelimeler)
                                {
                                    if (icPtezIfadeIndex == sayac)
                                    {
                                        if (model.Length - 1 != i && model[i + 1] == '*')
                                        {
                                            yeniModel += elemanTekrarEttir(item, icPtezTekrarMiktari);
                                            i++;
                                        }
                                        else
                                        {
                                            yeniModel += item;
                                        }
                                    }
                                    sayac++;
                                }
                                icParantezGirildiMi = false;
                            }
                        }
                        else
                        {
                            yeniModel += model[i];
                        }
                    }
                    icParantezModel = "";
                    model = yeniModel;
                    yeniModel = "";
                    icParantezIndex = parantezIndexHesapla(model);
                }
                //kelimeler.Add(model);

                List<string> sonKelimeler = new List<string>();
                if (model.Contains("*"))
                {
                    sonKelimeler = kelimeUretParantezsiz(model, 999);
                    int sayac = 0;
                    foreach (string item in sonKelimeler)
                    {
                        if (sayac == sonKelimeIndex)
                        {
                            kelimeler.Add(item);
                        }
                        sayac++;
                    }
                }
                else
                {
                    kelimeler.Add(model);
                }

                sonKelimeIndex++;
                if (sonKelimeIndex >= sonKelimeler.Count())
                {
                    icPtezIfadeIndex++;
                    if (icPtezIfadeIndex >= icPtezKelimelerBoyut)
                    {
                        return kelimeler;
                    }
                }
            }
            return kelimeler;
        }
        // a,b,c,d,e,f,g,h,j,k,l,m,n,o,p,r,s,t,u,x,y,z
        // a((b*+c)+d(ef+g*(h+j*)(kl+m)*n+x)*+o)p+r*(s*+t)*+u
        // a(b*+c(d*+ef+g)*)+a*bc*d+ef+g

        private List<string> parantezliModelAyirici(string model)
        {
            List<string> ifadeler = new List<string>();
            string ifade = "";
            int parantezSayac = 0;
            for (int i = 0; i < model.Length; i++)  // "a()+a*bc*d+ef+g"
            {
                ifade += model[i];
                if (model[i] == '(')
                {
                    parantezSayac++;
                }
                if (model[i] == ')')
                {
                    parantezSayac--;
                }
                if ((i + 1) < model.Length && model[i + 1] == '+' && parantezSayac == 0)
                {
                    ifadeler.Add(ifade);
                    ifade = "";
                    i++;
                }
            }
            ifadeler.Add(ifade);
            return ifadeler;
        }


        // a((b*+c)+d(ef+g*(h+j*)(kl+m)*+x)*+y) 
        private List<string> parantezliKelimeUretici(string anaModel, int kelimeMiktar)
        {
            List<string> kelimeler = new List<string>();
            Boolean parantezliYildizVarMiGlobal = false;
            Boolean psizIfadeYildizVarMiGlobal = false;
            int ifadeTekrarSayac = 0;
            int parantezliYildizTekrar = 0;
            int psizYildizTekrar = 0;
            int ifadeIndex = 0;
            int ifadeBoyut = 0;
            for (int i = 0; i < kelimeMiktar; i++)
            {
                string model = anaModel;
                int parantezIndex = parantezIndexHesapla(model);
                string anaKelime = "";
                string geciciKural = "";
                Boolean parantezVarMi = false;
                string yeniModel = "";
                int parantezIfadeSayac = 0;
                while (parantezIndex != -1)
                {
                    for (int j = 0; j < model.Length; j++)
                    {
                        if (parantezIndex == j)
                        {
                            parantezVarMi = true;
                        }

                        if (parantezVarMi)
                        {
                            Boolean yildizVarMi = false;
                            string geciciModel = "";
                            geciciKural += model[j];
                            if (model[j] == ')')
                            {
                                if (model.Length - 1 != j && model[j + 1] == '*')
                                {
                                    j++;
                                    yildizVarMi = true;
                                    parantezliYildizVarMiGlobal = true;
                                }
                                // kelime uret
                                if (geciciKural.Contains('+'))
                                {
                                    geciciKural = geciciKural.Replace("(", "");
                                    geciciKural = geciciKural.Replace(")", "");
                                    string[] ifadeler = geciciKural.Split('+');
                                    ifadeBoyut = ifadeler.Length;
                                    if (ifadeIndex >= ifadeler.Length)
                                    {
                                        return kelimeler;
                                    }
                                    string ifade = ifadeler[ifadeIndex];
                                    if (ifade.Contains('*'))
                                    {
                                        ifade = ifade.Replace("*", "");
                                        if (ifadeTekrarSayac == 0)
                                        {
                                            geciciModel += ".";
                                        }
                                        for (int l = 0; l < ifadeTekrarSayac; l++)
                                        {
                                            geciciModel += ifade;
                                        }
                                    }
                                    else
                                    {
                                        geciciModel += ifade;
                                    }

                                }

                                // parantezde yildiz var mi kontrolu
                                if (yildizVarMi)
                                {
                                    if (parantezliYildizTekrar == 0)
                                    {
                                        yeniModel += ".";
                                    }

                                    for (int l = 0; l < parantezliYildizTekrar; l++)
                                    {
                                        yeniModel += geciciModel;
                                    }
                                }
                                else
                                {
                                    if (parantezIndex == 0)
                                    {
                                        geciciKural = geciciKural.Replace("(", "");
                                        geciciKural = geciciKural.Replace(")", "");
                                        geciciModel = geciciKural;
                                    }
                                    yeniModel += geciciModel;
                                }
                                parantezVarMi = false;
                            }
                        }
                        else
                        {
                            Boolean yildizKontrol = false;
                            string geciciKarakter = "";
                            if ((j + 1) != (model.Length) && model[j + 1] == '*')
                            {
                                geciciKarakter += model[j];
                                j++;
                                yildizKontrol = true;
                                psizIfadeYildizVarMiGlobal = true;
                            }
                            else
                            {
                                yeniModel += model[j];
                            }

                            if (yildizKontrol)
                            {
                                if (psizYildizTekrar == 0)
                                {
                                    yeniModel += ".";
                                }

                                for (int l = 0; l < psizYildizTekrar; l++)
                                {
                                    yeniModel += geciciKarakter;
                                }
                            }
                            else
                            {
                                yeniModel += geciciKarakter;
                            }
                            yildizKontrol = false;
                        }
                    }
                    parantezIndex = parantezIndexHesapla(yeniModel);
                    anaKelime = yeniModel;
                    model = yeniModel;
                    yeniModel = "";
                    geciciKural = "";
                    parantezIfadeSayac++;
                }
                int ifadeIndexKontrolcu = 0;
                if (parantezliYildizVarMiGlobal)
                {
                    ifadeTekrarSayac++;
                    if (ifadeTekrarSayac == yildizTekrarMiktari)
                    {
                        ifadeTekrarSayac = 0;
                        // eger diger ifade mevcutsa ve tum ifade tekrarlari eklendiyse diger ifadeye gecer
                        if (ifadeIndexKontrolcu == 0)
                        {
                            ifadeIndex++;
                            ifadeIndexKontrolcu = 1;
                        }
                    }
                    parantezliYildizTekrar++; // parantezdeki ifadeyi tekrarlatir.
                    if (parantezliYildizTekrar > yildizTekrarMiktari)
                    {
                        parantezliYildizTekrar = 0;

                        return kelimeler;
                    }
                }
                if (psizIfadeYildizVarMiGlobal)
                {
                    psizYildizTekrar++;
                    if (psizYildizTekrar > yildizTekrarMiktari)
                    {
                        psizYildizTekrar = 0;

                        if (ifadeIndexKontrolcu == 0)
                        {
                            ifadeIndex++;
                            ifadeIndexKontrolcu = 1;
                        }

                        return kelimeler;

                    }
                }
                kelimeler.Add(anaKelime);
            }
            return kelimeler;
        }


        public int parantezIndexHesapla(string model)
        {
            int parantezIndex = -1;
            for (int i = 0; i < model.Length; i++)
            {
                if (model[i] == '(')
                {
                    parantezIndex = i;
                }
            }

            return parantezIndex;
        }


        private List<string> alfabeAyristir(string alfabe)
        {
            List<string> harfler = alfabe.Split(",").ToList();

            Regex regex = new Regex(@"[a-zA-Z0-9]");
            foreach (string s in harfler)
            {
                Match match = regex.Match(s);
                if (!match.Success) // Ayrilan harflerin uygunluk kontrolu
                {
                    MessageBox.Show("HATA! a-z, A-Z ve 0-9 aralýklarý haricinde bir harf girmeyiniz !");
                    return null;
                }
            }

            return harfler;
        }
    }
}