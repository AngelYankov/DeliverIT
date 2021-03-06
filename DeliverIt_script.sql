USE [DeliverIt]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[StreetName] [nvarchar](30) NOT NULL,
	[CityID] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[FirstName] [nvarchar](15) NOT NULL,
	[LastName] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[FirstName] [nvarchar](15) NOT NULL,
	[LastName] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parcels]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parcels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ShipmentId] [int] NOT NULL,
	[Weight] [float] NOT NULL,
 CONSTRAINT [PK_Parcels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Departure] [datetime2](7) NOT NULL,
	[Arrival] [datetime2](7) NOT NULL,
	[StatusId] [int] NOT NULL,
	[WarehouseId] [int] NOT NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouses]    Script Date: 21.4.2021 г. 12:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Georgi Rakovski 1', 1)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tsar Osvoboditel 10', 2)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Mayfair Avenue 5', 3)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (4, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Wayton Road 3', 4)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (5, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Central Park 23', 5)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (6, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Central Ave 34', 6)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (7, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Dondukov blvd 23', 1)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (8, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Picadilly Circus 13', 2)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (9, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Sickla Kanalgata 3', 3)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (10, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Industrialna zona 2', 1)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (11, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Industrial Park 53', 2)
INSERT [dbo].[Addresses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [StreetName], [CityID]) VALUES (12, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Vasatan 7', 3)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Electronics')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Clothing')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Medical')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (1, N'Sofia', 1)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (2, N'Plovdiv', 1)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (3, N'London', 2)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (4, N'Birmingham', 2)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (5, N'Stockholm', 3)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (6, N'Malmo', 3)
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Bulgaria')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (2, N'United Kingdom')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (3, N'Sweden')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Stefan', N'Popov', N'stefan.popov@gmail.com', 1)
INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Georgi', N'Ivanov', N'georgi.ivanov@gmail.com', 2)
INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Peter', N'Crouch', N'peter.crouch@gmail.com', 3)
INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (4, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Steven', N'Tyler', N'steven.tyler@gmail.com', 4)
INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (5, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Sven', N'Jorgensson', N'sven.jorgensson@gmail.com', 5)
INSERT [dbo].[Customers] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (6, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Ragnar', N'Lothbrok', N'ragnar.lothbrok@gmail.com', 6)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Petar', N'Shapkov', N'peter.shapkov@gmail.com', 7)
INSERT [dbo].[Employees] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tyler', N'Johnson', N'tyler.johnson@gmail.com', 8)
INSERT [dbo].[Employees] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [FirstName], [LastName], [Email], [AddressId]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Eric', N'Berg', N'eric.berg@gmail.com', 9)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Parcels] ON 

INSERT [dbo].[Parcels] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [CustomerId], [WarehouseId], [CategoryId], [ShipmentId], [Weight]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 1, 1, 1, 1, 2.5)
INSERT [dbo].[Parcels] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [CustomerId], [WarehouseId], [CategoryId], [ShipmentId], [Weight]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 2, 2, 2, 2, 22.5)
INSERT [dbo].[Parcels] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [CustomerId], [WarehouseId], [CategoryId], [ShipmentId], [Weight]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 3, 3, 3, 3, 1)
SET IDENTITY_INSERT [dbo].[Parcels] OFF
GO
SET IDENTITY_INSERT [dbo].[Shipments] ON 

INSERT [dbo].[Shipments] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Departure], [Arrival], [StatusId], [WarehouseId]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2021-04-28T08:48:03.0663738' AS DateTime2), CAST(N'2021-05-03T08:48:03.0664319' AS DateTime2), 1, 1)
INSERT [dbo].[Shipments] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Departure], [Arrival], [StatusId], [WarehouseId]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2021-04-27T08:48:03.0665152' AS DateTime2), CAST(N'2021-05-01T08:48:03.0665166' AS DateTime2), 1, 2)
INSERT [dbo].[Shipments] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Departure], [Arrival], [StatusId], [WarehouseId]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2021-05-01T08:48:03.0665175' AS DateTime2), CAST(N'2021-05-05T08:48:03.0665176' AS DateTime2), 1, 3)
SET IDENTITY_INSERT [dbo].[Shipments] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (1, N'Preparing')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (2, N'On the way')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (3, N'Completed')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Warehouses] ON 

INSERT [dbo].[Warehouses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [AddressId]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 10)
INSERT [dbo].[Warehouses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [AddressId]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 11)
INSERT [dbo].[Warehouses] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [AddressId]) VALUES (3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 12)
SET IDENTITY_INSERT [dbo].[Warehouses] OFF
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Cities_CityID] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Cities_CityID]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries_CountryId]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Addresses_AddressId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Addresses_AddressId]
GO
ALTER TABLE [dbo].[Parcels]  WITH CHECK ADD  CONSTRAINT [FK_Parcels_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Parcels] CHECK CONSTRAINT [FK_Parcels_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Parcels]  WITH CHECK ADD  CONSTRAINT [FK_Parcels_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Parcels] CHECK CONSTRAINT [FK_Parcels_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Parcels]  WITH CHECK ADD  CONSTRAINT [FK_Parcels_Shipments_ShipmentId] FOREIGN KEY([ShipmentId])
REFERENCES [dbo].[Shipments] ([Id])
GO
ALTER TABLE [dbo].[Parcels] CHECK CONSTRAINT [FK_Parcels_Shipments_ShipmentId]
GO
ALTER TABLE [dbo].[Parcels]  WITH CHECK ADD  CONSTRAINT [FK_Parcels_Warehouses_WarehouseId] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouses] ([Id])
GO
ALTER TABLE [dbo].[Parcels] CHECK CONSTRAINT [FK_Parcels_Warehouses_WarehouseId]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Statuses_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [FK_Shipments_Statuses_StatusId]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Warehouses_WarehouseId] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [FK_Shipments_Warehouses_WarehouseId]
GO
ALTER TABLE [dbo].[Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Warehouses_Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Warehouses] CHECK CONSTRAINT [FK_Warehouses_Addresses_AddressId]
GO
