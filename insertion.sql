USE MegaCasting;
GO

DELETE FROM PackPaid;
DELETE FROM Pack;
DELETE FROM Post;
DELETE FROM Offer;
DELETE FROM Client;
DELETE FROM Partner;
DELETE FROM WebUser;
DELETE FROM TypeOfContract;
DELETE FROM Job;
DELETE FROM JobDomain;
DELETE FROM Account;

SET IDENTITY_INSERT TypeOfContract ON
GO
INSERT INTO TypeOfContract(Identifier, Label) VALUES
(1, 'CDD'),
(2, 'CDI'),
(3, 'Intérim'),
(4, 'Intermittens');
SET IDENTITY_INSERT TypeOfContract OFF
GO

SET IDENTITY_INSERT JobDomain ON
GO
INSERT INTO JobDomain(Identifier, Label) VALUES
(1, 'Danse'),
(2, 'Musique'),
(3, 'Cinéma'),
(4, 'Opéra'),
(5, 'Humour'),
(6, 'Thêatre');
SET IDENTITY_INSERT JobDomain OFF
GO

SET IDENTITY_INSERT Job ON
GO
INSERT INTO Job	(Identifier, Label,			IdJobDomain) VALUES
				(1,			'Danseur',		  1),
				(2,			'Réalisateur',    3),
				(3,			'Musicien',		  2),
				(4,			'Acteur/Actrice', 3),
				(5,			'Chanteur',		  4),
				(6,			'Humouriste',	  5),
				(7,			'Caméraman',	  6),
				(8,			'Chanteur',		  2);
SET IDENTITY_INSERT Job OFF
GO

SET IDENTITY_INSERT Account ON
GO
INSERT INTO Account (Identifier, Email,					Password) VALUES
					(1,			'kevin@kpic.fr',		'7c6a180b36896a0a8c02787eeafb0e4c'),
					(2,			'radouane@iia.fr',		'7c6a180b36896a0a8c02787eeafb0e4c'),
					(3,			'martin@iia.fr',		'7c6a180b36896a0a8c02787eeafb0e4c'),
					(4,			'compte@valide.com',	'7c6a180b36896a0a8c02787eeafb0e4c'),
					(5,			'compte@invalide.com',  '7c6a180b36896a0a8c02787eeafb0e4c'),
					(6,			'test@test.com',		'7c6a180b36896a0a8c02787eeafb0e4c');
SET IDENTITY_INSERT Account OFF
GO

SET IDENTITY_INSERT Client ON
GO
INSERT INTO Client	(Identifier, URL,							PhoneNumber,  Fax,	Address,				City,	  ZipCode, Company,		  DateRegister,  IsValid, IdAccount) VALUES
					(1,			'http://ppe.kpic.fr/kevin',		'0612345678', null, '1 rue eugene messmer',	'Laval',  '53000', 'Kpichon',	  GETDATE(),		0,		 1),
					(2,			'http://ppe.kpic.fr/martin',	'0612345678', null, '1 rue de paris', 		'Rennes', '35000', 'Mamicel',	  GETDATE(),		0,		 3),
					(3,			'http://ppe.kpic.fr/radouane',	'0612345678', null, '1 rue d'' alger',		'Lille',  '59000', 'Rbouzerrara', GETDATE(),		0,		 2),
					(4,			'http://ppe.kpic.fr/test1',		'0612345678', null, '1 rue renaise',		'Laval',  '53000', 'Test1',		  GETDATE(),		1,		 4),
					(5,			'http://ppe.kpic.fr/test2',		'0612345678', null, '1 rue alcan',			'Metz',   '57000', 'Test2',		  GETDATE(),		0,		 5);
SET IDENTITY_INSERT Client OFF
GO

SET IDENTITY_INSERT Partner ON
GO
INSERT INTO Partner (Identifier, URL,						  DateRegister, IsValid, IdAccount) VALUES
					(1,			'http://ppe.kpic.fr/partner', GETDATE(),	1,		 4),
					(2,			'http://ppe.kpic.fr/partner', GETDATE(),	0,		 5);
SET IDENTITY_INSERT Partner OFF
GO

SET IDENTITY_INSERT WebUser ON
GO
INSERT INTO WebUser	(Identifier, Firstname, Lastname,		PhoneNumber,  Address,			 City,	 ZipCode, DateRegister, IdAccount) VALUES
					(1,			'Kevin',	'Pichon',		'0612345678', '01 rue intitut', 'Laval', '53000', GETDATE(),	1),
					(2,			'Radouane', 'Bouzerrara',	'0612345678', '01 rue intitut', 'Laval', '53000', GETDATE(),	2),
					(3,			'Martin',	'Amicel',		'0612345678', '01 rue intitut', 'Laval', '53000', GETDATE(),	3);
SET IDENTITY_INSERT WebUser OFF
GO

SET IDENTITY_INSERT Pack ON
GO
INSERT INTO Pack(Identifier, Price,  NumberDays, NumberOffers) VALUES
				(1,			 15.00,  3,			 1),
				(2,			 30.00,  10,		 3),
				(3,			 60.00,  25,		 8),
				(4,			 100.00, 50,		 12),
				(5,			 200.00, 150,		 40),
				(6,			 400.00, 500,		 100);
SET IDENTITY_INSERT Pack OFF
GO

SET IDENTITY_INSERT Offer ON
GO
INSERT INTO Offer(Identifier, Title,						Reference,		DateStartPublication, PublicationDuration,  DateStartContract, JobQuantity, Latitude, Longitude, Address,				  City,	  ZipCode,  IdTypeOfContract, IdJob, IdClient, JobDescription, ProfileDescription) VALUES
				 (1,		  'Recherche Developpeur',		'ADKJ97DQD87D', '25-04-2016',		  5,					'01-05-2016',	   1,		    NULL,	  NULL,		 '22 Rue Eugne Messmer', 'Laval', 53000,	1,				  1,	 1,		  'On recherche un développeur PHP pour plusieurs missions diverses',	'Expert PHP ayant des connaissances en Ajax. BONUS : Connaissances en Javascript et HTML5/CSS3'),
				 (2,		  'Musiciens',					'OIJQ9776986T',	'15-05-2016',		  15,					'20-05-2016',	   4,		    NULL,	  NULL,		 '39 Rue Franche Comté', 'Laval', 53000,	2,				  3,	 3,		  'On recherche un pianiste, un guitariste et un batteur.',				'Pianiste, guitariste ou batteur'),
				 (3,		  'Acteurs pour film de comédie','98UJZDGJ6EFQ','28-04-2016',		  8,					'05-05-2016',	   2,		    NULL,	  NULL,		 'Rue de Rennes',		 'Rennes', 35000,	3,				  4,	 2,		  'On recherche deux acteurs pour un film de comédie',					'Acteur de comédie ayant déjà partcipé dans au moins 5 films'),
				 (4,		  'Opérateurs',					'09ZFLHX5WHS5', '17-03-2016',		  4,					'07-04-2016',	   12,		    NULL,	  NULL,		 '3 Place de France',	 'Nantes', 44000,	4,				  5,	 2,		  'On recherche un chanteur d''opéra',									'Opérateur');
SET IDENTITY_INSERT Offer OFF
GO