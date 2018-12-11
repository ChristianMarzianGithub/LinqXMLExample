using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleAppLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.workWithXML();
        }

        public static void workWithXML()
        {
            string xmlDatei = $@"<?xml version='1.0' encoding='UTF-8'?>
                                    <Innosoft version='1.0'>
                                      <Database>
                                        <Clients>
                                          <Client name='Thofehrn'>
                                            <ConnectionString>Provider=SQLNCLI11.1;Persist Security Info=False;User ID=innosoft;Initial Catalog=THOFEHRN;Data Source=SQL2014</ConnectionString>
                                            <Password>202071247168014167143040008192185077068</Password>
                                            <Type>MSSQL</Type>
                                          </Client>
                                          <Client name='Engel'>
                                            <ConnectionString>Provider=SQLNCLI11.1;Persist Security Info=False;User ID=innosoft;Initial Catalog=Engel;Data Source=SQL2014</ConnectionString>
                                            <Password>202071247168014167143040008192185077068</Password>
                                            <Type>MSSQL</Type>
                                          </Client>
                                        <Client name='Telogs'>
                                            <ConnectionString>Provider=SQLNCLI11.1;Persist Security Info=False;User ID=innosoft;Initial Catalog=Telogs;Data Source=SQL2014</ConnectionString>
                                            <Password>202071247168014167143040008192185077068</Password>
                                            <Type>MSSQL</Type>
                                          </Client>
                                        </Clients>
                                      </Database>
                                      <Options></Options>
                                      <Debugging></Debugging>
                                      <Printing></Printing>
                                      <Logging>
                                        <Path>ERROR</Path>
                                      </Logging>
                                      <Visual>
                                        <FormLoader></FormLoader>
                                        <Font></Font>
                                      </Visual>
                                      <DotNetIntegration></DotNetIntegration>
                                    </Innosoft>
                                ";

            XElement XMLelement = XElement.Parse(xmlDatei);

            var liste = from knoten in XMLelement.Descendants()
                        where knoten.Name == "Client"
                        select (string) knoten.Attribute("name");


            var listeKnoten = getClientListe(XMLelement);

            var listeConnString = Program.getKnotenListe(listeKnoten, "ConnectionString");
            Program.ausgabe<string>(liste);
            Program.ausgabeConnString(listeConnString);
            Console.ReadLine();
        }

        public static IEnumerable<XElement> getClientListe(XElement xElement)
        {
            IEnumerable<XElement> retListe = from knoten in xElement.Descendants()
                                             where knoten.Name == "Client"
                                             select knoten;
            return retListe;
        }

        public static IEnumerable<XElement> getKnotenListe(IEnumerable<XElement> listeXElements, string Knoten)
        {
            var listeConnString = from element in listeXElements.Descendants()
                                  where element.Name == Knoten
                                  select element;
            return listeConnString;
        }


        public static void ausgabeConnString(IEnumerable<XElement> liste)
        {
            for (int i = 0; i < liste.Count(); i++)
            {
                Console.WriteLine(liste.ElementAt(i).Value.ToString());
            }
        }

        public static void ausgabe<T>(IEnumerable<T> liste)
        {
            for (int i = 0; i < liste.Count(); i++)
            {
                Console.WriteLine(liste.ElementAt(i));
            }
        }
    }
}
