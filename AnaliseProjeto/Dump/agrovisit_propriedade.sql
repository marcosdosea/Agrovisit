-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: agrovisit
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `propriedade`
--

DROP TABLE IF EXISTS `propriedade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `propriedade` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  `estado` varchar(10) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `quantFuncionario` int DEFAULT NULL,
  `areaReserva` float DEFAULT NULL,
  `areaPreservar` float DEFAULT NULL,
  `car` varchar(50) DEFAULT NULL,
  `ccir` varchar(50) DEFAULT NULL,
  `itr` varchar(50) DEFAULT NULL,
  `georreferenciamento` blob,
  `matriculaImovel` blob,
  `numAnimais` int DEFAULT NULL,
  `raca` varchar(50) DEFAULT NULL,
  `fonteAlimento` varchar(50) DEFAULT NULL,
  `areaPasto` float DEFAULT NULL,
  `historicoProducao` blob,
  `areaTotal` float DEFAULT NULL,
  `areaCultivada` float DEFAULT NULL,
  `tipoSolo` varchar(50) DEFAULT NULL,
  `cultura` varchar(50) DEFAULT NULL,
  `comercializacao` enum('C','A') DEFAULT NULL,
  `historicoProdAgricola` blob,
  `historicoFitossanidade` blob,
  `idSolo` int unsigned NOT NULL,
  `idCultura` int unsigned NOT NULL,
  `idCliente` int unsigned NOT NULL,
  `idEngenheiroAgronomo` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fkPropriedadeSolo1_idx` (`idSolo`),
  KEY `fkPropriedadeCultura1_idx` (`idCultura`),
  KEY `fkPropriedadeCliente1_idx` (`idCliente`),
  KEY `fkPropriedadeUsuario1_idx` (`idEngenheiroAgronomo`),
  CONSTRAINT `fkPropriedadeCliente1` FOREIGN KEY (`idCliente`) REFERENCES `cliente` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeCultura1` FOREIGN KEY (`idCultura`) REFERENCES `cultura` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeSolo1` FOREIGN KEY (`idSolo`) REFERENCES `solo` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeUsuario1` FOREIGN KEY (`idEngenheiroAgronomo`) REFERENCES `engenheiroagronomo` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `propriedade`
--

LOCK TABLES `propriedade` WRITE;
/*!40000 ALTER TABLE `propriedade` DISABLE KEYS */;
/*!40000 ALTER TABLE `propriedade` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-02 12:40:04
