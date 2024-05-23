-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 23, 2024 at 10:12 AM
-- Server version: 8.3.0
-- PHP Version: 8.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `budget`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `User_ID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `User_ID` (`User_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`ID`, `Email`, `Password`, `User_ID`) VALUES
(1, 'admin@fintrack.com', 'Oneandonly123.', 1);

-- --------------------------------------------------------

--
-- Table structure for table `categoryexpense`
--

DROP TABLE IF EXISTS `categoryexpense`;
CREATE TABLE IF NOT EXISTS `categoryexpense` (
  `ID` int NOT NULL,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Income_ID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Income_ID` (`Income_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `categroyincome`
--

DROP TABLE IF EXISTS `categroyincome`;
CREATE TABLE IF NOT EXISTS `categroyincome` (
  `ID` int NOT NULL,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Income_ID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Income_ID` (`Income_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `expense`
--

DROP TABLE IF EXISTS `expense`;
CREATE TABLE IF NOT EXISTS `expense` (
  `ID` int NOT NULL,
  `Amount` float NOT NULL,
  `Month` int NOT NULL,
  `Year` int NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `income`
--

DROP TABLE IF EXISTS `income`;
CREATE TABLE IF NOT EXISTS `income` (
  `ID` int NOT NULL,
  `Amount` float NOT NULL,
  `Month` int NOT NULL,
  `Year` int NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `password_reset_requests`
--

DROP TABLE IF EXISTS `password_reset_requests`;
CREATE TABLE IF NOT EXISTS `password_reset_requests` (
  `RequestID` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `RequestTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `Status` varchar(50) COLLATE utf8mb4_general_ci DEFAULT 'Pending',
  PRIMARY KEY (`RequestID`)
) ENGINE=MyISAM AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `password_reset_requests`
--

INSERT INTO `password_reset_requests` (`RequestID`, `Email`, `RequestTime`, `Status`) VALUES
(1, 'user@fintrack.com', '2024-05-09 19:10:29', 'Completed'),
(2, 'user@fintrack.com', '2024-05-09 20:36:52', 'Completed'),
(3, 'user@fintrack.com', '2024-05-09 20:39:41', 'Completed'),
(4, 'user@fintrack.com', '2024-05-09 20:40:09', 'Completed'),
(5, 'user@fintrack.com', '2024-05-09 21:02:38', 'Denied'),
(6, 'user@fintrack.com', '2024-05-09 21:14:04', 'Completed'),
(7, 'user@fintrack.com', '2024-05-09 22:05:58', 'Completed'),
(8, 'user@fintrack.com', '2024-05-09 22:10:20', 'Denied'),
(9, 'user@fintrack.com', '2024-05-09 22:14:09', 'Completed'),
(10, 'user@fintrack.com', '2024-05-09 22:46:03', 'Completed'),
(14, 'useR4@fintrack.com', '2024-05-20 12:18:34', 'Pending'),
(13, 'user3@fintrack.com', '2024-05-19 22:16:45', 'Pending'),
(15, 'user4@fintrack.com', '2024-05-20 12:18:46', 'Pending'),
(16, 'user4@fintrack.com', '2024-05-20 12:22:31', 'Pending'),
(17, 'user2@fintrack.com', '2024-05-20 13:41:24', 'Pending'),
(18, 'user2@fintrack.com', '2024-05-20 13:43:24', 'Pending'),
(19, 'user2@fintrack.com', '2024-05-20 13:44:30', 'Pending'),
(20, 'user2@fintrack.com', '2024-05-20 13:45:25', 'Pending'),
(21, 'user2@fintrack.com', '2024-05-20 13:47:14', 'Pending'),
(22, 'user2@fintrack.com', '2024-05-20 13:48:09', 'Pending'),
(23, 'user2@fintrack.com', '2024-05-20 13:49:05', 'Pending'),
(24, 'user2@fintrack.com', '2024-05-20 13:49:47', 'Pending'),
(25, 'user2@fintrack.com', '2024-05-20 13:53:40', 'Pending'),
(26, 'user2@fintrack.com', '2024-05-20 13:59:19', 'Pending'),
(27, 'user2@fintrack.com', '2024-05-20 14:01:40', 'Pending'),
(28, 'user2@fintrack.com', '2024-05-20 14:04:58', 'Pending'),
(29, 'user2@fintrack.com', '2024-05-20 14:07:11', 'Pending'),
(30, 'user1@fintrack.com', '2024-05-20 14:42:07', 'Pending'),
(31, 'user1@fintrack.com', '2024-05-22 11:13:53', 'Pending'),
(32, 'user@fintrack.com', '2024-05-22 11:49:34', 'Pending');

-- --------------------------------------------------------

--
-- Table structure for table `savings`
--

DROP TABLE IF EXISTS `savings`;
CREATE TABLE IF NOT EXISTS `savings` (
  `ID` int NOT NULL,
  `User_ID` int NOT NULL,
  `total_income` float NOT NULL,
  `total_expense` float NOT NULL,
  `CalculatedAmount` float NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
CREATE TABLE IF NOT EXISTS `settings` (
  `ID` int NOT NULL,
  `User_ID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `settings_User` (`User_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `First_Name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Last_Name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Email` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Password` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `ProfilePhoto` blob NOT NULL,
  `SecurityQuestion1` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Answer1` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SecurityQuestion2` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Answer2` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SecurityQuestion3` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Answer3` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID`, `First_Name`, `Last_Name`, `Email`, `Password`, `ProfilePhoto`, `SecurityQuestion1`, `Answer1`, `SecurityQuestion2`, `Answer2`, `SecurityQuestion3`, `Answer3`) VALUES
(1, 'Ja', 'Nee', 'user@fintrack.com', 'Useruser123.', '', NULL, 'Germany', NULL, 'ILO', NULL, 'AGO'),
(2, 'Jan', 'Last', 'user1@fintrack.com', 'Jandelast123.', '', NULL, 'Italy', NULL, 'SLM', NULL, 'UZA'),
(3, 'An', 'De Pan', 'user2@fintrack.com', 'Andepan123.', '', NULL, 'France', NULL, 'AP', NULL, 'APP'),
(4, 'Tom', 'Peters', 'user3@fintrack.com', 'Tompeters123.', '', NULL, 'Belgium', NULL, 'ELE', NULL, 'APE'),
(6, 'ja', 'ja', 'JA@fintrack.com', '4154e8f7bd0d17f3fd31ff395b4ca2d3a1ee8e12340cc216a030a6e728aefe7e', '', NULL, 's', NULL, 'g', NULL, 'g'),
(7, 'Sara', 'Thompson', 'user4@fintrack.com', '6c4a2a1d8b64e7694091e4db0c6c84db96108a546835fa5a48d3f8d7b109f9a3', '', NULL, 'Belgium', NULL, 'Hah', NULL, 'Aha'),
(8, 'Ja', 'ja', 'ja@fintrack.com', '89ed915daa2ff630ec87938febebdf526a70fe5d6339dd1703df3511de36befe', '', NULL, 'ja', NULL, 'ja', NULL, 'ja');

-- --------------------------------------------------------

--
-- Table structure for table `userexpense`
--

DROP TABLE IF EXISTS `userexpense`;
CREATE TABLE IF NOT EXISTS `userexpense` (
  `ID` int NOT NULL,
  `User_ID` int NOT NULL,
  `Expense_ID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `User_ID` (`User_ID`),
  KEY `Expense_ID` (`Expense_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `userincome`
--

DROP TABLE IF EXISTS `userincome`;
CREATE TABLE IF NOT EXISTS `userincome` (
  `ID` int NOT NULL,
  `User_Id` int NOT NULL,
  `Income_Id` int NOT NULL,
  KEY `User_Id` (`User_Id`),
  KEY `Income_Id` (`Income_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `admin`
--
ALTER TABLE `admin`
  ADD CONSTRAINT `admin_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `user` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `categoryexpense`
--
ALTER TABLE `categoryexpense`
  ADD CONSTRAINT `categoryexpense_ibfk_1` FOREIGN KEY (`Income_ID`) REFERENCES `income` (`ID`);

--
-- Constraints for table `categroyincome`
--
ALTER TABLE `categroyincome`
  ADD CONSTRAINT `categroyincome_ibfk_1` FOREIGN KEY (`Income_ID`) REFERENCES `income` (`ID`);

--
-- Constraints for table `userexpense`
--
ALTER TABLE `userexpense`
  ADD CONSTRAINT `userexpense_ibfk_1` FOREIGN KEY (`User_ID`) REFERENCES `user` (`ID`),
  ADD CONSTRAINT `userexpense_ibfk_2` FOREIGN KEY (`Expense_ID`) REFERENCES `expense` (`ID`);

--
-- Constraints for table `userincome`
--
ALTER TABLE `userincome`
  ADD CONSTRAINT `userincome_ibfk_1` FOREIGN KEY (`User_Id`) REFERENCES `user` (`ID`),
  ADD CONSTRAINT `userincome_ibfk_2` FOREIGN KEY (`Income_Id`) REFERENCES `income` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
