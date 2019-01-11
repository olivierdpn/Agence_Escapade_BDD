using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Newtonsoft.Json;
using Json;




namespace BDD_FILROUGE22
{
    class MainClass
    {
        public static void E1()
        {
            XmlDocument docXml = new XmlDocument();

            // En-tête XML 
            docXml.CreateXmlDeclaration("1.0", "UTF-8", "no"); // no : pas de DTD
            docXml.CreateComment("xml version='1.0' encoding='UTF-8'");

            XmlElement racine = docXml.CreateElement("Client");
            docXml.AppendChild(racine);
            racine.SetAttribute("sexe", "masculin");

            XmlElement Balise1 = docXml.CreateElement("Nom");
            string nomClient = "DUPAIN";
            Balise1.InnerText = nomClient;

            racine.AppendChild(Balise1);

            XmlElement Balise2 = docXml.CreateElement("Adresse");
            Balise2.InnerText = "ESILV, la defense";
            Balise2.SetAttribute("arrondissement", "16eme");
            racine.AppendChild(Balise2);

            XmlElement Balise3 = docXml.CreateElement("Date");
            Balise3.InnerText = "semaine 14";
            racine.AppendChild(Balise3);

            XmlElement Balise4 = docXml.CreateElement("Sejour");
            Balise3.InnerText = "visite de la Défense";
            racine.AppendChild(Balise3);

            string nom = "M1";

            // Enregistrement du document XML  (bin/debug de Visual)
            docXml.Save(nom + ".xml");

            // Permet d'ouvrir un document 
            ProcessStartInfo Ouvrir = new ProcessStartInfo(@nom + ".xml", "");
            Process.Start(Ouvrir);
         
        }

        static string RecuperationNom()
        {
            string nomClient = "";

            XPathDocument doc = new XPathDocument("M1.xml");
            XPathNavigator nav = doc.CreateNavigator();

            //créer une requete XPath

            string maRequeteXPath = "/Client/Nom";
            XPathExpression expr = nav.Compile(maRequeteXPath);

            //exécution de la requete

            XPathNodeIterator nodes = nav.Select(expr);// exécution de la requête XPath

            while (nodes.MoveNext())
            {

                nomClient = nodes.Current.Value;
            }

            return nomClient;
        }

        static string E2(string nomClient)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "select Nom, NumeroClient from Client";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string nom = "";
            string numeroClient = "";
            while (reader.Read())// Parcours l'ensemble des lignes des données (renvoie false quand une ligne est vide)
            {
                nom = reader.GetString(0);  // récupération de la 1ère colonne 
                if (nom == nomClient) //cas du client existant
                { 
                    numeroClient = reader.GetString(1); // on récupere le numero client 
                    Console.WriteLine("Le client existe déjà, son numéro client est" + numeroClient+".\n\n");
                } 
            }
            reader.Close();
            if (numeroClient == "") // cas du nouveau client 
            {
                MySqlCommand nouvellecommmande = connection.CreateCommand();
                numeroClient = "3344";
                nouvellecommmande.CommandText = "INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES ("+numeroClient+", '"+nomClient+"', 'Olivier', '143rueduTemple', 0663815407, 'dupain@icloud.fr');";
                reader = nouvellecommmande.ExecuteReader();
                reader.Close();
                Console.WriteLine("Le client numero "+numeroClient+" a été ajouté à la base de donnée.\n\n");
            }


            connection.Close();
            Console.WriteLine();
            return numeroClient;
        }
        static string E3()
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "select Immatriculation , est_dispo from Voiture v, Stationne s , Parking p where p.CodePostal = '75016' and p.CodeParking = s.Parking_CodeParking and s.Voiture_immatriculation = v.immatriculation; ";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            string ImmatVoiture = "";
          
            while(reader.Read() && reader.GetString(1)=="1")
            {
                ImmatVoiture = reader.GetString(0);
            }

            reader.Close();

            if (ImmatVoiture == "")
            {
                MySqlCommand nouvellecommmande = connection.CreateCommand();
                nouvellecommmande.CommandText = "select Immatriculation from Voiture where est_dispo='1';";
                reader = nouvellecommmande.ExecuteReader();
                reader.Read();
                ImmatVoiture = reader.GetString(0);
                reader.Close();

                MySqlCommand nouvellecommmande2 = connection.CreateCommand();
                nouvellecommmande2.CommandText = "update Stationne set Parking_CodeParking='A16' where Voiture_Immatriculation='" + ImmatVoiture + "';";
                reader = nouvellecommmande2.ExecuteReader();
                reader.Close();
            }

