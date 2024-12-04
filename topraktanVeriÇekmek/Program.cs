using System;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace topraktanVeriÇekmek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int veriToplamı = 0;
            string csvFilePath = "verilerTesst.csv";
            StreamReader reader = new StreamReader(csvFilePath);
            StreamWriter sw = new StreamWriter("Topraktan.csv");
           
            Console.WriteLine("İLAN NO;KATEGORİ;İLAN TİPİ;TOPLAM DAİRE SAYISI;TESLİM TARİHİ;FİRMAADI; PROJE; ADI KOORDİNAT ");
            sw.WriteLine("İLAN NO;KATEGORİ;İLAN TİPİ;TOPLAM DAİRE SAYISI;TESLİM TARİHİ;FİRMA ADI; PROJE ADI;KOORDİNAT");


            
            for (int j = 1; j <=3760; j++)
            {
                veriToplamı++;
                string yol = reader.ReadLine();
                HtmlDocument doc = new HtmlDocument();
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(yol);
                doc.LoadHtml(html);
                
                for (int i = 1; i <= 8; i++)
                {
                    if (i == 2 || i == 5 )
                    {
                        i++;
                    }


                    var nodes = doc.DocumentNode.SelectNodes("//table/tbody/tr[" + i + "]/td[2]");
                    

                    if (nodes == null)
                    {

                        Console.Write(" " + ";");
                        sw.Write(" " + ";");


                    }
                    else
                    {
                        foreach (var node in nodes)
                        {
                            Console.Write(node.InnerText + ";");
                            sw.Write(node.InnerText + ";");
                        }
                    }
                }
                var nodes1 = doc.DocumentNode.SelectNodes("//h2[1]");
                string oge = nodes1[0].InnerText;
                Console.Write(oge +";");
                sw.Write(oge+ ";");
                var Koordinatlar = doc.DocumentNode.SelectNodes("//*[@id=\"gmap\"]/script[2]");
                if(Koordinatlar == null)
                {
                    Console.Write(" " + ";");
                   sw.Write(" " + ";");
                }
                else
                {
                    foreach (var kortinat in Koordinatlar)
                    {
                        int baslangic = kortinat.InnerText.IndexOf("center: [") + 9;
                        int bitis = kortinat.InnerText.IndexOf("]", baslangic);
                        string yeni = kortinat.InnerText.Substring(baslangic, bitis - baslangic);
                        Console.Write(yeni + "\n");
                        sw.Write(yeni +"\n");
                    }
                }
                    
                
            }
            Console.WriteLine("Toplam Veri: {0}" ,veriToplamı);
            Console.ReadKey();
            
        }
        
    }
}
