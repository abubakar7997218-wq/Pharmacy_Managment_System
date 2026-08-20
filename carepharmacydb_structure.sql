-- Care Pharmacy Database - Structure Only
-- Combined from MySQL Workbench structure-only exports.
-- No table data/INSERT statements are included.


-- =====================================================
-- carepharmacydb_roles.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `RoleID` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) NOT NULL,
  PRIMARY KEY (`RoleID`),
  UNIQUE KEY `RoleName` (`RoleName`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:10


-- =====================================================
-- carepharmacydb_users.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(100) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `CNIC` varchar(15) NOT NULL,
  `RoleID` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `CreatedAt` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Username` (`Username`),
  UNIQUE KEY `CNIC` (`CNIC`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:10


-- =====================================================
-- carepharmacydb_medicinecategories.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `medicinecategories`
--

DROP TABLE IF EXISTS `medicinecategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicinecategories` (
  `CategoryID` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(100) NOT NULL,
  PRIMARY KEY (`CategoryID`),
  UNIQUE KEY `CategoryName` (`CategoryName`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:09


-- =====================================================
-- carepharmacydb_medicines.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `medicines`
--

DROP TABLE IF EXISTS `medicines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicines` (
  `MedicineID` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryID` int(11) NOT NULL,
  `SupplierID` int(11) DEFAULT NULL,
  `MedicineName` varchar(150) NOT NULL,
  `GenericName` varchar(150) DEFAULT NULL,
  `Brand` varchar(100) DEFAULT NULL,
  `BatchNumber` varchar(50) DEFAULT NULL,
  `UnitPrice` decimal(10,2) NOT NULL CHECK (`UnitPrice` >= 0),
  `StockQty` int(11) NOT NULL DEFAULT 0 CHECK (`StockQty` >= 0),
  `MinStockLevel` int(11) NOT NULL DEFAULT 10,
  `ExpiryDate` date DEFAULT NULL,
  `RequiresPrescription` tinyint(1) NOT NULL DEFAULT 0,
  `IsAvailable` tinyint(1) NOT NULL DEFAULT 1,
  `AddedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` datetime DEFAULT NULL,
  `PurchasePrice` decimal(10,2) DEFAULT NULL,
  `SellingPrice` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`MedicineID`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:10


-- =====================================================
-- carepharmacydb_suppliers.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `SupplierID` int(11) NOT NULL AUTO_INCREMENT,
  `SupplierName` varchar(100) NOT NULL,
  `ContactName` varchar(100) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  `CreatedAt` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:10


-- =====================================================
-- carepharmacydb_stockdeliverymaster.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `stockdeliverymaster`
--

DROP TABLE IF EXISTS `stockdeliverymaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stockdeliverymaster` (
  `DeliveryID` int(11) NOT NULL AUTO_INCREMENT,
  `SupplierID` int(11) NOT NULL,
  `InvoiceNo` varchar(50) DEFAULT NULL,
  `DeliveryDate` date DEFAULT NULL,
  `ReceivedBy` int(11) DEFAULT NULL,
  `Notes` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`DeliveryID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:10


-- =====================================================
-- carepharmacydb_stockdeliverydetails.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `stockdeliverydetails`
--

DROP TABLE IF EXISTS `stockdeliverydetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stockdeliverydetails` (
  `DetailID` int(11) NOT NULL AUTO_INCREMENT,
  `DeliveryID` int(11) NOT NULL,
  `MedicineID` int(11) NOT NULL,
  `BatchNumber` varchar(50) DEFAULT NULL,
  `ExpiryDate` date DEFAULT NULL,
  `QtyReceived` int(11) DEFAULT NULL,
  `UnitCost` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`DetailID`),
  KEY `DeliveryID` (`DeliveryID`),
  CONSTRAINT `stockdeliverydetails_ibfk_1` FOREIGN KEY (`DeliveryID`) REFERENCES `stockdeliverymaster` (`DeliveryID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:11


-- =====================================================
-- carepharmacydb_prescriptions.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `prescriptions`
--

DROP TABLE IF EXISTS `prescriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescriptions` (
  `PrescriptionID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerName` varchar(100) NOT NULL,
  `CustomerPhone` varchar(20) DEFAULT NULL,
  `DoctorName` varchar(100) NOT NULL,
  `PrescriptionDate` date NOT NULL,
  `RecordedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT current_timestamp(),
  `Notes` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`PrescriptionID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:09


-- =====================================================
-- carepharmacydb_prescriptionmedicines.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `prescriptionmedicines`
--

DROP TABLE IF EXISTS `prescriptionmedicines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescriptionmedicines` (
  `PrescMedID` int(11) NOT NULL AUTO_INCREMENT,
  `PrescriptionID` int(11) NOT NULL,
  `MedicineID` int(11) DEFAULT NULL,
  `MedicineName` varchar(150) NOT NULL,
  `Dosage` varchar(100) DEFAULT NULL,
  `Duration` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`PrescMedID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:09


-- =====================================================
-- carepharmacydb_bills.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `bills`
--

DROP TABLE IF EXISTS `bills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bills` (
  `BillID` int(11) NOT NULL AUTO_INCREMENT,
  `BillDate` datetime NOT NULL DEFAULT current_timestamp(),
  `CustomerName` varchar(100) DEFAULT NULL,
  `PrescriptionID` int(11) DEFAULT NULL,
  `CashierUserID` int(11) NOT NULL,
  `SubTotal` decimal(10,2) NOT NULL,
  `GSTAmount` decimal(10,2) NOT NULL DEFAULT 0.00,
  `DiscountAmount` decimal(10,2) NOT NULL DEFAULT 0.00,
  `GrandTotal` decimal(10,2) NOT NULL,
  `PaymentMethod` varchar(30) NOT NULL DEFAULT 'Cash',
  `Status` varchar(20) NOT NULL DEFAULT 'Completed' CHECK (`Status` in ('Completed','Cancelled','Refunded')),
  `CustomerPhone` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`BillID`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:09


-- =====================================================
-- carepharmacydb_billitems.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `billitems`
--

DROP TABLE IF EXISTS `billitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `billitems` (
  `BillItemID` int(11) NOT NULL AUTO_INCREMENT,
  `BillID` int(11) NOT NULL,
  `MedicineID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL CHECK (`Quantity` > 0),
  `UnitPrice` decimal(10,2) NOT NULL,
  `TotalPrice` decimal(10,2) NOT NULL,
  PRIMARY KEY (`BillItemID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:09


-- =====================================================
-- carepharmacydb_settings.sql
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `settings` (
  `SettingKey` varchar(50) NOT NULL,
  `SettingValue` varchar(200) NOT NULL,
  `Description` varchar(300) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`SettingKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:11


-- =====================================================
-- carepharmacydb_routines.sql (views/routines)
-- =====================================================

-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carepharmacydb
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Temporary view structure for view `vw_expiryalerts`
--

DROP TABLE IF EXISTS `vw_expiryalerts`;
/*!50001 DROP VIEW IF EXISTS `vw_expiryalerts`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_expiryalerts` AS SELECT 
 1 AS `MedicineID`,
 1 AS `MedicineName`,
 1 AS `Brand`,
 1 AS `StockQty`,
 1 AS `ExpiryDate`,
 1 AS `DaysToExpiry`,
 1 AS `AlertType`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_lowstockalerts`
--

DROP TABLE IF EXISTS `vw_lowstockalerts`;
/*!50001 DROP VIEW IF EXISTS `vw_lowstockalerts`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_lowstockalerts` AS SELECT 
 1 AS `MedicineID`,
 1 AS `MedicineName`,
 1 AS `Brand`,
 1 AS `StockQty`,
 1 AS `MinStockLevel`,
 1 AS `CategoryName`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_topsellingmedicines`
--

DROP TABLE IF EXISTS `vw_topsellingmedicines`;
/*!50001 DROP VIEW IF EXISTS `vw_topsellingmedicines`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_topsellingmedicines` AS SELECT 
 1 AS `MedicineID`,
 1 AS `MedicineName`,
 1 AS `Brand`,
 1 AS `CategoryName`,
 1 AS `TotalSold`,
 1 AS `TotalRevenue`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_dailysales`
--

DROP TABLE IF EXISTS `vw_dailysales`;
/*!50001 DROP VIEW IF EXISTS `vw_dailysales`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_dailysales` AS SELECT 
 1 AS `SaleDate`,
 1 AS `TotalBills`,
 1 AS `TotalRevenue`,
 1 AS `TotalGST`,
 1 AS `TotalDiscounts`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `vw_expiryalerts`
--

/*!50001 DROP VIEW IF EXISTS `vw_expiryalerts`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = cp850 */;
/*!50001 SET character_set_results     = cp850 */;
/*!50001 SET collation_connection      = cp850_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_expiryalerts` AS select `m`.`MedicineID` AS `MedicineID`,`m`.`MedicineName` AS `MedicineName`,`m`.`Brand` AS `Brand`,`m`.`StockQty` AS `StockQty`,`m`.`ExpiryDate` AS `ExpiryDate`,to_days(`m`.`ExpiryDate`) - to_days(current_timestamp()) AS `DaysToExpiry`,case when `m`.`ExpiryDate` < cast(current_timestamp() as date) then 'Expired' else 'Expiring Soon' end AS `AlertType` from `medicines` `m` where `m`.`ExpiryDate` <= cast(current_timestamp() as date) + interval 30 day and `m`.`IsAvailable` = 1 */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_lowstockalerts`
--

/*!50001 DROP VIEW IF EXISTS `vw_lowstockalerts`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = cp850 */;
/*!50001 SET character_set_results     = cp850 */;
/*!50001 SET collation_connection      = cp850_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_lowstockalerts` AS select `m`.`MedicineID` AS `MedicineID`,`m`.`MedicineName` AS `MedicineName`,`m`.`Brand` AS `Brand`,`m`.`StockQty` AS `StockQty`,`m`.`MinStockLevel` AS `MinStockLevel`,`c`.`CategoryName` AS `CategoryName` from (`medicines` `m` join `medicinecategories` `c` on(`m`.`CategoryID` = `c`.`CategoryID`)) where `m`.`StockQty` <= `m`.`MinStockLevel` and `m`.`IsAvailable` = 1 */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_topsellingmedicines`
--

/*!50001 DROP VIEW IF EXISTS `vw_topsellingmedicines`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = cp850 */;
/*!50001 SET character_set_results     = cp850 */;
/*!50001 SET collation_connection      = cp850_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_topsellingmedicines` AS select `m`.`MedicineID` AS `MedicineID`,`m`.`MedicineName` AS `MedicineName`,`m`.`Brand` AS `Brand`,`c`.`CategoryName` AS `CategoryName`,sum(`bi`.`Quantity`) AS `TotalSold`,sum(`bi`.`TotalPrice`) AS `TotalRevenue` from (((`billitems` `bi` join `medicines` `m` on(`bi`.`MedicineID` = `m`.`MedicineID`)) join `medicinecategories` `c` on(`m`.`CategoryID` = `c`.`CategoryID`)) join `bills` `b` on(`bi`.`BillID` = `b`.`BillID`)) where `b`.`Status` = 'Completed' group by `m`.`MedicineID`,`m`.`MedicineName`,`m`.`Brand`,`c`.`CategoryName` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_dailysales`
--

/*!50001 DROP VIEW IF EXISTS `vw_dailysales`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = cp850 */;
/*!50001 SET character_set_results     = cp850 */;
/*!50001 SET collation_connection      = cp850_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_dailysales` AS select cast(`b`.`BillDate` as date) AS `SaleDate`,count(distinct `b`.`BillID`) AS `TotalBills`,sum(`b`.`GrandTotal`) AS `TotalRevenue`,sum(`b`.`GSTAmount`) AS `TotalGST`,sum(`b`.`DiscountAmount`) AS `TotalDiscounts` from `bills` `b` where `b`.`Status` = 'Completed' group by cast(`b`.`BillDate` as date) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-20 20:58:11


/* =========================================================
   DEMO / SAMPLE DATA
   Synthetic data only. No real customer/patient information.
   ========================================================= */

SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO `roles` (`RoleID`,`RoleName`) VALUES
(1,'Admin'),(2,'Pharmacist'),(3,'Cashier'),(4,'Supplier Manager');

INSERT INTO `users`
(`UserID`,`FullName`,`Username`,`Email`,`PasswordHash`,`CNIC`,`RoleID`,`IsActive`,`CreatedAt`) VALUES
(1,'Demo Administrator','admin.demo','admin.demo@example.com','DEMO_HASH_ADMIN','00000-0000000-0',1,1,'2026-08-01 09:00:00'),
(2,'Ali Raza','pharmacist.demo','pharmacist.demo@example.com','DEMO_HASH_PHARMACIST','00000-0000001-0',2,1,'2026-08-02 09:15:00'),
(3,'Sara Ahmed','cashier.demo','cashier.demo@example.com','DEMO_HASH_CASHIER','00000-0000002-0',3,1,'2026-08-03 10:00:00'),
(4,'Usman Khan','supplier.demo','supplier.demo@example.com','DEMO_HASH_SUPPLIER','00000-0000003-0',4,1,'2026-08-04 10:30:00'),
(5,'Hassan Malik','pharmacist2.demo','pharmacist2.demo@example.com','DEMO_HASH_PHARMACIST2','00000-0000004-0',2,1,'2026-08-05 11:00:00');

INSERT INTO `medicinecategories` (`CategoryID`,`CategoryName`) VALUES
(1,'Pain Relief'),(2,'Antibiotics'),(3,'Cold & Flu'),(4,'Vitamins & Supplements'),
(5,'Gastrointestinal'),(6,'Allergy'),(7,'Diabetes'),(8,'First Aid');

INSERT INTO `suppliers`
(`SupplierID`,`SupplierName`,`ContactName`,`Phone`,`Email`,`Address`,`IsActive`,`CreatedAt`) VALUES
(1,'MedCare Distributors','Ahmed Shah','0300-0000001','medcare@example.com','Lahore Demo Address',1,'2026-08-01 08:30:00'),
(2,'HealthPlus Pharma','Bilal Ahmed','0300-0000002','healthplus@example.com','Lahore Demo Address',1,'2026-08-02 09:00:00'),
(3,'PakMed Supplies','Usman Ali','0300-0000003','pakmed@example.com','Islamabad Demo Address',1,'2026-08-03 09:30:00'),
(4,'CareSource Healthcare','Hina Tariq','0300-0000004','caresource@example.com','Rawalpindi Demo Address',1,'2026-08-04 10:00:00'),
(5,'Wellness Pharma','Hamza Iqbal','0300-0000005','wellness@example.com','Karachi Demo Address',1,'2026-08-05 10:30:00'),
(6,'Prime Medical Traders','Ayesha Noor','0300-0000006','prime@example.com','Faisalabad Demo Address',1,'2026-08-06 11:00:00');

INSERT INTO `medicines`
(`MedicineID`,`CategoryID`,`SupplierID`,`MedicineName`,`GenericName`,`Brand`,`BatchNumber`,
`UnitPrice`,`StockQty`,`MinStockLevel`,`ExpiryDate`,`RequiresPrescription`,`IsAvailable`,
`AddedBy`,`CreatedAt`,`UpdatedAt`,`PurchasePrice`,`SellingPrice`) VALUES
(1,1,1,'Paracetamol 500mg','Paracetamol','DemoPar','BAT-PA-001',12.00,120,20,'2027-06-30',0,1,2,'2026-08-01 09:00:00',NULL,8.00,12.00),
(2,1,2,'Ibuprofen 400mg','Ibuprofen','DemoIbu','BAT-IB-002',18.00,75,15,'2027-04-30',0,1,2,'2026-08-01 09:10:00',NULL,12.00,18.00),
(3,2,3,'Amoxicillin 500mg','Amoxicillin','DemoMox','BAT-AM-003',35.00,45,10,'2027-02-28',1,1,2,'2026-08-02 09:20:00',NULL,25.00,35.00),
(4,3,1,'Cough Relief Syrup','Dextromethorphan','DemoCough','BAT-CS-004',145.00,28,10,'2027-01-31',0,1,2,'2026-08-02 10:00:00',NULL,105.00,145.00),
(5,3,4,'Cold & Flu Tablets','Paracetamol + Phenylephrine','DemoCold','BAT-CF-005',22.00,18,20,'2026-12-31',0,1,2,'2026-08-03 10:15:00',NULL,15.00,22.00),
(6,4,5,'Vitamin C 500mg','Ascorbic Acid','DemoC','BAT-VC-006',10.00,200,30,'2028-03-31',0,1,2,'2026-08-03 11:00:00',NULL,6.50,10.00),
(7,4,6,'Multivitamin Tablets','Multivitamin','DemoVita','BAT-MV-007',28.00,95,20,'2027-11-30',0,1,2,'2026-08-04 09:00:00',NULL,19.00,28.00),
(8,5,2,'Antacid Suspension','Aluminium Hydroxide','DemoAntacid','BAT-AS-008',95.00,32,10,'2027-08-31',0,1,2,'2026-08-04 09:30:00',NULL,70.00,95.00),
(9,5,3,'Omeprazole 20mg','Omeprazole','DemoOme','BAT-OM-009',16.00,14,15,'2027-05-31',1,1,2,'2026-08-05 10:00:00',NULL,10.00,16.00),
(10,6,4,'Cetirizine 10mg','Cetirizine','DemoCet','BAT-CT-010',9.00,60,15,'2027-09-30',0,1,2,'2026-08-05 10:20:00',NULL,5.50,9.00),
(11,6,5,'Loratadine 10mg','Loratadine','DemoLor','BAT-LR-011',14.00,52,10,'2027-10-31',0,1,2,'2026-08-05 11:00:00',NULL,8.00,14.00),
(12,7,6,'Glucose Test Strips','Blood Glucose Test Strips','DemoStrip','BAT-GS-012',650.00,12,15,'2027-12-31',0,1,2,'2026-08-06 09:00:00',NULL,520.00,650.00),
(13,7,1,'Metformin 500mg','Metformin','DemoMet','BAT-MT-013',11.00,80,20,'2027-07-31',1,1,2,'2026-08-06 09:20:00',NULL,7.00,11.00),
(14,8,2,'Antiseptic Solution','Povidone-Iodine','DemoSept','BAT-PS-014',180.00,25,8,'2028-01-31',0,1,2,'2026-08-06 10:00:00',NULL,130.00,180.00),
(15,8,3,'Adhesive Bandages','Adhesive Bandage','DemoBand','BAT-AB-015',75.00,40,10,'2029-01-31',0,1,2,'2026-08-06 10:30:00',NULL,50.00,75.00);

INSERT INTO `stockdeliverymaster`
(`DeliveryID`,`SupplierID`,`InvoiceNo`,`DeliveryDate`,`ReceivedBy`,`Notes`) VALUES
(1,1,'DEMO-INV-001','2026-08-05',2,'Demo delivery - general medicines'),
(2,2,'DEMO-INV-002','2026-08-07',2,'Demo delivery - gastrointestinal medicines'),
(3,3,'DEMO-INV-003','2026-08-10',4,'Demo delivery - antibiotics and first aid'),
(4,5,'DEMO-INV-004','2026-08-12',2,'Demo delivery - vitamins and allergy medicines');

INSERT INTO `stockdeliverydetails`
(`DetailID`,`DeliveryID`,`MedicineID`,`BatchNumber`,`ExpiryDate`,`QtyReceived`,`UnitCost`) VALUES
(1,1,1,'BAT-PA-001','2027-06-30',100,8.00),
(2,1,4,'BAT-CS-004','2027-01-31',30,105.00),
(3,1,5,'BAT-CF-005','2026-12-31',40,15.00),
(4,2,8,'BAT-AS-008','2027-08-31',35,70.00),
(5,2,9,'BAT-OM-009','2027-05-31',25,10.00),
(6,3,3,'BAT-AM-003','2027-02-28',50,25.00),
(7,3,14,'BAT-PS-014','2028-01-31',30,130.00),
(8,3,15,'BAT-AB-015','2029-01-31',50,50.00),
(9,4,6,'BAT-VC-006','2028-03-31',150,6.50),
(10,4,11,'BAT-LR-011','2027-10-31',60,8.00);

INSERT INTO `prescriptions`
(`PrescriptionID`,`CustomerName`,`CustomerPhone`,`DoctorName`,`PrescriptionDate`,`RecordedBy`,`CreatedAt`,`Notes`) VALUES
(1,'Demo Customer A','0300-1111111','Dr. Ahmed','2026-08-08',2,'2026-08-08 10:15:00','Demo prescription'),
(2,'Demo Customer B','0300-2222222','Dr. Sara','2026-08-09',2,'2026-08-09 11:00:00','Demo prescription'),
(3,'Demo Customer C','0300-3333333','Dr. Hamza','2026-08-11',4,'2026-08-11 12:30:00','Demo prescription'),
(4,'Demo Customer D','0300-4444444','Dr. Hina','2026-08-13',2,'2026-08-13 14:00:00','Demo prescription'),
(5,'Demo Customer E','0300-5555555','Dr. Usman','2026-08-15',2,'2026-08-15 16:20:00','Demo prescription');

INSERT INTO `prescriptionmedicines`
(`PrescMedID`,`PrescriptionID`,`MedicineID`,`MedicineName`,`Dosage`,`Duration`) VALUES
(1,1,3,'Amoxicillin 500mg','1 capsule, 3 times daily','5 days'),
(2,1,13,'Metformin 500mg','1 tablet after dinner','30 days'),
(3,2,9,'Omeprazole 20mg','1 capsule before breakfast','14 days'),
(4,2,10,'Cetirizine 10mg','1 tablet at night','7 days'),
(5,3,3,'Amoxicillin 500mg','1 capsule, 2 times daily','5 days'),
(6,4,13,'Metformin 500mg','1 tablet twice daily','30 days'),
(7,5,2,'Ibuprofen 400mg','1 tablet after meal as needed','3 days'),
(8,5,9,'Omeprazole 20mg','1 capsule daily','7 days');

INSERT INTO `bills`
(`BillID`,`BillDate`,`CustomerName`,`PrescriptionID`,`CashierUserID`,`SubTotal`,`GSTAmount`,
`DiscountAmount`,`GrandTotal`,`PaymentMethod`,`Status`,`CustomerPhone`) VALUES
(1,'2026-08-08 10:30:00','Demo Customer A',1,3,70.00,3.50,0.00,73.50,'Cash','Completed','0300-1111111'),
(2,'2026-08-09 11:20:00','Demo Customer B',2,3,25.00,1.25,2.00,24.25,'Card','Completed','0300-2222222'),
(3,'2026-08-10 12:10:00','Demo Walk-in',NULL,3,120.00,6.00,5.00,121.00,'Cash','Completed','0300-6666666'),
(4,'2026-08-11 13:45:00','Demo Customer C',3,3,105.00,5.25,0.00,110.25,'Cash','Completed','0300-3333333'),
(5,'2026-08-12 15:00:00','Demo Walk-in',NULL,3,180.00,9.00,10.00,179.00,'Card','Completed','0300-7777777'),
(6,'2026-08-13 14:15:00','Demo Customer D',4,3,22.00,1.10,0.00,23.10,'Cash','Completed','0300-4444444'),
(7,'2026-08-14 16:40:00','Demo Walk-in',NULL,3,95.00,4.75,0.00,99.75,'Cash','Completed','0300-8888888'),
(8,'2026-08-15 17:10:00','Demo Customer E',5,3,52.00,2.60,2.00,52.60,'Cash','Completed','0300-5555555'),
(9,'2026-08-16 10:05:00','Demo Walk-in',NULL,3,75.00,3.75,0.00,78.75,'Card','Completed','0300-9999999'),
(10,'2026-08-17 18:00:00','Demo Walk-in',NULL,3,36.00,1.80,1.00,36.80,'Cash','Cancelled','0300-1212121');

INSERT INTO `billitems`
(`BillItemID`,`BillID`,`MedicineID`,`Quantity`,`UnitPrice`,`TotalPrice`) VALUES
(1,1,3,2,35.00,70.00),
(2,2,9,1,16.00,16.00),
(3,2,10,1,9.00,9.00),
(4,3,1,5,12.00,60.00),
(5,3,10,2,9.00,18.00),
(6,3,6,2,10.00,20.00),
(7,3,15,1,75.00,75.00),
(8,4,3,3,35.00,105.00),
(9,5,14,1,180.00,180.00),
(10,6,5,1,22.00,22.00),
(11,7,8,1,95.00,95.00),
(12,8,2,2,18.00,36.00),
(13,8,9,1,16.00,16.00),
(14,9,15,1,75.00,75.00),
(15,10,1,3,12.00,36.00);

INSERT INTO `settings`
(`SettingKey`,`SettingValue`,`Description`,`UpdatedBy`,`UpdatedAt`) VALUES
('PharmacyName','Care Pharmacy Demo','Demo pharmacy name',1,'2026-08-01 09:00:00'),
('Currency','PKR','Demo currency setting',1,'2026-08-01 09:00:00'),
('TaxRate','5','Demo GST/tax rate percentage',1,'2026-08-01 09:00:00'),
('LowStockThreshold','10','Default low-stock threshold',1,'2026-08-01 09:00:00'),
('ExpiryAlertDays','30','Show medicines expiring within this many days',1,'2026-08-01 09:00:00'),
('ReceiptFooter','Thank you for visiting Care Pharmacy','Demo receipt footer',1,'2026-08-01 09:00:00');

SET FOREIGN_KEY_CHECKS = 1;
