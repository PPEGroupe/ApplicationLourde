USE MegaCasting

INSERT INTO Client (URL, Email, PhoneNumber, Fax, [Address], City, ZipCode, Company)
VALUES ('supercasting.fr', 'gonjean@supercasting.fr', '0299606362', '0233656968', '55, rue des petits poids', 'Bazouges', '35664', 'supercasting')

INSERT INTO JobDomain (Label)
VALUES ('Cin�ma')

INSERT INTO Job (Label, IdJobDomain)
VALUES ('Acteur/Actrice', 1)

INSERT INTO TypeOfContract (Label)
VALUES ('CDD')

INSERT INTO Offer (Title, Reference, DateStartPublication, PublicationDuration, DateStartContract, JobQuantity, Latitude, Longitude, JobDescription, ProfileDescription, [Address], City, ZipCode, IdTypeOfContrat, IdJob, IdClient)
VALUES ('Actrice Professionnelle', 'A558479ER', '22/02/2016', 4, '30/03/2016', 2, '10001', '13256', 'Publicit�', 'Actrice avec exp�rience pour jouer une grenouille', '20, rue de la tour', 'Rennes', '35200', 1, 1, 1)