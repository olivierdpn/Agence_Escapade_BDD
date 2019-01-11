SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `Client`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (1111, 'LEBAS', 'MORGAN', '3villaDavid', 0668562468, 'morgan.lebas@devinci.fr');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (1211, 'DUPAIN', 'OLIVIER', '143ruedutemple', 0663815407, 'o.dupain@gmail.com');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (1121, 'ERNSTMETZMAIER', 'GEOFFROY', '21ruelacauxbelles', 0672918472, 'geo.ernst@orange.fr');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (1112, 'LHOMME', 'EDWARD', '19rueernestpaulpicard', 0612314567, 'lhomme@gmail.com');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (2111, 'SMITH', 'STEPHANIE', '120ruemarechalpicard', 0612892839, 'smith.steph@devinci.fr');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (2211, 'BEZIN', 'SIMON', '98avenuedulac', 0718390491, 'simon.bezin@gmail.com');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (2221, 'ROMANO', 'LEA', '90ruerivoli', 0612864129, 'lea.romano@devinci.fr');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (2222, 'BERNARD', 'BRUNO', '203ruedeslilas', 0617293214, 'bernard.bb@gmail.com');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (3122, 'MONTALEMBERT', 'HENRI', '12avenuedestuileries', 0612739103, 'reeva@gmail.com');
INSERT INTO `Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`, `email`) VALUES (3123, 'JOSSERAND', 'LENA', '120avenuedeclamart', 0718293090, 'josserand.mathieu@devinci.fr');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Séjour`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Séjour` (`idSejour`, `Theme`, `Date`, `Hebergement (par API)`) VALUES ('0001', 'A01', '2018-03-20', '092837481');
INSERT INTO `Séjour` (`idSejour`, `Theme`, `Date`, `Hebergement (par API)`) VALUES ('0002', 'A18', '2018-03-22', '123739201');
INSERT INTO `Séjour` (`idSejour`, `Theme`, `Date`, `Hebergement (par API)`) VALUES ('0003', 'A12', '2018-03-21', '193774900');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Voiture`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('13AZE21', 'PEUGEOT ', '508', 'BERLINE', '', 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('43BDI09', 'AUDI', 'A4', 'BERLINE', 'A9', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('98TGH32', 'CITROEN', 'C4', 'BERLINE', 'A8', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('90VBC78', 'CITROEN', 'CACTUS', 'BERLINE', 'A7', 0, 0, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('32BNV67', 'MERCEDES', 'AMG', 'CABRIOLET', 'A4', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('78DGS89', 'MERCEDES', 'CLASSEC', 'CABRIOLET', NULL, 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('65GDT90', 'MERCEDES', 'CLASSEE', 'CABRIOLET', 'A2', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('90TRE12', 'AUDI', 'A5', 'BERLINE', NULL, 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('72TRE56', 'RENAULT', 'CAPTURE', 'BERLINE', 'A3', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `est_verif`, `est_dispo`, `est_rendu`) VALUES ('18NVA00', 'RENAULT', 'TALISMAN', 'BERLINE', 'A1', 1, 1, 1);

COMMIT;


-- -----------------------------------------------------
-- Data for table `Reserve`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Reserve` (`NumeroRésa`, `Client_NumeroClient`, `Séjour_idSejour`, `Voiture_Immatriculation`) VALUES (1, 1111, '0001', '13AZE21');
INSERT INTO `Reserve` (`NumeroRésa`, `Client_NumeroClient`, `Séjour_idSejour`, `Voiture_Immatriculation`) VALUES (2, 1112, '0002', '78DGS89');
INSERT INTO `Reserve` (`NumeroRésa`, `Client_NumeroClient`, `Séjour_idSejour`, `Voiture_Immatriculation`) VALUES (3, 1211, '0003', '90TRE12');
INSERT INTO `Reserve` (`NumeroRésa`, `Client_NumeroClient`, `Séjour_idSejour`, `Voiture_Immatriculation`) VALUES (4, 2111, '0001', '90VBC78');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Parking`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A01', 'RIVOLI', '2rueBoucher', 75001, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A03', 'BEAUBOURG', '31rueBeaubourg', 75003, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A04', 'LOBAU', '4rueLobau', 75004, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A05', 'SOUFFLOT', '22rueSoufflot', 75005, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A06', 'JARDINDESPLANTES', '25rueGeoffroySaintHilaire', 75006, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A07', 'MAUBOURG', '45quaiD\'Orsay', 75007, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A08', 'CHAMPSELYSEES', '77avenueMarceau', 75008, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A09', 'PIGALLE', '10rueJean-BaptistePigalle', 75009, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A10', 'LARIBOISIERE', '1bisrueAmbroisePare', 75010, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A11', 'OBERKAMPF', '11rueTernaux', 75011, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A12', 'GAREDELYON', '6ruedeRambouillet', 75012, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A13', 'ITALIE', '23rueStephenPichon', 75013, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A14', 'RASPAIL', '120boulevardduMontparnasse', 75014, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A15', 'BEAUGRENELLE', '5quaiAndreCitroen', 75015, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A16', 'VICTOR HUGO', '74avenueVictorHugo', 75016, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A17', 'TERNES', '38avenuedesTernes', 75017, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A18', 'STALINGRAD', '13rueD\'Aubervillier', 75018, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A19', 'PHILARMONIE', '185boulevardSerurier', 75019, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A20', 'ROSAPARKS', '157boulevardMacDonald', 75020, 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('ORY', 'ORLY', 'OrlyAirport', 94310, 'ORLY');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('CDG', 'ROISSY', 'RoissyAirport', 95700, 'ROISSY EN FRANCE');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Controleur`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Controleur` (`idControleur`, `Nom`, `Prenom`) VALUES (001, 'MOULIN', 'Jean');
INSERT INTO `Controleur` (`idControleur`, `Nom`, `Prenom`) VALUES (002, 'BONNAMIE', 'Nathan');
INSERT INTO `Controleur` (`idControleur`, `Nom`, `Prenom`) VALUES (003, 'GERARD', 'Claire');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Entretien`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Entretien` (`idEntretien`, `Motif`, `Date`, `Voiture_Immatriculation`, `Controleur_idControleur`) VALUES (0001, 'NETTOYAGE', '2018-03-20', '90VBC78', 001);
INSERT INTO `Entretien` (`idEntretien`, `Motif`, `Date`, `Voiture_Immatriculation`, `Controleur_idControleur`) VALUES (0002, 'VIDANGE', '2018-02-15', '72TRE56', 002);
INSERT INTO `Entretien` (`idEntretien`, `Motif`, `Date`, `Voiture_Immatriculation`, `Controleur_idControleur`) VALUES (0003, 'GONFLAGE', '2018-02-14', '18NVA00', 003);

COMMIT;


-- -----------------------------------------------------
-- Data for table `Stationne`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('98TGH32', 'A01');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('43BDI09', 'A03');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('90VBC78', 'A06');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('32BNV67', 'A05');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('65GDT90', 'A07');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('72TRE56', 'A09');
INSERT INTO `Stationne` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('18NVA00', 'A03');

COMMIT;

