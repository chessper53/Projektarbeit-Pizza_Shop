-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 01, 2023 at 01:31 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `projekt`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `id` bigint(20) NOT NULL,
  `Username` varchar(255) DEFAULT NULL,
  `Password` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id`, `Username`, `Password`) VALUES
(1, 'lahmichine', 'nigger1');

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `image` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `images`
--

INSERT INTO `images` (`id`, `name`, `image`) VALUES
(1, 'baller-roblox.gif', ''),
(2, '16680761535055571752716356916469.jpg', ''),
(3, '1055_4da02fa310a6b.jpg', ''),
(4, 'ezgif-1-ba809d61a0.gif', ''),
(5, 'dribbling-people-are-awesome.gif', ''),
(6, 'image_2022-11-10_110420349.png', ''),
(7, 'image_2022-11-16_083541253.png', ''),
(8, 'Unbenannt.png', ''),
(9, 'WIN_20221110_11_30_18_Pro.jpg', ''),
(10, 'arno_fliri_0.jpg', ''),
(11, '16680761535055571752716356916469.jpg', ''),
(12, '1055_4da02fa310a6b.jpg', ''),
(13, '16680761693528256434463698440770.jpg', ''),
(14, '1055_4da02fa310a6b.jpg', ''),
(15, 'image_2022-11-10_110411287.png', ''),
(16, 'fabiocangini.jpg', ''),
(17, 'baller-roblox.gif', ''),
(18, 'h.gif', '');

-- --------------------------------------------------------

--
-- Table structure for table `updatetable`
--

CREATE TABLE `updatetable` (
  `AmountofClears` int(11) NOT NULL,
  `Count` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `updatetable`
--

INSERT INTO `updatetable` (`AmountofClears`, `Count`) VALUES
(1, 2);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `NachName` varchar(255) NOT NULL,
  `Vorname` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`NachName`, `Vorname`) VALUES
('Damjan Ziconigger', 'Niggerwigger'),
('Damjan Ziconigger', 'Niggerwigger'),
('Damjan Ziconigger', 'Niggerwigger'),
('Damjan Ziconigger', 'Niggerwigger'),
('Damjan Ziconigger', 'Niggerwigger');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `updatetable`
--
ALTER TABLE `updatetable`
  ADD PRIMARY KEY (`AmountofClears`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `updatetable`
--
ALTER TABLE `updatetable`
  MODIFY `AmountofClears` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
