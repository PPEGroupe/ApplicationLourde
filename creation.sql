USE [master]
GO

-- Création de la Base de données
CREATE DATABASE [MegaCasting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MegaCasting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MegaCasting.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MegaCasting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MegaCasting_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [MegaCasting] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MegaCasting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

USE [MegaCasting]
GO

-- Création de la table CLIENT
CREATE TABLE [dbo].[Client](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Création de la table JOBDOMAIN
CREATE TABLE [dbo].[JobDomain](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_JobDomain] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Création de la table JOB
CREATE TABLE [dbo].[Job](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[IdJobDomain] [bigint] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_JobDomain] FOREIGN KEY([IdJobDomain])
REFERENCES [dbo].[JobDomain] ([Identifier])
GO

ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_JobDomain]
GO

-- Création de la table PARTNER
CREATE TABLE [dbo].[Partner](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Création de la table TYPEOFCONTRACT
CREATE TABLE [dbo].[TypeOfContract](
	[Identifier] [bigint] NOT NULL,
	[Label] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_TypeOfContract] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Création de la table OFFER
CREATE TABLE [dbo].[Offer](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](100) NOT NULL,
	[Reference] [nvarchar](100) NULL,
	[DateStartPublication] [date] NOT NULL,
	[PublicationDuration] [int] NOT NULL,
	[DateStartContract] [date] NOT NULL,
	[JobQuantity] [int] NOT NULL,
	[Latitude] [nchar](10) NULL,
	[Longitude] [nchar](10) NULL,
	[JobDescription] [text] NOT NULL,
	[ProfileDescription] [text] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](50) NOT NULL,
	[IdTypeOfContrat] [bigint] NOT NULL,
	[IdJob] [bigint] NOT NULL,
	[IdClient] [bigint] NOT NULL,
 CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_Job] FOREIGN KEY([IdJob])
REFERENCES [dbo].[Job] ([Identifier])
GO

ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_Job]
GO

ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_TypeOfContract] FOREIGN KEY([IdTypeOfContrat])
REFERENCES [dbo].[TypeOfContract] ([Identifier])
GO

ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_TypeOfContract]
GO

ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_Client] FOREIGN KEY(IdClient)
REFERENCES [dbo].Client ([Identifier])
GO

ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_Client]
GO

