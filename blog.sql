-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1:3306
-- Létrehozás ideje: 2026. Sze 21. 05:30
-- Kiszolgáló verziója: 8.4.7
-- PHP verzió: 8.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `blog`
--
CREATE DATABASE IF NOT EXISTS `blog` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `blog`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `blogger`
--

DROP TABLE IF EXISTS `blogger`;
CREATE TABLE IF NOT EXISTS `blogger` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `email` varchar(40) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `age` int DEFAULT NULL,
  `password` text COLLATE utf8mb4_hungarian_ci,
  `registrationTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `blogger`
--

INSERT INTO `blogger` (`id`, `name`, `email`, `age`, `password`, `registrationTime`) VALUES
(1, 'Kovacs Bela', 'kovacs.bela@gmail.com', 34, 'jelszo123', '2021-03-14 09:12:00'),
(2, 'Nagy Aniko', 'nagy.aniko@freemail.hu', 28, 'Titok2021', '2021-04-02 14:35:10'),
(3, 'Szabo Peter', 'szabo.peter@gmail.com', 52, 'Peti1972', '2021-04-18 08:05:45'),
(4, 'Toth Erika', 'toth.erika@citromail.hu', 41, 'erika41', '2021-05-09 19:22:30'),
(5, 'Horvath Gabor', 'horvath.gabor@gmail.com', 63, 'GaborBacsi', '2021-05-27 11:47:12'),
(6, 'Varga Julia', 'varga.julia@indamail.hu', 23, 'julcsi23', '2021-06-11 16:03:55'),
(7, 'Kiss Laszlo', 'kiss.laszlo@gmail.com', 37, 'Laci1988', '2021-06-30 07:58:20'),
(8, 'Molnar Zsofia', 'molnar.zsofia@gmail.com', 19, 'zsofi2005', '2021-07-15 21:14:05'),
(9, 'Nemeth Tamas', 'nemeth.tamas@freemail.hu', 45, 'TamasN45', '2021-08-01 10:26:38'),
(10, 'Farkas Andrea', 'farkas.andrea@gmail.com', 56, 'Andi1966', '2021-08-22 13:41:09'),
(11, 'Balogh Istvan', 'balogh.istvan@citromail.hu', 68, 'Pista68x', '2021-09-05 09:33:17'),
(12, 'Papp Katalin', 'papp.katalin@gmail.com', 31, 'Kati0931', '2021-09-19 18:07:44'),
(13, 'Takacs Roland', 'takacs.roland@gmail.com', 26, 'Roli2606', '2021-10-03 12:19:51'),
(14, 'Juhasz Marta', 'juhasz.marta@indamail.hu', 49, 'MartaJ49', '2021-10-25 15:55:02'),
(15, 'Lakatos Zoltan', 'lakatos.zoltan@gmail.com', 38, 'Zoli38', '2021-11-07 08:44:26'),
(16, 'Meszaros Eva', 'meszaros.eva@freemail.hu', 59, 'EvaM1963', '2021-11-28 20:31:13'),
(17, 'Olah Sandor', 'olah.sandor@gmail.com', 71, 'Sanyi71', '2021-12-12 10:02:47'),
(18, 'Simon Reka', 'simon.reka@gmail.com', 22, 'Reka2002', '2021-12-30 23:18:35'),
(19, 'Racz Kristof', 'racz.kristof@citromail.hu', 33, 'Kris3312', '2022-01-14 07:26:09'),
(20, 'Fekete Nikolett', 'fekete.niki@gmail.com', 27, 'Niki2795', '2022-01-29 17:40:22'),
(21, 'Szilagyi Adam', 'szilagyi.adam@gmail.com', 44, 'AdamSz44', '2022-02-11 11:11:11'),
(22, 'Torok Beatrix', 'torok.bea@freemail.hu', 36, 'Bea3610', '2022-02-27 14:53:48'),
(23, 'Fabian Denes', 'fabian.denes@gmail.com', 55, 'Denes55x', '2022-03-16 09:07:30'),
(24, 'Gal Veronika', 'gal.veronika@indamail.hu', 30, 'Vera3003', '2022-04-04 18:22:16'),
(25, 'Sipos Gergely', 'sipos.gergely@gmail.com', 21, 'Gergo21', '2022-04-21 22:35:59'),
(26, 'Boros Melinda', 'boros.melinda@gmail.com', 47, 'Meli4711', '2022-05-08 06:49:24'),
(27, 'Orosz Attila', 'orosz.attila@citromail.hu', 62, 'Atika62', '2022-05-25 13:14:37'),
(28, 'Halasz Dora', 'halasz.dora@gmail.com', 25, 'Dorka25', '2022-06-10 16:28:03'),
(29, 'Balazs Norbert', 'balazs.norbert@gmail.com', 39, 'Norbi39x', '2022-06-27 10:41:55'),
(30, 'Somogyi Timea', 'somogyi.timea@freemail.hu', 53, 'Timi53', '2022-07-13 19:56:12'),
(31, 'Bogdan Mate', 'bogdan.mate@gmail.com', 20, 'Mate2004', '2022-07-30 08:33:41'),
(32, 'Hegedus Klara', 'hegedus.klara@gmail.com', 66, 'Klari66', '2022-08-17 12:05:28'),
(33, 'Kelemen Viktor', 'kelemen.viktor@indamail.hu', 42, 'Viki4280', '2022-09-02 15:19:07'),
(34, 'Bognar Renata', 'bognar.renata@gmail.com', 29, 'Reni2993', '2022-09-20 20:44:50'),
(35, 'Fulop Csaba', 'fulop.csaba@gmail.com', 50, 'Csabi50', '2022-10-06 07:12:33'),
(36, 'Vincze Agnes', 'vincze.agnes@citromail.hu', 35, 'Agi3587', '2022-10-23 17:38:19'),
(37, 'Szucs Levente', 'szucs.levente@gmail.com', 24, 'Leve24', '2022-11-09 11:50:44'),
(38, 'Deak Petra', 'deak.petra@gmail.com', 46, 'Petra46', '2022-11-26 09:04:57'),
(39, 'Illes Barnabas', 'illes.barnabas@freemail.hu', 58, 'Barni58', '2022-12-14 21:27:15'),
(40, 'Katona Eszter', 'katona.eszter@gmail.com', 32, 'Eszti32', '2023-01-03 13:43:02'),
(41, 'Fodor Marcell', 'fodor.marcell@gmail.com', 18, 'Marci2006', '2023-01-21 18:59:38'),
(42, 'Pinter Szilvia', 'pinter.szilvia@indamail.hu', 61, 'Szilvi61', '2023-02-08 08:15:26'),
(43, 'Hajdu Krisztian', 'hajdu.krisztian@gmail.com', 40, 'Kriszti40', '2023-02-25 10:37:49'),
(44, 'Magyar Emese', 'magyar.emese@gmail.com', 54, 'Emese54x', '2023-03-15 16:02:11'),
(45, 'Balint Robert', 'balint.robert@citromail.hu', 48, 'Robi4875', '2023-04-01 19:24:36'),
(46, 'Veres Alexandra', 'veres.alexandra@gmail.com', 26, 'Szandi26', '2023-04-19 07:48:53'),
(47, 'Antal Zsolt', 'antal.zsolt@gmail.com', 43, 'Zsolti43', '2023-05-06 14:11:29'),
(48, 'Bakos Vivien', 'bakos.vivien@freemail.hu', 27, 'Vivi2796', '2023-05-24 22:06:41'),
(49, 'Gulyas Akos', 'gulyas.akos@gmail.com', 57, 'AkosG57', '2023-06-11 09:29:18'),
(50, 'Kozma Diana', 'kozma.diana@gmail.com', 21, 'Didi2103', '2023-06-29 12:52:07'),
(51, 'Sebestyen Tibor', 'sebestyen.tibor@indamail.hu', 73, 'Tibi73x', '2023-07-17 15:16:44'),
(52, 'Vass Henrietta', 'vass.henrietta@gmail.com', 37, 'Heni3786', '2023-08-04 20:39:22'),
(53, 'Baranyi Daniel', 'baranyi.daniel@gmail.com', 30, 'Dani3093', '2023-08-23 08:53:10'),
(54, 'Csonka Boglarka', 'csonka.boglarka@gmail.com', 51, 'Bogi51', '2023-09-10 17:07:35'),
(55, 'Doman Ferenc', 'doman.ferenc@citromail.hu', 64, 'Feri64x', '2023-09-28 11:31:58');
COMMIT;

CREATE TABLE IF NOT EXISTS `blogpost` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(40) DEFAULT NULL,
  `Content` text,
  `postTime` datetime DEFAULT NULL,
  `updateTime` datetime DEFAULT NULL,
  `blogId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (`blogId`) REFERENCES `blogger` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;