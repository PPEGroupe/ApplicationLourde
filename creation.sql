USE [master]
GO
/****** Object:  Database [MegaCasting]    Script Date: 08/04/2016 16:06:44 ******/
CREATE DATABASE [MegaCasting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'megacasting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\megacasting.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'megacasting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\megacasting_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MegaCasting] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MegaCasting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MegaCasting] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MegaCasting] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MegaCasting] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MegaCasting] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MegaCasting] SET ARITHABORT OFF 
GO
ALTER DATABASE [MegaCasting] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MegaCasting] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MegaCasting] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MegaCasting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MegaCasting] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MegaCasting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MegaCasting] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MegaCasting] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MegaCasting] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MegaCasting] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MegaCasting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MegaCasting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MegaCasting] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MegaCasting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MegaCasting] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MegaCasting] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MegaCasting] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MegaCasting] SET RECOVERY FULL 
GO
ALTER DATABASE [MegaCasting] SET  MULTI_USER 
GO
ALTER DATABASE [MegaCasting] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MegaCasting] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MegaCasting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MegaCasting] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MegaCasting] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MegaCasting', N'ON'
GO
USE [MegaCasting]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[URL] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[IdAccount] [bigint] NOT NULL,
	[DateRegister] [date] DEFAULT(GETDATE()) NOT NULL,
	[IsValid] [bit] DEFAULT(0) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Job]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[JobDomain]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobDomain](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_JobDomain] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offer]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Reference] [nvarchar](100) NOT NULL,
	[DateStartPublication] [date] NOT NULL,
	[PublicationDuration] [int] NOT NULL,
	[DateStartContract] [date] NOT NULL,
	[JobQuantity] [int] NOT NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
	[JobDescription] [text] NOT NULL,
	[ProfileDescription] [text] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](50) NOT NULL,
	[IdTypeOfContract] [bigint] NOT NULL,
	[IdJob] [bigint] NOT NULL,
	[IdClient] [bigint] NOT NULL,
	[NumberViews] [int] DEFAULT(0) NOT NULL,
	[IsDeleted] [bit] DEFAULT(0) NOT NULL,
 CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pack]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pack](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[NumberDays] [int] NULL,
	[NumberOffers] [int] NULL,
 CONSTRAINT [PK_Pack] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PackPaid]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackPaid](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[IdClient] [bigint] NOT NULL,
	[IdPack] [bigint] NULL,
	[NumberOffers] [int] NULL,
	[NumberDays] [int] NULL,
	[DatePaiment] [date] NOT NULL,
 CONSTRAINT [PK_PackPaid] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partner]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[IdAccount] [bigint] NOT NULL,
	[DateRegister] [date] DEFAULT(GETDATE()) NOT NULL,
	[IsValid] [bit] DEFAULT(0) NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Post]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[DatePost] [date] NOT NULL,
	[Letter] [nvarchar](255) NULL,
	[CV] [nvarchar](255) NOT NULL,
	[IdOffer] [bigint] NOT NULL,
	[IdWebUser] [bigint] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeOfContract]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfContract](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_TypeOfContract] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebUser]    Script Date: 08/04/2016 16:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebUser](
	[Identifier] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[IdAccount] [bigint] NULL,
	[DateRegister] [date] DEFAULT(GETDATE()) NOT NULL,
 CONSTRAINT [PK_WebUser] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Identifier])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Account]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_JobDomain] FOREIGN KEY([IdJobDomain])
REFERENCES [dbo].[JobDomain] ([Identifier])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_JobDomain]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Identifier])
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_Client]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_Job] FOREIGN KEY([IdJob])
REFERENCES [dbo].[Job] ([Identifier])
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_Job]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_TypeOfContract] FOREIGN KEY([IdTypeOfContract])
REFERENCES [dbo].[TypeOfContract] ([Identifier])
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_TypeOfContract]
GO
ALTER TABLE [dbo].[PackPaid]  WITH CHECK ADD  CONSTRAINT [FK_PackPaid_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Identifier])
GO
ALTER TABLE [dbo].[PackPaid] CHECK CONSTRAINT [FK_PackPaid_Client]
GO
ALTER TABLE [dbo].[PackPaid]  WITH CHECK ADD  CONSTRAINT [FK_PackPaid_Pack] FOREIGN KEY([IdPack])
REFERENCES [dbo].[Pack] ([Identifier])
GO
ALTER TABLE [dbo].[PackPaid] CHECK CONSTRAINT [FK_PackPaid_Pack]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Account1] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Identifier])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Account1]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Offer] FOREIGN KEY([IdOffer])
REFERENCES [dbo].[Offer] ([Identifier])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Offer]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_WebUser] FOREIGN KEY([IdWebUser])
REFERENCES [dbo].[WebUser] ([Identifier])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_WebUser]
GO
ALTER TABLE [dbo].[WebUser]  WITH CHECK ADD  CONSTRAINT [FK_WebUser_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Identifier])
GO
ALTER TABLE [dbo].[WebUser] CHECK CONSTRAINT [FK_WebUser_Account]
GO
USE [master]
GO
ALTER DATABASE [MegaCasting] SET  READ_WRITE 
GO