            connection.Close();

            return ImmatVoiture;
        }

        static List<Appartement> E5_deserialisation()
        {
            List<List<string>> echantillon = new List<List<string>>();
            List<string> propositions = new List<string>();


            StreamReader reader = new StreamReader("reponseRBNP.Json");
            string JsonToString = reader.ReadToEnd();

            List<Appartement> apparts = JsonConvert.DeserializeObject<List<Appartement>>(JsonToString);

            return apparts;
        }

        static List<Appartement> choisirAppartement(List<Appartement> apparts)
        {
            List<Appartement> selection = new List<Appartement>();

            foreach(Appartement i in apparts)
            {
                if(i.borough==16 && i.overall_satisfaction>=4.5 && i.bedrooms==1 && i.availability=="yes")
                {
                    selection.Add(i);
                }
            }
            return selection;
        }

        static void AfficherPrettyJson(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier);
            JsonTextReader jreader = new JsonTextReader(reader);
            while (jreader.Read())
            {
                if (jreader.Value != null)
                {
                    if (jreader.TokenType.ToString() == "PropertyName")
                    {
                        Console.Write(jreader.Value + " : ");
                    }
                    else
                    {
                        Console.WriteLine(jreader.Value);
                    }
                }
                else
                {
                    if (jreader.TokenType.ToString() == "StartObject") Console.WriteLine("Nouvel objet\n--------------");
                    if (jreader.TokenType.ToString() == "EndObject") Console.WriteLine("-------------\n");
                    if (jreader.TokenType.ToString() == "StartArray") Console.WriteLine("Liste\n");
                }
            }
            jreader.Close();
            reader.Close();
        }

        static string Info_RBNP_Json(Appartement appartChoisi)
        {
            
            string monFichier = "reponse_d_escapade.json";

            //informations sur l'appartement choisi
            string host_id = Convert.ToString(appartChoisi.host_id);
            string room_id = Convert.ToString(appartChoisi.room_id);
            string week = appartChoisi.week;
            appartChoisi.availability = "no";
            string availibility = appartChoisi.availability;

            //instanciation des "writer"
            StreamWriter writer = new StreamWriter(monFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            //debut du fichier Json
            jwriter.WriteStartObject();

            //debut du tableau Json
            jwriter.WritePropertyName("Reponse_d_ESCAPADE");
            jwriter.WriteStartArray();
           
            jwriter.WriteStartObject();
            jwriter.WritePropertyName("host_id");
            jwriter.WriteValue(host_id);
            jwriter.WritePropertyName("room_id");
            jwriter.WriteValue(room_id);
            jwriter.WritePropertyName("Week");
            jwriter.WriteValue(week);
            jwriter.WritePropertyName("availibility");
            jwriter.WriteValue(availibility);
            jwriter.WriteEndObject();

            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //femeture de "writer"
            jwriter.Close();
            writer.Close();

            Process.Start(monFichier);

            return monFichier;

        }

        static void E5_reponse(List<Appartement> selection)
        {
             afficherAppartement(selection);
        }

        static void afficherAppartement(List<Appartement> apparts)
        {
            foreach (Appartement i in apparts)
            {
                Console.WriteLine(i);
            }
        }

        static void E6(List<Appartement> selection, string immat_voiture, string nom_client, string num_client)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "select p.nom, v.place from Voiture v, Parking p, Stationne s where v.immatriculation ='" + immat_voiture + "' and voiture_immatriculation=immatriculation and Parking_CodeParking = CodeParking";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            string nom_parking = reader.GetString(0);
            string num_place = reader.GetString(1);
            reader.Close();

            Console.WriteLine("Votre voiture d'immaticulation "+immat_voiture+" se trouve dans le parking "+ nom_parking+" et est garée à la place "+num_place+".\n");

            XmlDocument docXml = new XmlDocument();

            docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.CreateComment("xml version='1.0' encoding='UTF-8'");
           
            XmlElement racine_de_racine = docXml.CreateElement("Message_M2");
            docXml.AppendChild(racine_de_racine);
           
            XmlElement info_voyage = docXml.CreateElement("Detail_du_voyage");
            racine_de_racine.AppendChild(info_voyage);

            XmlElement reservation = docXml.CreateElement("Numero_reservation");
            reservation.InnerText = "00555";
            info_voyage.AppendChild(reservation);

            XmlElement adherent = docXml.CreateElement("Adherant");
            info_voyage.AppendChild(adherent);

            XmlElement nom = docXml.CreateElement("Nom");
            nom.InnerText = nom_client;
            adherent.AppendChild(nom);

            XmlElement numero_adherent = docXml.CreateElement("Numero_Adherent");
            numero_adherent.InnerText = num_client;
            adherent.AppendChild(numero_adherent);

            XmlElement detail_reservation = docXml.CreateElement("Detail_reservation");
            info_voyage.AppendChild(detail_reservation);

            XmlElement nom_theme = docXml.CreateElement("Nom_theme");
            nom_theme.InnerText = "16_ieme_arrondissement";
            detail_reservation.AppendChild(nom_theme);

            XmlElement date_sejour = docXml.CreateElement("Date_sejour");
            date_sejour.InnerText = "Semaine_14";
            detail_reservation.AppendChild(date_sejour);

            XmlElement etat_resa = docXml.CreateElement("etat_resa");
            etat_resa.InnerText = "sejour_valide";
            detail_reservation.AppendChild(etat_resa);

            XmlElement detail_voiture = docXml.CreateElement("Detail_voiture");
            info_voyage.AppendChild(detail_voiture);

            XmlElement Nom_parking = docXml.CreateElement("Nom_parking");
            Nom_parking.InnerText = nom_parking;
            detail_voiture.AppendChild(Nom_parking);

            XmlElement Num_place = docXml.CreateElement("Numero_place");
            Num_place.InnerText = num_place;
            detail_voiture.AppendChild(Num_place);

            XmlElement immatriculation = docXml.CreateElement("Immatriculation_du_vehicule");
            immatriculation.InnerText = immat_voiture;
            detail_voiture.AppendChild(immatriculation);


            for (int i = 0; i < 3; i++)
            {
                XmlElement racine = docXml.CreateElement("Appartement_propose");
                racine_de_racine.AppendChild(racine);

                XmlElement Balise1 = docXml.CreateElement("Reference_hebergement");
                Balise1.InnerText = Convert.ToString(selection.ElementAt(i).room_id);
                racine.AppendChild(Balise1);

                XmlElement Balise2 = docXml.CreateElement("Type_de_chambre");
                Balise2.InnerText = Convert.ToString(selection.ElementAt(i).room_type);
                racine.AppendChild(Balise2);

                XmlElement Balise3 = docXml.CreateElement("Pres_de");
                Balise3.InnerText = Convert.ToString(selection.ElementAt(i).neighborhood);
                racine.AppendChild(Balise3);

                XmlElement Balise4 = docXml.CreateElement("Theme");
                Balise4.InnerText = Convert.ToString(selection.ElementAt(i).borough);
                racine.AppendChild(Balise4);

                XmlElement Balise5 = docXml.CreateElement("Satisfaction_generale");
                Balise5.InnerText = Convert.ToString(selection.ElementAt(i).overall_satisfaction);
                racine.AppendChild(Balise5);

                XmlElement Balise6 = docXml.CreateElement("Nombre_de_chambre");
                Balise6.InnerText = Convert.ToString(selection.ElementAt(i).bedrooms);
                racine.AppendChild(Balise6);

                XmlElement Balise7 = docXml.CreateElement("Prix");
                Balise7.InnerText = Convert.ToString(selection.ElementAt(i).price);
                racine.AppendChild(Balise7);

                XmlElement Balise8 = docXml.CreateElement("Disponibilite");
                Balise8.InnerText = Convert.ToString(selection.ElementAt(i).availability);
                racine.AppendChild(Balise8);
            }

            string nom_fichier = "M2";

            // Enregistrement du document XML  (bin/debug de Visual)
            docXml.Save(nom_fichier + ".xml");

            // Permet d'ouvrir un document 
            ProcessStartInfo Ouvrir = new ProcessStartInfo(@nom_fichier + ".xml", "");
            Process.Start(Ouvrir);
        }

        public static string E7(string numero_immat)
        {
            //créer l'arborescence des chemins XPath du document
           
            XPathDocument doc = new XPathDocument("M3.xml");
            XPathNavigator nav = doc.CreateNavigator();

            //créer une requete XPath

            string maRequeteXPath = "/Confirmation/*";
            XPathExpression req = nav.Compile(maRequeteXPath);

            //exécution de la requete

            XPathNodeIterator nodes = nav.Select(req);// exécution de la requête XPath

            string text = "Confirmation du client :\n";

            Dictionary<string, string> info_resa = new Dictionary<string, string>();

            while (nodes.MoveNext())
            {
                text += nodes.Current.Name + " : " + nodes.Current.Value + "\n";
                info_resa.Add(nodes.Current.Name,nodes.Current.Value);
            }
         
            //Mise à jour SQL

            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;";//Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `ESCAPADE`.`Reserve` (`NumeroRésa`, `Client_NumeroClient`, `Séjour_idSejour`, `Voiture_Immatriculation`) VALUES (4,'"+info_resa["Numero_Client"]+"','"+info_resa["Numero_sejour"]+"','"+numero_immat+"');";
            MySqlDataReader reader;
            try
            {
                reader = command.ExecuteReader();
                reader.Close();
            }
            catch(Exception)
            {
                Console.WriteLine("La réservation existe déjà !");
            }

            Console.WriteLine("La réservation a été ajouté à la base de donnée.\n\n");

            MySqlCommand commande2 = connection.CreateCommand();
            commande2.CommandText = "update séjour set estConfirme = 1 where idSejour='"+info_resa["Numero_sejour"]+"';";
            reader = commande2.ExecuteReader();
            reader.Close();



            return text; //affichage demo

        }

        // CHECK OUT : 

        static string MAJPositionVoiture(string numeroClient)
        {
            string nouvelle_place = "";

            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select Place from Voiture natural join Reserve natural join Client where NumeroClient='" + numeroClient + "' and est_rendu=1;";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            nouvelle_place = reader.GetString(0);
            reader.Close();

            connection.Close();
            Console.WriteLine("La voiture est rendue est se trouve à la place "+nouvelle_place+".\n\n");
            return nouvelle_place;
        }

        static void VerificationControleur(string immatVoiture)
        {


            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //Serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command1 = connection.CreateCommand();
            command1.CommandText = "update Voiture set est_verif=0, est_dispo=0 where Immatriculation='" + immatVoiture + "';";
            MySqlDataReader reader1;
            reader1 = command1.ExecuteReader();
            reader1.Close();


            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = "INSERT INTO `Entretien` (`idEntretien`, `Motif`, `Date`, `Voiture_Immatriculation`, `Controleur_idControleur`) VALUES (0004, 'Netoyage', '2018-01-24', '" + immatVoiture + "', 001 );";
            MySqlDataReader reader2;
            try
            {
                reader2 = command2.ExecuteReader();
                reader2.Close();
            }
            catch(Exception)
            {
                Console.WriteLine("Cet entretien existe déjà dans la base de donnée.");
            }

            connection.Close();
            Console.WriteLine("La voiture d'immatriculation "+immatVoiture+" a été mise en entretien pour nettoyage, elle n'est plus disponible");
        }

        public static void NettoyageRemiseEnService(string immatVoiture)
        {
            Console.WriteLine("Appuyer pour nettoyer le véhicule\n");
            Console.ReadKey();
            Console.WriteLine("Véhicule nettoyé !\n\n");

            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;"; //serveur prof
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command1 = connection.CreateCommand();
            command1.CommandText = "update Voiture set est_verif=1, est_dispo=1 where Immatriculation='" + immatVoiture + "';";
            MySqlDataReader reader1;
            reader1 = command1.ExecuteReader();
            reader1.Close();

            Console.WriteLine("La voiture est de nouveau disponible dans la base de donnée.\n\n");
        }

        public static void TableaudeBord1(string immat)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;";
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select idEntretien , Motif , Date , Controleur_idControleur  from Entretien where Voiture_Immatriculation ='" + immat + "';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int numInter = Convert.ToInt32(reader.GetString(0));
                Console.WriteLine("Intervention numero :" + numInter);


                string motif = reader.GetString(1);
                string date = Convert.ToString(reader.GetString(2));
                int numCon = Convert.ToInt32(reader.GetString(3));

                Console.WriteLine("");
                Console.WriteLine("Motif : " + motif);
                Console.WriteLine("Date : " + date);
                Console.WriteLine("Numero du controleur : " + numCon);
                Console.WriteLine("\n\n");

            }
            reader.Close();
            connection.Close();

        }

        static void TableaudeBord2(string numC)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;";
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select idSejour , Date , Theme , `Hebergement (par API)`  from Séjour a, Client b, Reserve c where b.NumeroClient = '" + numC + "' and b.NumeroClient=c.Client_NumeroClient and c.Séjour_idSejour=a.idSejour;";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string numSejour = reader.GetString(0);
                Console.WriteLine("Sejour numero :" + numSejour);


                string date = reader.GetString(1);
                string theme = Convert.ToString(reader.GetString(2));
                string hebergement = reader.GetString(3);


                Console.WriteLine("");
                Console.WriteLine("Date : " + date);
                Console.WriteLine("Theme : " + theme);
                Console.WriteLine("Hebergement : " + hebergement);
                Console.WriteLine("\n\n");

            }
            reader.Close();
            connection.Close();

        }
        static int max(int a, int b, int c)
        {
            if (a >= b && a >= c) return 1;
            if (b >= a && b >= c) return 2;
            else return 3;

        }
        static void TableaudeBord3(string numC)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=dupa_oliv;UID=S6-DUPA-OLIV;PASSWORD=9297;SSLMODE = none;";
            //string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Escapade;UID=root;PASSWORD=Saisies73;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select Séjour_idSejour from Reserve";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            int S1 = 0;
            int S2 = 0;
            int S3 = 0;

            while (reader.Read())
            {
                if (reader.GetString(0) == "0001") S1++;
                if (reader.GetString(0) == "0002") S2++;
                if (reader.GetString(0) == "0003") S3++;
            }
            int monMax = max(S1, S2, S3);
            if (monMax == 1) Console.WriteLine("Le sejour dans le 1er arrodissement est le sejour le plus rentable, il a ete choisi a " + S1 + " reprises.");
            if (monMax == 2) Console.WriteLine("Le sejour dans le 18ieme arrondissement est le sejour le plus rentable, il a ete choisi a " + S2 + " reprises.");
            if (monMax == 3) Console.WriteLine("Le sejour dans le 12ieme arrondissement est le sejour le plus rentable, il a ete choisi a " + S3 + " reprises.");

            reader.Close();
            connection.Close();
        }

        public static void DemoEscapade()
        {
            Console.WriteLine("\t\t\t\tBienvenue dans la démonstration du logiciel Escapade :\n\n\n");
           
            Console.WriteLine("\t\tEtape 1: Génération du message XML de l'application mobile :\n(Appuyez pour continuer...)");
            Console.ReadKey();
            E1();
            Console.WriteLine("\nVotre fichier XML vient de s'ouvrir sur votre ordinateur, il est enregistré le cas échant sous le nom de M1.xml dans votre debug.\n(Appuyez pour continuer...)\n\n");
            Console.ReadKey();

            Console.WriteLine("\t\tEtape 2: Vérification de l'existence client :\n(Appuyez pour continuer...)");
            Console.ReadKey();
            string nomClient = RecuperationNom();
            Console.WriteLine("D'après le premier message XML, le code c# à récupérer le nom du client qui est "+nomClient+", le programme de démonstration va maintenant vérifier son existence dans la base de donnée.\n(Appuyez pour continuer)");
            Console.ReadKey();
            string numClient = E2(nomClient);
            Console.WriteLine("\n\n");

            Console.WriteLine("\t\tEtape 3: Selection d'une voiture disponible dans l'arrondissement souhaité :\n(Appuyez pour continuer...)");
            Console.ReadKey();
            string immatVoiture = E3();
            Console.WriteLine("Le logiciel vous a selectionné la voiture d'immatriculation "+immatVoiture+".\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine("\t\tEtape 4: Le programme a bien recu la réponse RBNP qui vient d'ouvrir sur votre ordinateur !\n(Appuyez pour continuer...)\n\n");
            Process.Start("reponseRBNP.json");
            Console.ReadKey();

            Console.WriteLine("\t\tEtape 5: Selection d'une proposition de 3 appartements répondant aux critères :\n(Appuyez pour continuer...)");
            List<Appartement> touslesappartements = new List<Appartement>();
            List<Appartement> appartement_selection = new List<Appartement>();
            touslesappartements = E5_deserialisation();
            appartement_selection = choisirAppartement(touslesappartements);
            Console.WriteLine("D'après vos critères de selection :\nArrondissment : 16ème\nNombre de chambres : 1\nEvaluation minimum 4.5\nDisponibilité : oui\n\n");
            Console.WriteLine("Le logiciel a selectionné 3 propositions différentes :\n(Appuyez...)\n\n");
            Console.ReadKey();
            for (int i = 0; i < 3;i++)
            {
                Console.WriteLine(appartement_selection[i].ToString());
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Pour les besoins de cette démonstration, nous avons choisi le premier appartement selectionné comme celui désiré par le client.\n(Appuyez pour continuer)");
            Console.ReadKey();
            string nomFichier = Info_RBNP_Json(appartement_selection[0]);
            Process.Start(nomFichier);
            Console.WriteLine("Le message JSON envoyé par Escapade à l'API pour confirmer la réservation de l'appartement choisi vient de s'ouvrir.\n(Appuyez pour continuer...)\n\n\n");
            Console.ReadKey();

            Console.WriteLine("\t\tEtape 6: Le programme crée un dossier de réservation (non confirmé), qui l'exporte en XML. \n(Appuyez pour continuer...)");
            Console.ReadKey();
            E6(appartement_selection, immatVoiture, nomClient, numClient);
            Console.WriteLine("Le message XML vient de s'ouvrir sur votre ordinateur.\n(Appuyez pour continuer...)\n\n");
            Console.ReadKey();

            Console.WriteLine("\t\tEtape 7: Validation du séjour \n(Appuyez pour continuer...)");
            Console.ReadKey();
            Console.WriteLine("Le message M3 de confirmation client écrit à la main vient de s'ouvrir sur votre ordinateur.\n(Appuyez pour continuer...)\n\n");
            Process.Start("M3.xml");
            Console.ReadKey();
            string retour_E7 = E7(immatVoiture);
            Console.WriteLine("Le programme de démo va maintenant confirmer la réservation dans la base de donnée à partir du message M3\n(Appuyez...)\n\n");
            Console.ReadKey();
            Console.WriteLine(retour_E7+"\n\nLe séjour associé à la reservation a été validé dans la base de donnée.\n\n(Appuyez...)\n\n\n");
           
            Console.ReadKey();
            Console.WriteLine(" \n\n\t\t---CHECK_OUT---\n\n\n ");


            Console.WriteLine("1. Mise à jour de la position du véhicule par extraction des données dans la BD\n(Appuyez...)\n\n ");
            Console.ReadKey();
            MAJPositionVoiture(numClient);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine("2. Vérification par un controleur, le programme rend indisponible la voiture et crée une intervention pour nettoyage dans la BD\n(Appuyez...)\n\n ");
            Console.ReadKey();
            VerificationControleur(immatVoiture);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine("3. Nettoyage de la voiture puis remettre la voiture disponible dans la BD\n(Appuyez...)\n\n ");
            Console.ReadKey();
            NettoyageRemiseEnService(immatVoiture);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine(" \n\n\t\t---TABLEAU DE BORD ESCAPADE---\n\n\n ");


            Console.WriteLine("1. Historique de tous les entretiens pour un véhicule donné :\n(Appuyez...)\n\n ");
            Console.ReadKey();
            TableaudeBord1(immatVoiture);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine("2. Historique de tous les séjours du client traité précedemment :\n(Appuyez...)\n\n ");
            Console.ReadKey();
            TableaudeBord2(numClient);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.WriteLine("3. Etude de la rentabilité des séjours :\n(Appuyez...)\n\n ");
            Console.ReadKey();
            TableaudeBord3(numClient);
            Console.WriteLine("\n(Appuyez...)\n\n");
            Console.ReadKey();

            Console.ReadKey();

        }

        public static void Main(string[] args)
        {
            DemoEscapade();
        }
          
    }
}
