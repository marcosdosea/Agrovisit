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
-- Table structure for table `cultura`
--

DROP TABLE IF EXISTS `cultura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cultura` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cultura`
--

LOCK TABLES `cultura` WRITE;
/*!40000 ALTER TABLE `cultura` DISABLE KEYS */;
INSERT INTO `cultura` VALUES (1,'Abacaxi'),(2,'Abóbora'),(3,'Acerola'),(4,'Alface'),(5,'Algodão'),(6,'Alho'),(7,'Amendoim'),(8,'Arroz'),(9,'Banana'),(10,'Batata'),(11,'Batata-doce'),(12,'Beterraba'),(13,'Café'),(14,'Cajú'),(15,'Cana-de-açúcar'),(16,'Cebola'),(17,'Cenoura'),(18,'Chuchu'),(19,'Citros'),(20,'Coco'),(21,'Coentro'),(22,'Couve'),(23,'Dendê'),(24,'Ervilha'),(25,'Eucalipto'),(26,'Feijão'),(27,'Gengibre'),(28,'Girassol'),(29,'Goiaba'),(30,'Guaraná'),(31,'Inhame'),(32,'Laranja'),(33,'Limão'),(34,'Maça'),(35,'Mamão'),(36,'Mandioca'),(37,'Manga'),(38,'Maracujá'),(39,'Marxixe'),(40,'Melancia'),(41,'Melão'),(42,'Milho'),(43,'Palma'),(44,'Pastagens'),(45,'Pepino'),(46,'Pimenta'),(47,'Quiabo'),(48,'Repolho'),(49,'Soja'),(50,'Tabaco');
/*!40000 ALTER TABLE `cultura` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-20 15:41:56
