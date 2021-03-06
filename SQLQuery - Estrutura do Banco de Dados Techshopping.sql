USE [master]
GO
/****** Object:  Database [techshopping]    Script Date: 04/12/2019 09:25:17 ******/
CREATE DATABASE [techshopping]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'techshopping', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\techshopping.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'techshopping_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\techshopping_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [techshopping] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [techshopping].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [techshopping] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [techshopping] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [techshopping] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [techshopping] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [techshopping] SET ARITHABORT OFF 
GO
ALTER DATABASE [techshopping] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [techshopping] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [techshopping] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [techshopping] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [techshopping] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [techshopping] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [techshopping] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [techshopping] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [techshopping] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [techshopping] SET  DISABLE_BROKER 
GO
ALTER DATABASE [techshopping] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [techshopping] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [techshopping] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [techshopping] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [techshopping] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [techshopping] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [techshopping] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [techshopping] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [techshopping] SET  MULTI_USER 
GO
ALTER DATABASE [techshopping] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [techshopping] SET DB_CHAINING OFF 
GO
ALTER DATABASE [techshopping] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [techshopping] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [techshopping] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [techshopping] SET QUERY_STORE = OFF
GO
USE [techshopping]
GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 04/12/2019 09:25:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estoque](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[quantidade] [int] NOT NULL,
	[codigoproduto] [int] NOT NULL,
	[data] [varchar](15) NOT NULL,
	[hora] [varchar](15) NOT NULL,
	[motivo] [varchar](max) NOT NULL,
	[acao] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Estoque] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 04/12/2019 09:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[codigobarras] [varchar](max) NOT NULL,
	[descricao] [varchar](max) NOT NULL,
	[unidademedida] [varchar](50) NOT NULL,
	[qtdminima] [int] NOT NULL,
	[qtdmaxima] [int] NOT NULL,
	[qtdatual] [int] NOT NULL,
	[custounitario] [decimal](18, 2) NOT NULL,
	[percentuallucro] [decimal](18, 2) NOT NULL,
	[precovenda] [decimal](18, 2) NOT NULL,
	[ativo] [varchar](3) NOT NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venda]    Script Date: 04/12/2019 09:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venda](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[data] [date] NOT NULL,
	[hora] [varchar](50) NOT NULL,
	[valortotal] [decimal](18, 2) NOT NULL,
	[desconto] [decimal](18, 2) NOT NULL,
	[ativo] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Venda] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendaItem]    Script Date: 04/12/2019 09:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendaItem](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[codigoproduto] [int] NOT NULL,
	[codigovenda] [int] NOT NULL,
	[quantidade] [int] NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VendaItem] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Produto] ADD  CONSTRAINT [DF_Produto_qtdatual]  DEFAULT ((0)) FOR [qtdatual]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_Estoque_Produto] FOREIGN KEY([codigoproduto])
REFERENCES [dbo].[Produto] ([codigo])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_Estoque_Produto]
GO
ALTER TABLE [dbo].[VendaItem]  WITH CHECK ADD  CONSTRAINT [FK_VendaItem_Produto] FOREIGN KEY([codigoproduto])
REFERENCES [dbo].[Produto] ([codigo])
GO
ALTER TABLE [dbo].[VendaItem] CHECK CONSTRAINT [FK_VendaItem_Produto]
GO
ALTER TABLE [dbo].[VendaItem]  WITH CHECK ADD  CONSTRAINT [FK_VendaItem_Venda] FOREIGN KEY([codigovenda])
REFERENCES [dbo].[Venda] ([codigo])
GO
ALTER TABLE [dbo].[VendaItem] CHECK CONSTRAINT [FK_VendaItem_Venda]
GO
USE [master]
GO
ALTER DATABASE [techshopping] SET  READ_WRITE 
GO
