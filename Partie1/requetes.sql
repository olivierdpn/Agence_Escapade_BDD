-- 1. Liste des clients (par numéro de client) :

select * from Client group by NumeroClient;

-- 2. Saisie d'un nouveau client : 

INSERT INTO `ESCAPADE`.`Client` (`NumeroClient`, `Nom`, `Prenom`, `Adresse`, `NumeroTel`,`email` ) VALUES ('3333', 'PEREIRA', 'LUCIE', '19alleedesCerises' , '0619912839', 'lucie.pereira@gmail.com');

-- 3. Liste des voitures, de leur position et de leur disponibilité :

select distinct Immatriculation,est_dispo ,Nom from Stationne S natural join Voiture  V natural join Parking  P where S.Parking_CodeParking=P.CodeParking and Voiture_Immatriculation=Immatriculation;

-- 4. Sélection d'une voiture disponible dans un arrondissement :

select distinct Immatriculation,est_dispo ,Nom from Stationne S natural join Voiture V natural join Parking P where S.Parking_CodeParking=P.CodeParking and Voiture_Immatriculation=Immatriculation and est_dispo=1 and CodeParking = 'A05';

-- 5. Requête de mise à jour de la place de parking d'un véhicule identifié par son immatriculation : 

update Voiture set Place='A5' where Immatriculation='18NVA00';

-- 6. Combien d'opérations de maintenance sur une voiture identifiée par son immatriculation

select count(*) from Entretien where Voiture_Immatriculation='18NVA00';

-- 7. Enregistrement du retour d'une voiture :

update Voiture set est_verif=0,est_rendu=1 where Immatriculation='18NVA00';

-- 8. Nombre de voitures contrôlées par chacun des contrôleurs : 

select count(Voiture_Immatriculation),Controleur_idControleur from Entretien group by Controleur_idControleur;

-- 9. Liste des voitures indisponible et du motif correspondant :

Select Motif ,Voiture_Immatriculation from Entretien, Voiture, Controleur where est_dispo=0 and Voiture_Immatriculation=Immatriculation and idControleur = Controleur_idControleur;

-- 10. Enregistrement d'une opération de maintenance par un des contrôleurs sur une voiture identifiée par son immatriculation

INSERT INTO Entretien (IdEntretien, Motif, Date, Voiture_Immatriculation, Controleur_IdControleur) VALUES ('0004', 'ESSENCE', '2018-03-27', '32BNV67', '001' );






