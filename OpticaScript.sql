USE [master]
GO
/****** Object:  Database [BD_Optica]    Script Date: 12/1/2024 8:01:13 PM ******/
CREATE DATABASE [BD_Optica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_Optica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_Optica.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BD_Optica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_Optica_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BD_Optica] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_Optica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_Optica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_Optica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_Optica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_Optica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_Optica] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_Optica] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_Optica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_Optica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_Optica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_Optica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_Optica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_Optica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_Optica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_Optica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_Optica] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BD_Optica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_Optica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_Optica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_Optica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_Optica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_Optica] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_Optica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_Optica] SET RECOVERY FULL 
GO
ALTER DATABASE [BD_Optica] SET  MULTI_USER 
GO
ALTER DATABASE [BD_Optica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_Optica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_Optica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_Optica] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BD_Optica] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BD_Optica] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BD_Optica', N'ON'
GO
ALTER DATABASE [BD_Optica] SET QUERY_STORE = ON
GO
ALTER DATABASE [BD_Optica] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BD_Optica]
GO
/****** Object:  Table [dbo].[Diopters]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diopters](
	[DiopterId] [int] IDENTITY(1,1) NOT NULL,
	[DiopterValue] [float] NOT NULL,
 CONSTRAINT [PK_Diopters] PRIMARY KEY CLUSTERED 
(
	[DiopterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diseases]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diseases](
	[DiseaseId] [int] IDENTITY(1,1) NOT NULL,
	[Disease] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Diseases] PRIMARY KEY CLUSTERED 
(
	[DiseaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlassesOrders]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlassesOrders](
	[GlassOrderId] [int] IDENTITY(1,1) NOT NULL,
	[IdPatient] [int] NOT NULL,
	[FrameBrandAndColor] [varchar](50) NOT NULL,
	[OrderCost] [float] NOT NULL,
	[OrderDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_GlassesOrders] PRIMARY KEY CLUSTERED 
(
	[GlassOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HardLensesOrders]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HardLensesOrders](
	[HardLensOrderId] [int] IDENTITY(1,1) NOT NULL,
	[IdPatient] [int] NOT NULL,
	[YKeratometry] [int] NOT NULL,
	[XKeratometry] [int] NOT NULL,
	[Radius] [float] NOT NULL,
	[Diameter] [float] NOT NULL,
	[OrderCost] [float] NOT NULL,
	[OrderDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_HardLensesOrders] PRIMARY KEY CLUSTERED 
(
	[HardLensOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeftEyesRx]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeftEyesRx](
	[LeftEyeRxId] [int] IDENTITY(1,1) NOT NULL,
	[IdSphereLeft] [int] NOT NULL,
	[IdCylinderLeft] [int] NOT NULL,
	[AxisLeft] [int] NOT NULL,
	[AdditionLeft] [int] NOT NULL,
	[IdPatientLeft] [int] NOT NULL,
 CONSTRAINT [PK_LeftEyesRx] PRIMARY KEY CLUSTERED 
(
	[LeftEyeRxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LensesLifetimeTypes]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LensesLifetimeTypes](
	[LensLifetimeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LifeTimeType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LensesLifetimeTypes] PRIMARY KEY CLUSTERED 
(
	[LensLifetimeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LensesUsageTypes]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LensesUsageTypes](
	[LensUsageTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LensUsageType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LensesUsageTypes] PRIMARY KEY CLUSTERED 
(
	[LensUsageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientDiseases]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDiseases](
	[PatientDiseaseId] [int] IDENTITY(1,1) NOT NULL,
	[IdPatient] [int] NOT NULL,
	[IdDisease] [int] NOT NULL,
 CONSTRAINT [PK_PatientDiseases] PRIMARY KEY CLUSTERED 
(
	[PatientDiseaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[IdGender] [int] NOT NULL,
	[AnotherDiseases] [varchar](100) NOT NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[Due] [float] NOT NULL,
	[RegistryDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RightEyesRx]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RightEyesRx](
	[RightEyeRxId] [int] IDENTITY(1,1) NOT NULL,
	[IdSphereRight] [int] NOT NULL,
	[IdCylinderRight] [int] NOT NULL,
	[AxisRight] [int] NOT NULL,
	[AdditionRight] [int] NOT NULL,
	[IdPatientRight] [int] NOT NULL,
 CONSTRAINT [PK_RightEyesRx] PRIMARY KEY CLUSTERED 
(
	[RightEyeRxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoftLensesOrders]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoftLensesOrders](
	[SoftLensOrderId] [int] IDENTITY(1,1) NOT NULL,
	[IdPatient] [int] NOT NULL,
	[BrandName] [varchar](50) NOT NULL,
	[Life] [int] NOT NULL,
	[Usage] [int] NOT NULL,
	[OrderCost] [float] NOT NULL,
	[OrderDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_SoftLensesOrders] PRIMARY KEY CLUSTERED 
(
	[SoftLensOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Diopters] ON 

INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (1, -30)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (2, -29.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (3, -29)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (4, -28.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (5, -28)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (6, -27.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (7, -27)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (8, -26.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (9, -26)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (10, -25.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (11, -25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (12, -24.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (13, -24)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (14, -23.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (15, -23)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (16, -22.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (17, -22)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (18, -21.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (19, -21)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (20, -20.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (21, -20)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (22, -19.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (23, -19)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (24, -18.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (25, -18)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (26, -17.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (27, -17)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (28, -16.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (29, -16)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (30, -15.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (31, -15)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (32, -14.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (33, -14)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (34, -13.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (35, -13)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (36, -12.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (37, -12)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (38, -11.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (39, -11)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (40, -10.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (41, -10)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (42, -9.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (43, -9.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (44, -9.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (45, -9)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (46, -8.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (47, -8.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (48, -8.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (49, -8)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (50, -7.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (51, -7.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (52, -7.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (53, -7)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (54, -6.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (55, -6.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (56, -6.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (57, -6)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (58, -5.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (59, -5.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (60, -5.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (61, -5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (62, -4.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (63, -4.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (64, -4.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (65, -4)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (66, -3.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (67, -3.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (68, -3.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (69, -3)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (70, -2.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (71, -2.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (72, -2.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (73, -2)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (74, -1.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (75, -1.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (76, -1.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (77, -1)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (78, -0.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (79, -0.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (80, -0.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (81, 0)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (82, 0.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (83, 0.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (84, 0.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (85, 1)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (86, 1.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (87, 1.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (88, 1.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (89, 2)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (90, 2.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (91, 2.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (92, 2.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (93, 3)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (94, 3.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (95, 3.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (96, 3.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (97, 4)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (98, 4.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (99, 4.5)
GO
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (100, 4.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (101, 5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (102, 5.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (103, 5.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (104, 5.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (105, 6)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (106, 6.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (107, 6.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (108, 6.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (109, 7)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (110, 7.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (111, 7.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (112, 7.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (113, 8)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (114, 8.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (115, 8.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (116, 8.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (117, 9)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (118, 9.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (119, 9.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (120, 9.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (121, 10)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (122, 10.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (123, 11)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (124, 11.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (125, 12)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (126, 12.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (127, 13)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (128, 13.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (129, 14)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (130, 14.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (131, 15)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (132, 15.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (133, 16)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (134, 16.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (135, 17)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (136, 17.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (137, 18)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (138, 18.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (139, 19)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (140, 19.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (141, 20)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (142, 20.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (143, 21)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (144, 21.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (145, 22)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (146, 22.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (147, 23)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (148, 23.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (149, 24)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (150, 24.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (151, 25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (152, 25.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (153, 26)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (154, 26.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (155, 27)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (156, 27.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (157, 28)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (158, 28.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (159, 29)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (160, 29.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (161, 30)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (162, 31)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (163, 32)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (164, 33)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (165, 34)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (166, 35)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (167, 36)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (168, 37)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (169, 37.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (170, 37.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (171, 37.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (172, 38)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (173, 38.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (174, 38.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (175, 38.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (176, 39)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (177, 39.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (178, 39.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (179, 39.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (180, 40)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (181, 40.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (182, 40.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (183, 40.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (184, 41)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (185, 41.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (186, 41.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (187, 41.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (188, 42)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (189, 42.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (190, 42.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (191, 42.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (192, 43)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (193, 43.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (194, 43.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (195, 43.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (196, 44)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (197, 44.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (198, 44.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (199, 44.75)
GO
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (200, 45)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (201, 45.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (202, 45.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (203, 45.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (204, 46)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (205, 46.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (206, 46.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (207, 46.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (208, 47)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (209, 47.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (210, 47.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (211, 47.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (212, 48)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (213, 48.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (214, 48.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (215, 48.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (216, 49)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (217, 49.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (218, 49.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (219, 49.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (220, 50)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (221, 50.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (222, 50.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (223, 50.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (224, 51)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (225, 51.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (226, 51.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (227, 51.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (228, 52)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (229, 52.25)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (230, 52.5)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (231, 52.75)
INSERT [dbo].[Diopters] ([DiopterId], [DiopterValue]) VALUES (232, 53)
SET IDENTITY_INSERT [dbo].[Diopters] OFF
GO
SET IDENTITY_INSERT [dbo].[Diseases] ON 

INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (1, N'Diabetes')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (2, N'Hipertensión')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (3, N'Cataratas')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (4, N'Glaucoma')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (5, N'Queratocono')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (6, N'Agujero Macular')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (7, N'Degeneraciones Maculares')
INSERT [dbo].[Diseases] ([DiseaseId], [Disease]) VALUES (8, N'Desprendimiento de Retina')
SET IDENTITY_INSERT [dbo].[Diseases] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

INSERT [dbo].[Genders] ([GenderId], [Gender]) VALUES (1, N'Hombre')
INSERT [dbo].[Genders] ([GenderId], [Gender]) VALUES (2, N'Mujer')
INSERT [dbo].[Genders] ([GenderId], [Gender]) VALUES (3, N'Sin especificar')
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
SET IDENTITY_INSERT [dbo].[LeftEyesRx] ON 

INSERT [dbo].[LeftEyesRx] ([LeftEyeRxId], [IdSphereLeft], [IdCylinderLeft], [AxisLeft], [AdditionLeft], [IdPatientLeft]) VALUES (1, 82, 84, 0, 81, 1)
INSERT [dbo].[LeftEyesRx] ([LeftEyeRxId], [IdSphereLeft], [IdCylinderLeft], [AxisLeft], [AdditionLeft], [IdPatientLeft]) VALUES (3, 81, 80, 30, 1, 2)
INSERT [dbo].[LeftEyesRx] ([LeftEyeRxId], [IdSphereLeft], [IdCylinderLeft], [AxisLeft], [AdditionLeft], [IdPatientLeft]) VALUES (6, 79, 78, 15, 1, 3)
INSERT [dbo].[LeftEyesRx] ([LeftEyeRxId], [IdSphereLeft], [IdCylinderLeft], [AxisLeft], [AdditionLeft], [IdPatientLeft]) VALUES (7, 82, 83, 0, 1, 4)
SET IDENTITY_INSERT [dbo].[LeftEyesRx] OFF
GO
SET IDENTITY_INSERT [dbo].[LensesLifetimeTypes] ON 

INSERT [dbo].[LensesLifetimeTypes] ([LensLifetimeTypeId], [LifeTimeType]) VALUES (1, N'Anual')
INSERT [dbo].[LensesLifetimeTypes] ([LensLifetimeTypeId], [LifeTimeType]) VALUES (2, N'Bimestral')
INSERT [dbo].[LensesLifetimeTypes] ([LensLifetimeTypeId], [LifeTimeType]) VALUES (3, N'Un Día')
SET IDENTITY_INSERT [dbo].[LensesLifetimeTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[LensesUsageTypes] ON 

INSERT [dbo].[LensesUsageTypes] ([LensUsageTypeId], [LensUsageType]) VALUES (1, N'Diurno')
INSERT [dbo].[LensesUsageTypes] ([LensUsageTypeId], [LensUsageType]) VALUES (2, N'Nocturno')
SET IDENTITY_INSERT [dbo].[LensesUsageTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[PatientDiseases] ON 

INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (1, 1, 2)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (2, 1, 1)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (3, 1, 3)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (4, 1, 4)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (5, 1, 5)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (7, 1, 3)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (8, 6, 3)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (9, 7, 5)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (10, 9, 5)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (16, 15, 2)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (17, 15, 3)
INSERT [dbo].[PatientDiseases] ([PatientDiseaseId], [IdPatient], [IdDisease]) VALUES (18, 15, 4)
SET IDENTITY_INSERT [dbo].[PatientDiseases] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (1, N'Sinuhe', N'Alejandro', N'Gómez Hernández', 26, 1, N'N/A', N'5624859827', 4000, CAST(N'2024-10-06T16:39:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (2, N'Carlos', N'Alexis', N'Martinez Rodriguez', 25, 1, N'Ta renalgon', N'S/N', 0, CAST(N'2024-10-15T13:13:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (3, N'Monserrat', N'', N'García Velasco', 26, 2, N'', N'S/N', 0, CAST(N'2024-10-15T18:51:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (4, N'Rodrigo', N'Rafael', N'Gomez', 51, 1, N'', N'S/N', 0, CAST(N'2024-10-17T18:31:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (5, N'Rodrigo', N'Rafael', N'Gómez', 52, 1, N'Problemas renales', N'1231231231', 0, CAST(N'2024-10-19T20:25:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (6, N'Rodrigo', N'', N'Gómez', 12, 1, N'123', N'1231231232', 0, CAST(N'2024-10-20T16:49:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (7, N'juan', N'', N'garces', 35, 1, N'', N'5578932765', 0, CAST(N'2024-10-20T16:55:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (8, N'Hector', N'', N'De León', 40, 1, N'Le sabe a los factos', N'5515580909', 0, CAST(N'2024-11-01T14:13:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (9, N'Francisco', N'Guillermo', N'Salvador Montiel', 24, 1, N'', N'S/N', 0, CAST(N'2024-11-02T18:00:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (15, N'Woshingo', N'', N'Chespirito', 60, 1, N'', N'S/N', 0, CAST(N'2024-11-05T22:18:00' AS SmallDateTime))
INSERT [dbo].[Patients] ([PatientId], [FirstName], [MiddleName], [LastName], [Age], [IdGender], [AnotherDiseases], [ContactNumber], [Due], [RegistryDate]) VALUES (16, N'Alejandra', N'', N'Mendoza Lopez', 25, 2, N'Problemas renales', N'5512312312', 0, CAST(N'2024-11-15T12:52:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[RightEyesRx] ON 

INSERT [dbo].[RightEyesRx] ([RightEyeRxId], [IdSphereRight], [IdCylinderRight], [AxisRight], [AdditionRight], [IdPatientRight]) VALUES (1, 81, 83, 0, 85, 1)
INSERT [dbo].[RightEyesRx] ([RightEyeRxId], [IdSphereRight], [IdCylinderRight], [AxisRight], [AdditionRight], [IdPatientRight]) VALUES (2, 81, 80, 30, 1, 2)
INSERT [dbo].[RightEyesRx] ([RightEyeRxId], [IdSphereRight], [IdCylinderRight], [AxisRight], [AdditionRight], [IdPatientRight]) VALUES (3, 82, 82, 23, 2, 3)
INSERT [dbo].[RightEyesRx] ([RightEyeRxId], [IdSphereRight], [IdCylinderRight], [AxisRight], [AdditionRight], [IdPatientRight]) VALUES (4, 78, 79, 0, 1, 4)
SET IDENTITY_INSERT [dbo].[RightEyesRx] OFF
GO
SET IDENTITY_INSERT [dbo].[SoftLensesOrders] ON 

INSERT [dbo].[SoftLensesOrders] ([SoftLensOrderId], [IdPatient], [BrandName], [Life], [Usage], [OrderCost], [OrderDate]) VALUES (1, 1, N'Marca A', 1, 1, 2000, CAST(N'2024-10-08T13:59:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[SoftLensesOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FullName], [Username], [Password], [IsAdmin]) VALUES (1, N'Sinuhe Alejandro Gomez Hernández', N'Alexandro_Morningstar', N'123', 1)
INSERT [dbo].[Users] ([UserId], [FullName], [Username], [Password], [IsAdmin]) VALUES (2, N'Aily Jazmin Gomez Hernandez', N'Aily', N'123', 0)
INSERT [dbo].[Users] ([UserId], [FullName], [Username], [Password], [IsAdmin]) VALUES (3, N'Hugo Gomez Castellanos', N'Hugo', N'123', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[GlassesOrders]  WITH CHECK ADD  CONSTRAINT [FK_GlassesOrders_Patients] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[GlassesOrders] CHECK CONSTRAINT [FK_GlassesOrders_Patients]
GO
ALTER TABLE [dbo].[HardLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_HardLensesOrders_Diopters] FOREIGN KEY([YKeratometry])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[HardLensesOrders] CHECK CONSTRAINT [FK_HardLensesOrders_Diopters]
GO
ALTER TABLE [dbo].[HardLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_HardLensesOrders_Diopters1] FOREIGN KEY([XKeratometry])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[HardLensesOrders] CHECK CONSTRAINT [FK_HardLensesOrders_Diopters1]
GO
ALTER TABLE [dbo].[HardLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_HardLensesOrders_Patients] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[HardLensesOrders] CHECK CONSTRAINT [FK_HardLensesOrders_Patients]
GO
ALTER TABLE [dbo].[LeftEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_LeftEyesRx_Diopters1] FOREIGN KEY([IdSphereLeft])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[LeftEyesRx] CHECK CONSTRAINT [FK_LeftEyesRx_Diopters1]
GO
ALTER TABLE [dbo].[LeftEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_LeftEyesRx_Diopters2] FOREIGN KEY([IdCylinderLeft])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[LeftEyesRx] CHECK CONSTRAINT [FK_LeftEyesRx_Diopters2]
GO
ALTER TABLE [dbo].[LeftEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_LeftEyesRx_Patients] FOREIGN KEY([IdPatientLeft])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[LeftEyesRx] CHECK CONSTRAINT [FK_LeftEyesRx_Patients]
GO
ALTER TABLE [dbo].[PatientDiseases]  WITH CHECK ADD  CONSTRAINT [FK_PatientDiseases_Diseases] FOREIGN KEY([IdDisease])
REFERENCES [dbo].[Diseases] ([DiseaseId])
GO
ALTER TABLE [dbo].[PatientDiseases] CHECK CONSTRAINT [FK_PatientDiseases_Diseases]
GO
ALTER TABLE [dbo].[PatientDiseases]  WITH CHECK ADD  CONSTRAINT [FK_PatientDiseases_Patients] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientDiseases] CHECK CONSTRAINT [FK_PatientDiseases_Patients]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Genders] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Genders] ([GenderId])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Genders]
GO
ALTER TABLE [dbo].[RightEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_RightEyesRx_Diopters] FOREIGN KEY([IdSphereRight])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[RightEyesRx] CHECK CONSTRAINT [FK_RightEyesRx_Diopters]
GO
ALTER TABLE [dbo].[RightEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_RightEyesRx_Diopters1] FOREIGN KEY([IdCylinderRight])
REFERENCES [dbo].[Diopters] ([DiopterId])
GO
ALTER TABLE [dbo].[RightEyesRx] CHECK CONSTRAINT [FK_RightEyesRx_Diopters1]
GO
ALTER TABLE [dbo].[RightEyesRx]  WITH CHECK ADD  CONSTRAINT [FK_RightEyesRx_Patients] FOREIGN KEY([IdPatientRight])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[RightEyesRx] CHECK CONSTRAINT [FK_RightEyesRx_Patients]
GO
ALTER TABLE [dbo].[SoftLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_SoftLensesOrders_LensesLifetimeTypes] FOREIGN KEY([Life])
REFERENCES [dbo].[LensesLifetimeTypes] ([LensLifetimeTypeId])
GO
ALTER TABLE [dbo].[SoftLensesOrders] CHECK CONSTRAINT [FK_SoftLensesOrders_LensesLifetimeTypes]
GO
ALTER TABLE [dbo].[SoftLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_SoftLensesOrders_LensesUsageTypes] FOREIGN KEY([Usage])
REFERENCES [dbo].[LensesUsageTypes] ([LensUsageTypeId])
GO
ALTER TABLE [dbo].[SoftLensesOrders] CHECK CONSTRAINT [FK_SoftLensesOrders_LensesUsageTypes]
GO
ALTER TABLE [dbo].[SoftLensesOrders]  WITH CHECK ADD  CONSTRAINT [FK_SoftLensesOrders_Patients] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[SoftLensesOrders] CHECK CONSTRAINT [FK_SoftLensesOrders_Patients]
GO
/****** Object:  StoredProcedure [dbo].[Add_LeftEye]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Add_LeftEye]
	@idsphereleft int,
	@idcylinderleft int,
	@axisleft int,
	@additionleft int,
	@idpatientleft int
AS
BEGIN
	INSERT INTO
		dbo.LeftEyesRx(IdSphereLeft, IdCylinderLeft, AxisLeft, AdditionLeft, IdPatientLeft)
	VALUES
		(@idsphereleft, @idcylinderleft, @axisleft, @additionleft, @idpatientleft)
END;
GO
/****** Object:  StoredProcedure [dbo].[Add_RightEye]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Add_RightEye]
	@idsphereright int,
	@idcylinderright int,
	@axisright int,
	@additionright int,
	@idpatientright int
AS
BEGIN
	INSERT INTO
		dbo.RightEyesRx(IdSphereRight, IdCylinderRight, AxisRight, AdditionRight, IdPatientRight)
	VALUES
		(@idsphereright, @idcylinderright, @axisright, @additionright, @idpatientright)
END;
GO
/****** Object:  StoredProcedure [dbo].[Create_Patient]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Create_Patient]
	@firstname varchar(50),
	@middlename varchar(50), --*
	@lastname varchar(50),
	@age int,
	@idgender int, 
	@anotherdiseases varchar(100), --*
	@contactnumber varchar(50), --*
	@due float, -- Siempre se inicializa en 0 desde la API
	@registrydate smalldatetime
AS BEGIN
	INSERT INTO
		dbo.Patients
	VALUES
		(@firstname,@middlename,@lastname,@age,@idgender,@anotherdiseases,@contactnumber,@due,@registrydate)
END;
GO
/****** Object:  StoredProcedure [dbo].[Create_Patient_Disease]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Create_Patient_Disease]
	@idpatient int,
	@iddisease int
AS BEGIN
	INSERT INTO
		dbo.PatientDiseases
	VALUES
		(@idpatient, @iddisease)
END;
GO
/****** Object:  StoredProcedure [dbo].[Create_Patient_With_Diseases]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Create_Patient_With_Diseases]
	@firstname varchar(50),
	@middlename varchar(50), --*
	@lastname varchar(50),
	@age int,
	@idgender int, 
	@anotherdiseases varchar(100), --*
	@contactnumber varchar(50), --*
	@due float, -- Siempre se inicializa en 0 desde la API
	@registrydate smalldatetime,
	@sqlCurrentId INT OUTPUT --Parámetro de salida para devolver el nuevo ID
AS BEGIN
	INSERT INTO
		dbo.Patients
	VALUES
		(@firstname,@middlename,@lastname,@age,@idgender,@anotherdiseases,@contactnumber,@due,@registrydate)
	SET
		@sqlCurrentId = SCOPE_IDENTITY()
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Diseases]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_All_Diseases]
AS BEGIN
	SELECT
		DiseaseId, Disease
	FROM
		dbo.Diseases
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Patients]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_All_Patients]
AS BEGIN
	SELECT
		PatientId, FirstName, MiddleName, LastName, Age, IdGender, AnotherDiseases, ContactNumber, Due, RegistryDate, GenderId, Gender
	FROM
		dbo.Patients
	INNER JOIN
		dbo.Genders
	ON
		dbo.Patients.IdGender = dbo.Genders.GenderId
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Diopters_ToMR]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Diopters_ToMR]
AS
BEGIN
	SELECT
		*
	FROM
		dbo.Diopters
	WHERE
		DiopterValue
	BETWEEN
		-30 AND 30
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Existing_User]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Existing_User]
	@username varchar(50),
	@password varchar(50)
AS BEGIN
	SELECT
		*
	FROM
		dbo.Users
	WHERE
		Username=@username AND Password=@password
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Ids_With_Orders]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Ids_With_Orders]
	@id INT
AS
BEGIN
	IF
		EXISTS (SELECT 1 FROM dbo.GlassesOrders WHERE IdPatient=@id)
		OR EXISTS (SELECT 1 FROM dbo.SoftLensesOrders WHERE IdPatient=@id)
		OR EXISTS (SELECT 1 FROM dbo.HardLensesOrders WHERE IdPatient=@id)
		BEGIN
			SELECT 'Existe al menos un pedido' AS Resultado
		END
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Ids_WithMR]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Ids_WithMR]
	@id INT
AS
BEGIN
	IF
		EXISTS (SELECT 1 FROM dbo.LeftEyesRx WHERE IdPatientLeft=@id)
		AND EXISTS (SELECT 1 FROM dbo.RightEyesRx WHERE IdPatientRight=@id)
		BEGIN
			SELECT 'Existe historia clinica' AS Resultado
		END
	--ELSE
	--	BEGIN
	--		SELECT 'No existe historia clinica' AS Resultado
	--	END
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Last_Patient_Id]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Last_Patient_Id]
AS BEGIN
	SELECT 
		IDENT_CURRENT('dbo.Patients')
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Patient_Details]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Patient_Details]
	@patientid int
AS
BEGIN
	SELECT
		PatientId, FirstName, MiddleName, LastName, Age, Gender, AnotherDiseases, ContactNumber, Due, RegistryDate, IdGender
	FROM
		dbo.Patients
	INNER JOIN
		dbo.Genders
	ON
		dbo.Patients.IdGender = dbo.Genders.GenderId
	WHERE
		dbo.Patients.PatientId = @patientid
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Patient_Diseases]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Patient_Diseases]
	@idpatient INT
AS
BEGIN
	SELECT
		DiseaseId, Disease
	FROM
		dbo.PatientDiseases
	INNER JOIN
		dbo.Diseases
	ON
		dbo.PatientDiseases.IdDisease = dbo.Diseases.DiseaseId
	WHERE
		dbo.PatientDiseases.IdPatient = @idpatient
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Patients_LeftEye]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Patients_LeftEye]
	@idpatientleft INT
AS
BEGIN
	SELECT
		LeftEyeRxId,
		IdPatientLeft,
		AxisLeft,
		EsferaIzquierda.DiopterValue AS SphereLeft,
		CilindroIzquierdo.DiopterValue AS CylinderLeft,
		AdicionIzquierda.DiopterValue AS AdditionLeft
	FROM
		dbo.LeftEyesRx
	INNER JOIN
		dbo.Diopters AS EsferaIzquierda ON dbo.LeftEyesRx.IdSphereLeft = EsferaIzquierda.DiopterId
	INNER JOIN
		dbo.Diopters AS CilindroIzquierdo ON dbo.LeftEyesRx.IdCylinderLeft = CilindroIzquierdo.DiopterId
	INNER JOIN
		dbo.Diopters AS AdicionIzquierda ON dbo.LeftEyesRx.AdditionLeft = AdicionIzquierda.DiopterId
	WHERE IdPatientLeft = @idpatientleft
END;
GO
/****** Object:  StoredProcedure [dbo].[Get_Patients_RightEye]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Get_Patients_RightEye]
	@idpatientright INT
AS
BEGIN
	SELECT
		RightEyeRxId,
		IdPatientRight,
		AxisRight,
		EsferaDerecha.DiopterValue AS SphereRight,
		CilindroDerecho.DiopterValue AS CylinderRight,
		AdicionDerecha.DiopterValue AS AdditionRight
	FROM
		dbo.RightEyesRx
	INNER JOIN
		dbo.Diopters AS EsferaDerecha ON dbo.RightEyesRx.IdSphereRight = EsferaDerecha.DiopterId
	INNER JOIN
		dbo.Diopters AS CilindroDerecho ON dbo.RightEyesRx.IdCylinderRight = CilindroDerecho.DiopterId
	INNER JOIN
		dbo.Diopters AS AdicionDerecha ON dbo.RightEyesRx.AdditionRight = AdicionDerecha.DiopterId
	WHERE IdPatientRight = @idpatientright
END;
GO
/****** Object:  StoredProcedure [dbo].[Patient_Edit]    Script Date: 12/1/2024 8:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Patient_Edit]
	@patientid int,
	@firstname VARCHAR(50),
	@middlename VARCHAR(50),
	@lastname VARCHAR(50),
	@age INT,
	@idgender INT,
	@anotherdiseases VARCHAR(100),
	@contactnumber VARCHAR(50)
AS
BEGIN
	UPDATE
		dbo.Patients
	SET
		FirstName = @firstname, MiddleName = @middlename, LastName = @lastname, Age = @age, IdGender = @idgender, AnotherDiseases = @anotherdiseases, ContactNumber = @contactnumber
	WHERE
		PatientId = @patientid
END;
GO
USE [master]
GO
ALTER DATABASE [BD_Optica] SET  READ_WRITE 
GO
