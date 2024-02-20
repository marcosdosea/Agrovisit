-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: agrovisit
-- ------------------------------------------------------
-- Server version	5.7.43-log

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
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  `estado` varchar(10) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `quantFuncionario` int(11) DEFAULT NULL,
  `areaReserva` float DEFAULT NULL,
  `areaPreservar` float DEFAULT NULL,
  `car` varchar(50) DEFAULT NULL,
  `ccir` varchar(50) DEFAULT NULL,
  `itr` varchar(50) DEFAULT NULL,
  `georreferenciamento` blob,
  `matriculaImovel` blob,
  `numAnimais` int(11) DEFAULT NULL,
  `raca` varchar(50) DEFAULT NULL,
  `fonteAlimento` varchar(50) DEFAULT NULL,
  `areaPasto` float DEFAULT NULL,
  `historicoProducao` blob,
  `areaTotal` float DEFAULT NULL,
  `areaCultivada` float DEFAULT NULL,
  `comercializacao` enum('C','A') DEFAULT NULL,
  `historicoProdAgricola` blob,
  `historicoFitossanidade` blob,
  `idSolo` int(10) unsigned NOT NULL,
  `idCultura` int(10) unsigned NOT NULL,
  `idCliente` int(10) unsigned NOT NULL,
  `idEngenheiroAgronomo` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fkPropriedadeSolo1_idx` (`idSolo`),
  KEY `fkPropriedadeCultura1_idx` (`idCultura`),
  KEY `fkPropriedadeCliente1_idx` (`idCliente`),
  KEY `fkPropriedadeUsuario1_idx` (`idEngenheiroAgronomo`),
  CONSTRAINT `fkPropriedadeCliente1` FOREIGN KEY (`idCliente`) REFERENCES `cliente` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeCultura1` FOREIGN KEY (`idCultura`) REFERENCES `cultura` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeSolo1` FOREIGN KEY (`idSolo`) REFERENCES `solo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fkPropriedadeUsuario1` FOREIGN KEY (`idEngenheiroAgronomo`) REFERENCES `engenheiroagronomo` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `propriedade`
--

LOCK TABLES `propriedade` WRITE;
/*!40000 ALTER TABLE `propriedade` DISABLE KEYS */;
INSERT INTO `propriedade` VALUES (1,'Fazenda Feliz','SE','Aracaju',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,2,1);
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

-- Dump completed on 2024-02-20 15:41:55
