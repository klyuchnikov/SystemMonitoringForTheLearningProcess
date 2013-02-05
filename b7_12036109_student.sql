-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Хост: sql109.byethost7.com
-- Время создания: Янв 12 2013 г., 05:48
-- Версия сервера: 5.5.27-28.0
-- Версия PHP: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `b7_12036109_student`
--

-- --------------------------------------------------------

--
-- Структура таблицы `coment_discip`
--

CREATE TABLE IF NOT EXISTS `coment_discip` (
  `id_discip_prepod` bigint(20) DEFAULT NULL,
  `id_history_stud` bigint(20) DEFAULT NULL,
  `is_individual` tinyint(1) NOT NULL,
  `comment` text NOT NULL,
  KEY `fk_individual_discip_prepod` (`id_discip_prepod`),
  KEY `fk_individual_hystory_stud` (`id_history_stud`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

-- --------------------------------------------------------

--
-- Структура таблицы `comment_discip`
--

CREATE TABLE IF NOT EXISTS `comment_discip` (
  `id_stud` bigint(20) DEFAULT NULL,
  `id_discip` bigint(20) DEFAULT NULL,
  `comment` text NOT NULL,
  KEY `fk_comment_discip_discip` (`id_discip`),
  KEY `fk_comment_discip_student` (`id_stud`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

-- --------------------------------------------------------

--
-- Структура таблицы `discip`
--

CREATE TABLE IF NOT EXISTS `discip` (
  `full_name` varchar(200) NOT NULL,
  `socr` varchar(20) NOT NULL,
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=5 ;

--
-- Дамп данных таблицы `discip`
--

INSERT INTO `discip` (`full_name`, `socr`, `id`) VALUES
('Системное программное обеспечение', 'СПО', 1),
('Автоматизированное проектирование информационных систем', 'АПИС', 2),
('Базы данных', 'БД', 3),
('Технология программирования', 'ТП', 4);

-- --------------------------------------------------------

--
-- Структура таблицы `discip_droup`
--

CREATE TABLE IF NOT EXISTS `discip_droup` (
  `id_group` bigint(20) DEFAULT NULL,
  `id_discip` bigint(20) DEFAULT NULL,
  KEY `fk_discip_droup_discip` (`id_discip`),
  KEY `fk_discip_droup_groups` (`id_group`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `discip_droup`
--

INSERT INTO `discip_droup` (`id_group`, `id_discip`) VALUES
(1, 1),
(2, 2),
(2, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `discip_prepod`
--

CREATE TABLE IF NOT EXISTS `discip_prepod` (
  `id_discip` bigint(20) DEFAULT NULL,
  `id_prepod` bigint(20) DEFAULT NULL,
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `study_hours_itog` bigint(20) NOT NULL,
  `semestr` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_discip_prepod_discip` (`id_discip`),
  KEY `fk_discip_prepod_prepod` (`id_prepod`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `discip_prepod`
--

INSERT INTO `discip_prepod` (`id_discip`, `id_prepod`, `id`, `study_hours_itog`, `semestr`) VALUES
(1, 1, 1, 100, 1),
(2, 2, 2, 150, 1),
(3, 4, 3, 120, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `discip_prepod_type_work`
--

CREATE TABLE IF NOT EXISTS `discip_prepod_type_work` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `discip_prepod_id` bigint(20) DEFAULT NULL,
  `type_work_id` bigint(20) DEFAULT NULL,
  `study_hours` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_discip_prepod_type_work_discip_prepod` (`discip_prepod_id`),
  KEY `fk_discip_prepod_type_work_type_work` (`type_work_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `discip_prepod_type_work`
--

INSERT INTO `discip_prepod_type_work` (`id`, `discip_prepod_id`, `type_work_id`, `study_hours`) VALUES
(1, 1, 1, 100),
(2, 2, 1, 150);

-- --------------------------------------------------------

--
-- Структура таблицы `etap_work`
--

CREATE TABLE IF NOT EXISTS `etap_work` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `discip_prepod_type_work_id` bigint(20) DEFAULT NULL,
  `theme` text NOT NULL,
  `date` date NOT NULL,
  `link` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_etap_discip_prepod_type_work` (`discip_prepod_type_work_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=7 ;

--
-- Дамп данных таблицы `etap_work`
--

INSERT INTO `etap_work` (`id`, `discip_prepod_type_work_id`, `theme`, `date`, `link`) VALUES
(1, 1, 'Изучение Руби', '2012-09-01', 'vk.com'),
(2, 2, 'Изучение Github', '2012-09-01', 'vk.com'),
(3, 1, 'Лабораторная 1', '2013-01-05', ''),
(4, 1, 'Лабораторная 2', '2013-01-06', ''),
(5, 1, 'Лабораторная 3', '2013-01-08', ''),
(6, 1, 'Лабораторная 4', '2013-01-09', '');

-- --------------------------------------------------------

--
-- Структура таблицы `groups`
--

CREATE TABLE IF NOT EXISTS `groups` (
  `socr` varchar(10) NOT NULL,
  `kurs` int(11) NOT NULL,
  `num_group` int(11) NOT NULL,
  `full_name` varchar(20) NOT NULL,
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=7 ;

--
-- Дамп данных таблицы `groups`
--

INSERT INTO `groups` (`socr`, `kurs`, `num_group`, `full_name`, `id`) VALUES
('ЭВМ', 4, 1, 'ЭВМд-41', 1),
('БЭВМ', 4, 1, 'БЭВМд-41', 2),
('ЭВМ', 3, 1, 'ЭВМд-31', 3),
('ЭВМ', 3, 2, 'ЭВМд-32', 4),
('ИВТ', 2, 1, 'ИВТд-21', 5),
('ИВТ', 2, 2, 'ИВТд-22', 6);

-- --------------------------------------------------------

--
-- Структура таблицы `hystory_stud`
--

CREATE TABLE IF NOT EXISTS `hystory_stud` (
  `date` date NOT NULL,
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `status_id` bigint(20) DEFAULT NULL,
  `id_stud` bigint(20) DEFAULT NULL,
  `groups_id` bigint(20) DEFAULT NULL,
  `comment` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_hystory_stud_groups` (`groups_id`),
  KEY `fk_hystory_stud_status` (`status_id`),
  KEY `fk_hystory_stud_student` (`id_stud`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=38 ;

--
-- Дамп данных таблицы `hystory_stud`
--

INSERT INTO `hystory_stud` (`date`, `id`, `status_id`, `id_stud`, `groups_id`, `comment`) VALUES
('2012-09-01', 3, 3, 1, 1, '0'),
('2012-09-01', 4, 3, 3, 2, '0'),
('2012-09-01', 5, 3, 4, 3, '0'),
('2012-09-01', 6, 3, 5, 3, '0'),
('2012-09-01', 7, 3, 6, 1, ''),
('2012-09-01', 8, 3, 7, 1, ''),
('2012-09-01', 9, 3, 8, 1, ''),
('2012-09-01', 10, 3, 9, 1, ''),
('2012-09-01', 11, 3, 10, 1, ''),
('2012-09-01', 12, 3, 11, 1, ''),
('2012-09-01', 13, 3, 12, 1, ''),
('2012-09-01', 14, 3, 13, 1, ''),
('2012-09-01', 15, 3, 14, 1, ''),
('2012-09-01', 16, 3, 15, 1, ''),
('2012-09-01', 17, 3, 16, 1, ''),
('2012-09-01', 18, 3, 17, 1, ''),
('2012-09-01', 19, 3, 18, 1, ''),
('2012-09-01', 20, 3, 19, 1, ''),
('2012-09-01', 21, 3, 20, 1, ''),
('2012-09-01', 22, 3, 21, 1, ''),
('2012-09-01', 23, 3, 22, 1, ''),
('2012-09-01', 24, 3, 23, 1, ''),
('2012-09-01', 25, 3, 24, 1, ''),
('2012-09-01', 26, 3, 25, 1, ''),
('2012-09-01', 27, 3, 26, 1, ''),
('2012-09-01', 28, 3, 27, 1, ''),
('2012-09-01', 29, 3, 28, 1, ''),
('2012-09-01', 30, 3, 29, 1, ''),
('2012-09-01', 31, 3, 30, 2, ''),
('2012-09-01', 32, 3, 31, 2, ''),
('2012-09-01', 33, 3, 32, 2, ''),
('2012-09-01', 34, 3, 33, 2, ''),
('2012-09-01', 35, 3, 34, 2, ''),
('2012-09-01', 36, 3, 35, 2, ''),
('2012-09-01', 37, 3, 36, 2, '');

-- --------------------------------------------------------

--
-- Структура таблицы `lecture`
--

CREATE TABLE IF NOT EXISTS `lecture` (
  `id_etap` bigint(20) DEFAULT NULL,
  `id_histiry_stud` bigint(20) DEFAULT NULL,
  `presence` tinyint(4) NOT NULL,
  `theme_doklad` varchar(500) NOT NULL,
  `grade` bigint(20) NOT NULL,
  KEY `fk_lecture_etap_work` (`id_etap`),
  KEY `fk_lecture_hystory_stud` (`id_histiry_stud`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `lecture`
--

INSERT INTO `lecture` (`id_etap`, `id_histiry_stud`, `presence`, `theme_doklad`, `grade`) VALUES
(1, 3, 1, 'Доклад1', 2),
(2, 6, 1, 'Доклад2', 5);

-- --------------------------------------------------------

--
-- Структура таблицы `prepod`
--

CREATE TABLE IF NOT EXISTS `prepod` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `surname` varchar(200) NOT NULL,
  `name` varchar(200) NOT NULL,
  `patronymic` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=6 ;

--
-- Дамп данных таблицы `prepod`
--

INSERT INTO `prepod` (`id`, `surname`, `name`, `patronymic`) VALUES
(1, 'Макаров', 'Павел', 'Сергеевич'),
(2, 'Карпушин', 'Алексей', 'Николаевич'),
(3, 'Власенко', 'Олег', 'Федосович'),
(4, 'Игонин', 'Андрей', 'Геннадьевич'),
(5, 'Войт', 'Николай', 'Николаевич');

-- --------------------------------------------------------

--
-- Структура таблицы `status`
--

CREATE TABLE IF NOT EXISTS `status` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `status`
--

INSERT INTO `status` (`id`, `name`) VALUES
(1, 'отчислен'),
(2, 'академ'),
(3, 'очное');

-- --------------------------------------------------------

--
-- Структура таблицы `student`
--

CREATE TABLE IF NOT EXISTS `student` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `surname` varchar(200) NOT NULL,
  `name` varchar(200) NOT NULL,
  `patronymic` varchar(200) NOT NULL,
  `is_starosta` tinyint(1) NOT NULL,
  `mail` varchar(200) NOT NULL,
  `phone` varchar(50) NOT NULL,
  `comment_stud` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 COMMENT='?????????? ? ????????? ????????? ?????' AUTO_INCREMENT=37 ;

--
-- Дамп данных таблицы `student`
--

INSERT INTO `student` (`id`, `surname`, `name`, `patronymic`, `is_starosta`, `mail`, `phone`, `comment_stud`) VALUES
(1, 'Бексаева', 'Светлана', 'Вячеславовна', 0, 'beksaeva@gmail.com', '89084839905', 'бла бла бла'),
(2, 'Емельянова', 'Екатерина', 'Денисовна', 0, 'katy.emeljanova@gmail.com', '89084839905', 'умничка'),
(3, 'Фолунин', 'Владимир', 'Александрович', 1, 'fol34@ya.ru', '03', 'староста'),
(4, 'Фёдоров', 'Ярослав', 'Владимирович', 0, 'fedorov@gmail.com', '111111', 'алярики'),
(5, 'Хусаинов', 'Павел', 'Альбертович', 0, 'husainov@gmail.com', '222222', ''),
(6, 'Айзятуллен', 'Ренат', '', 0, '', '', ''),
(7, 'Ашанин', 'Вениамин', '', 0, '', '', ''),
(8, 'Баранников', 'Павел', '', 0, '', '', ''),
(9, 'Барсков', 'Евгений', '', 0, '', '', ''),
(10, 'Бексаева', 'Светлана', '', 0, '', '', ''),
(11, 'Бригаднов', 'Сергей', '', 0, '', '', ''),
(12, 'Вагин', 'Александр', '', 0, '', '', ''),
(13, 'Гуськов', 'Глеб', '', 0, '', '', ''),
(14, 'Дьякова', 'Варвара', '', 0, '', '', ''),
(15, 'Емельянова', 'Екатерина', '', 0, '', '', ''),
(16, 'Зарипов', 'Рустам', '', 0, '', '', ''),
(17, 'Казанчеев', 'Сергей', '', 0, '', '', ''),
(18, 'Коротин', 'Юрий', '', 0, '', '', ''),
(19, 'Логинов', 'Андрей', '', 0, '', '', ''),
(20, 'Минякова', 'Наталья', '', 0, '', '', ''),
(21, 'Нагорнов', 'Олег', '', 0, '', '', ''),
(22, 'Овченков', 'Андрей', '', 0, '', '', ''),
(23, 'Панфилов', 'Денис', '', 0, '', '', ''),
(24, 'Печеркин', 'Евгений', '', 0, '', '', ''),
(25, 'Русаков', 'Никита', '', 0, '', '', ''),
(26, 'Селюнина', 'Екатерина', '', 0, '', '', ''),
(27, 'Франк', 'Алексей', '', 0, '', '', ''),
(28, 'Шилимов', 'Николай', '', 0, '', '', ''),
(29, 'Калашников', 'Павел', '', 0, '', '', ''),
(30, 'Ключников', 'Дмитрий', '', 0, '', '', ''),
(31, 'Лешко', 'Алексей', 'Эдуардович', 0, '', '', ''),
(32, 'Матюшин', 'Данила', 'Юрьевич', 0, '', '', ''),
(33, 'Савичева', 'Юлия', 'Александровна', 0, '', '', ''),
(34, 'Фёдоров', 'Ярослав', 'Владимирович', 0, '', '', ''),
(35, 'Фолунин', 'Владимир', 'Александрович', 0, '', '', ''),
(36, 'Хусаинов', 'Павел', 'Альбертович', 0, '', '', '');

-- --------------------------------------------------------

--
-- Структура таблицы `tokens`
--

CREATE TABLE IF NOT EXISTS `tokens` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `oauth_token` varchar(50) DEFAULT NULL,
  `client_id` varchar(200) DEFAULT NULL,
  `expires` bigint(20) NOT NULL,
  `scope` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=183 ;

--
-- Дамп данных таблицы `tokens`
--

INSERT INTO `tokens` (`id`, `oauth_token`, `client_id`, `expires`, `scope`) VALUES
(2, '4eeff006593c00032b9a438d08e34a12', 'admin@mail.com', 1356898215, NULL),
(3, '86c49f73fa11f0881e1667e9c8c45837', 'admin@mail.com', 1356901686, NULL),
(4, '930d5be4b6f1e161b2113d2a685503b7', 'admin@mail.com', 1356902581, NULL),
(5, '0e97b3bdc5504de8a067e6bb405ecbc9', 'admin@mail.com', 1356914878, NULL),
(6, 'fcf4f7e38b5ba48cd9107858464f8dbb', 'admin@mail.com', 1357057739, NULL),
(7, 'c7604ff4020f393c63f620bd18d9c8ae', 'admin@mail.com', 1357060452, NULL),
(8, '5306e1df8ca702802b1c4a64d44109f1', 'admin@mail.com', 1357060622, NULL),
(9, '5deb7360e590eaed645bfcfbe96df978', 'admin@mail.com', 1357061726, NULL),
(10, '7635c9145945be81449a59296fdf6b63', 'admin@mail.com', 1357062345, NULL),
(11, 'c1e56cbe9dfeadd2da0a88e17c4c683d', 'admin@mail.com', 1357063302, NULL),
(12, '721729e0048b0b9c1d14ce47687681ed', 'admin@mail.com', 1357063357, NULL),
(13, '9510ed4633b322ed2b5c232f179fa5d7', 'admin@mail.com', 1357063377, NULL),
(14, '605666b926246a9e3fb082aaa4a06e5f', 'admin@mail.com', 1357063398, NULL),
(15, '98e97f7a1fac2d1028d38c1104e7d025', 'admin@mail.com', 1357064575, NULL),
(16, '3ff40b24b2e961f179df1b9699a63501', 'admin@mail.com', 1357065252, NULL),
(17, '00d575d52e87df91f052355f3ee8986d', 'admin@mail.com', 1357065442, NULL),
(18, '3fca0e56a1efb84b517b8400e2a25e8e', 'admin@mail.com', 1357065546, NULL),
(19, 'f2b11c5e7bbbe73a13873b195cd64196', 'admin@mail.com', 1357065618, NULL),
(20, '7bfdccd8f2b4267623f6176d699e8e49', 'admin@mail.com', 1357066514, NULL),
(21, 'fa2706e79f289c0e25d9378ba5a46d32', 'admin@mail.com', 1357078803, NULL),
(22, '4416065acb4fd811b67c5a5c1c87b460', 'admin@mail.com', 1357078834, NULL),
(23, '9c18bd116d27b9ad259f02a3e5c845c5', 'admin@mail.com', 1357078915, NULL),
(24, 'c8478db99dfb062ae26d84326dd4a4a6', 'admin@mail.com', 1357079180, NULL),
(25, 'f77ac148e59d34cfdcea740326876645', 'admin@mail.com', 1357079224, NULL),
(26, '453b72bbda6209d1cefc4287e41fd3e7', 'admin@mail.com', 1357079706, NULL),
(27, '94734049eb4669c417a8f800cef31310', 'admin@mail.com', 1357079751, NULL),
(28, 'ec5d48443cac39304581033361db5f93', 'admin@mail.com', 1357079803, NULL),
(29, '24fd17fba19d773d9b23558fa2f5bd47', 'admin@mail.com', 1357079865, NULL),
(30, '4379d8ab8408fc6b870a89e2341fe2c9', 'admin@mail.com', 1357079895, NULL),
(31, '698aba9ff311b3714a87f4884fd2964e', 'admin@mail.com', 1357080125, NULL),
(32, '70e154ee3756a5735af190133eadc531', 'admin@mail.com', 1357080149, NULL),
(33, 'b052917ec7436a8659d5ce2f0feed734', 'admin@mail.com', 1357080247, NULL),
(34, '72f940e3df09d22353f3b12967a3faf6', 'admin@mail.com', 1357080281, NULL),
(35, '1559db3a8680814c93f1d0333d55f69f', 'admin@mail.com', 1357080475, NULL),
(36, '05013ffa1941e3c42b781be14aa78e08', 'admin@mail.com', 1357081101, NULL),
(37, 'e827c532f0ba58f292eb91928bb669ec', 'admin@mail.com', 1357081108, NULL),
(38, 'c38306fdc982e6b91ca4f50b529023fe', 'kadmin@mail.com', 1357081133, NULL),
(39, '38ecb8681e76ac239f09bde6a1283248', 'kadmin@mail.com', 1357081149, NULL),
(40, '6047d3df85cf54e7e8d813f4e7722879', 'kadmin@mail.com', 1357081164, NULL),
(41, '24ee7fd5d841e8bed5673bb055aaa8da', 'admin@maila.com', 1357082422, NULL),
(42, '4bc39b79efacfddbb86d700e45bdbe12', 'adminnn@mail.com', 1357082445, NULL),
(43, '91411be3144608dd409d0d888eddc5e7', 'admin@mail.com', 1357082513, NULL),
(44, 'bdef2a8725ee52f99e6bd75dd2d2a41f', 'admin@mail.com', 1357082517, NULL),
(45, '2b792e606854a9982f223a649aa44e31', 'admin@mail.com', 1357082519, NULL),
(46, 'b9f2f8d82a691b91e108a19928bba44e', 'admin@mail.com', 1357082543, NULL),
(47, '884618eacc40505055919da50cf30e14', 'admin@mail.com', 1357082591, NULL),
(48, '2bd2ca25ba3d1fb300bd2560ced45c47', 'admin@mail.com', 1357082651, NULL),
(49, '96611dda6f1652821990db3f226fe7b5', 'admin@mail.com', 1357082811, NULL),
(50, '1c37a097e4bdcf2ad6c9f6b876b7149f', 'admin@mail.com', 1357082894, NULL),
(51, '6b33ba603932f9a9b6522c67daff1edd', 'admin@mail.com', 1357082906, NULL),
(52, 'df918a0fda9db1fe799ce077168495e3', 'admin@mail.com', 1357082950, NULL),
(53, '033faac881b0f1491af93120131055cd', 'admin@mail.com', 1357083017, NULL),
(54, 'c611cb4c11d0a891b494a5633edbf1d4', 'admin@mail.com', 1357083054, NULL),
(55, '6ce987245a269ff2ba03307a95675274', 'admin@mail.com', 1357083219, NULL),
(56, 'a8a03c75cc452d11682acb685d8c71dc', 'admin@mail.com', 1357083319, NULL),
(57, '90e556e9910211996ec16ed16cb1a879', 'admin@mail.com', 1357083716, NULL),
(58, '06c27a32aba10612a6c234af6724fef3', 'admin@mail.com', 1357083805, NULL),
(59, '19b11d98afb69b7e2b83a0ab306db7f1', 'admin@mail.com', 1357083891, NULL),
(60, '64f4667f88a4d3d673bbb78a608ff674', 'admin@mail.com', 1357083975, NULL),
(61, 'ebaa8cd874421eea61288f02c1b97979', 'admin@mail.com', 1357084021, NULL),
(62, '1c19477cec47a1bbfe53b9014b751b39', 'admin@mail.com', 1357084503, NULL),
(63, '9e176f7d9d47a5456562078796b00350', 'admin@mail.com', 1357084517, NULL),
(64, '6fdc7eec414178b864574e9d273d4c93', 'admin@mail.com', 1357085373, NULL),
(65, 'efefcd816d206b1a64f57f518611d5df', 'admin@mail.com', 1357085479, NULL),
(66, 'a5cee73649cffa69ffa08c11a35863f9', 'admin@mail.com', 1357091939, NULL),
(67, '1600eff70922be4e031289d03c03d2de', 'admin@mail.com', 1357092201, NULL),
(68, '700fd758571f2334ad2847429d3de3e1', 'admin@mail.com', 1357092307, NULL),
(69, 'ca9bdffa7976a9f0c5a69130b11e6b25', 'admin@mail.com', 1357092448, NULL),
(70, 'd414c1c7d02d9c7a4990fee0fcee035e', 'admin@mail.com', 1357092697, NULL),
(71, 'f9884abaa8ab14a38d1ca992d77a2915', 'admin@mail.com', 1357245322, NULL),
(72, '86e918359a8de04267a01d684116a074', 'admin@mail.com', 1357246410, NULL),
(73, 'e3c371e92bbde7579a00230098bf0fa5', 'admin@mail.com', 1357249598, NULL),
(74, 'f313605a41a1de0b72eaebfa064e922c', 'admin@mail.com', 1357249841, NULL),
(75, '8b4a1c4fe268f0257caf431ebf3c340b', 'admin@mail.com', 1357249877, NULL),
(76, '6714991c4128dc09faa641ab0b99322d', 'admin@mail.com', 1357249912, NULL),
(77, 'ec6a8e848e0eeace1c1eaeb285fbc0ed', 'admin@mail.com', 1357250167, NULL),
(78, '47a067ed71f85024e0b18341b4cbed8e', 'admin@mail.com', 1357250460, NULL),
(79, '4246c4051f091273f14bc07b7b449e04', 'admin@mail.com', 1357250538, NULL),
(80, 'c7d909218846af6d77df827152200bfb', 'admin@mail.com', 1357250790, NULL),
(81, '3e1a4e69449b1ba28084c4efa3fb577f', 'admin@mail.com', 1357250980, NULL),
(82, 'b93619cc4394b7c7f5485239edc9f986', 'admin@mail.com', 1357251164, NULL),
(83, '427fb6f1fcea37d0f60e7d168f3cae52', 'admin@mail.com', 1357251376, NULL),
(84, 'd6997e7e5d570336f7f46e7494adae77', 'admin@mail.com', 1357251463, NULL),
(85, '3689d6e1b7906bf700a0aefac1e3f2fd', 'admin@mail.com', 1357252032, NULL),
(86, 'cb89c84276dfc8ffd22b8d9cecc9904e', 'admin@mail.com', 1357252174, NULL),
(87, '46a53941cb20c84a4beca6fdea54af5c', 'admin@mail.com', 1357252215, NULL),
(88, 'f8aab421200fa98f217786cc9dfdd401', 'admin@mail.com', 1357252314, NULL),
(89, 'e7cd8b84f92ce757914103856db14d13', 'admin@mail.com', 1357252369, NULL),
(90, '331b868389f653da11ba7fb69b1a9969', 'admin@mail.com', 1357252490, NULL),
(91, 'fe8de54df4b16416a20ffabbc3ca2f02', 'admin@mail.com', 1357253379, NULL),
(92, '623faae1e452a8746cd486e555a1e6f7', 'admin@mail.com', 1357253495, NULL),
(93, 'e7df10e68ea99cc95ea48f3f65beb7fa', 'admin@mail.com', 1357253588, NULL),
(94, '11dd01f6e9de78f66d912427f68360b6', 'admin@mail.com', 1357253640, NULL),
(95, 'ea24c497f821c73a26a8c334f32b2ed7', 'admin@mail.com', 1357253690, NULL),
(96, 'cfc1f8a76e4f27fd368d4c310229a1f8', 'admin@mail.com', 1357253765, NULL),
(97, 'f5524432c76d1a0347da0d1681107577', 'admin@mail.com', 1357253882, NULL),
(98, '521f9741e7efdf3b295734908355b3b5', 'admin@mail.com', 1357253916, NULL),
(99, '82ac43041d9706325408f92b2d126be2', 'admin@mail.com', 1357253923, NULL),
(100, '9580fd916249dd484b9be44593eb3b56', 'admin@mail.com', 1357254039, NULL),
(101, '06b6874c746fba0c861cdbc5ea7356c6', 'admin@mail.com', 1357254099, NULL),
(102, '41b54a3d516be2998f949f1d4c6b8d4e', 'admin@mail.com', 1357254173, NULL),
(103, '821a2264d144fd56d597ae413a7c3b39', 'admin@mail.com', 1357254246, NULL),
(104, 'cc9db81f729235999c3497da3d4ba836', 'admin@mail.com', 1357254287, NULL),
(105, 'e0d7c36fd927ed8a17b7a2cca8ce5d75', 'admin@mail.com', 1357254445, NULL),
(106, 'a5acdf1c719bb7b0a5a3e56898550012', 'admin@mail.com', 1357254470, NULL),
(107, '0d83499e227ae5fc5be492f6eb0e3a92', 'admin@mail.com', 1357254486, NULL),
(108, '5ac5b66eedd58f02860a6e3bbc86e522', 'admin@mail.com', 1357254537, NULL),
(109, 'ee2936e3f2c2776cbb1c59666bcf523e', 'admin@mail.com', 1357256692, NULL),
(110, '489d8fe7c7332edae314159dc1373a98', 'admin@mail.com', 1357256864, NULL),
(111, '847da42ff62368992543b75c941caf9b', 'admin@mail.com', 1357256913, NULL),
(112, 'dc59725cc53b63d7e1b36d9ad7bcb4e6', 'admin@mail.com', 1357257141, NULL),
(113, '13dda144beaf557ee5099320fc2a38ba', 'admin@mail.com', 1357257175, NULL),
(114, '5e2d174d7237d7d4b12ab6bb1a79855e', 'admin@mail.com', 1357264043, NULL),
(115, 'fc1e80fa87aaf743e633023df94950f4', 'admin@mail.com', 1357264068, NULL),
(116, '66695cbc7f012392b1e72de2b16442d5', 'admin@mail.com', 1357264098, NULL),
(117, '5118780aad19f18482a2eee1aba1641e', 'admin@mail.com', 1357264185, NULL),
(118, '7b7e73e9616e85424c5859de61cc1a66', 'admin@mail.com', 1357264379, NULL),
(119, '76ec369faed4cdb7072f2b2f23c3ab61', 'admin@mail.com', 1357264433, NULL),
(120, '1c47f4658cc89b4fc6fdca4423c61504', 'admin@mail.com', 1357264500, NULL),
(121, 'cae010c88692b95914a7cdf3c93ec3fc', 'admin@mail.com', 1357264521, NULL),
(122, '24a58f5ebfb7d82668518084848316c2', 'admin@mail.com', 1357264549, NULL),
(123, 'b443632062216609343674baa7b7fe0b', 'admin@mail.com', 1357264557, NULL),
(124, '0ff1c76eb78676a9c0844539e7e857c1', 'admin@mail.com', 1357264596, NULL),
(125, '8c0c1f58a2c304fa640d3365eacb7e44', 'admin@mail.com', 1357334797, NULL),
(126, '498fad5617142281d466d8feed1f0a09', 'admin@mail.com', 1357334844, NULL),
(127, '0b10c677ae3819499cff5892661d9380', 'admin@mail.com', 1357335233, NULL),
(128, '7afae694d9fd4fb7ac034893c2e35590', 'admin@mail.com', 1357343530, NULL),
(129, '68ff81939f22f33c1f7c1356e0043be0', 'admin@mail.com', 1357343549, NULL),
(130, '41b57278825ce983399d8e2fdf83f52e', 'admin@mail.com', 1357343640, NULL),
(131, 'b1b82f6bc94c6c96e4e35da835c23d7b', 'admin@mail.com', 1357343782, NULL),
(132, '703ec6c5629bb16bebb0188c8f495789', 'admin@mail.com', 1357343953, NULL),
(133, '88c3822dcbcb122505b430345b4a0f41', 'admin@mail.com', 1357344085, NULL),
(134, '148f986a16f9a42b9348b8b72eeae767', 'admin@mail.com', 1357344946, NULL),
(135, '50f746b2776c240f3d290f7825e8526d', 'admin@mail.com', 1357350016, NULL),
(136, 'aac8cfb7b4bc265658a5261aca93ae71', 'admin@mail.com', 1357353702, NULL),
(137, 'f2769d16d55a9072e376dfcd4423522e', 'admin@mail.com', 1357357498, NULL),
(138, 'b11f89ada40d95c871fa9e271935d17b', 'admin@mail.com', 1357361129, NULL),
(139, '07f93cd5bed38744b3cdb7ad5fbacd7f', 'admin@mail.com', 1357362483, NULL),
(140, 'a0a005bd33e1dd6a765bf7dcd8faa0e2', 'admin@mail.com', 1357573870, NULL),
(141, '32138058e87c2aeac3eac40aaf21bba4', 'admin@mail.com', 1357577593, NULL),
(142, 'c0aa46e5eb8d52db51c5e4b92996c0dc', 'admin@mail.com', 1357580399, NULL),
(143, 'acfb74d7ae40c7c430ab69c0d86fde46', 'admin@mail.com', 1357584248, NULL),
(144, 'a56a0dce83f3946905c4592b3203f758', 'admin@mail.com', 1357587869, NULL),
(145, '413d5183b43e5022ca8c8ad063639cc8', 'admin@mail.com', 1357598205, NULL),
(146, '3354736007df7c6a0293007222a7457b', 'admin@mail.com', 1357601847, NULL),
(147, '9b6f436a6e10c785c03b7681247c76b7', 'admin@mail.com', 1357605778, NULL),
(148, 'c144a21b2ef4433d8facfa2951df6473', 'admin@mail.com', 1357609401, NULL),
(149, '5bb232ca6e25c64f5136ba1604e507e8', 'admin@mail.com', 1357613021, NULL),
(150, 'ba90752fb8328f71f0f78db6ee91f98d', 'admin@mail.com', 1357616216, NULL),
(151, '9d2027bb7dc21b3427731982f2d98991', 'admin@mail.com', 1357617198, NULL),
(152, '8652c19b3ae141d6625a6733fe8dedf7', 'admin@mail.com', 1357617427, NULL),
(153, '3e87a8a73049c7e71ae80313ee9d70b6', 'admin@mail.com', 1357617649, NULL),
(154, 'de8c2578190edfe41d413fac8c131d5f', 'admin@mail.com', 1357617928, NULL),
(155, '98b6c3502802446a87e884f17d1845d4', 'admin@mail.com', 1357617992, NULL),
(156, '92576aca5380aaabd37b7373941153c7', 'admin@mail.com', 1357618172, NULL),
(157, '689c0c1bbbed3a9971e717bd6c0a2710', 'admin@mail.com', 1357618541, NULL),
(158, '7a72190ccd889671bc7c8cfbe9c644ac', 'admin@mail.com', 1357621810, NULL),
(159, '11c25e796c9842394127d59e66027c9b', 'admin@mail.com', 1357622325, NULL),
(160, '13a225df1a049e55f6c958db07bc1b96', 'admin@mail.com', 1357622538, NULL),
(161, '1a58fca7ad7d3e0ea2a5c590b9b1f284', 'admin@mail.com', 1357629411, NULL),
(162, '081bab65f2f5fd517f8eebb81206d80a', 'admin@mail.com', 1357770302, NULL),
(163, 'f281fc5b927b2c977ecefb50da2d8db2', 'admin@mail.com', 1357773954, NULL),
(164, '73620b5739bc2a625218315da24a2666', 'admin@mail.com', 1357807940, NULL),
(165, 'fcf66b4bb91866e574bbe0b73e53443f', 'admin@mail.com', 1357808096, NULL),
(166, '0a79ca68e3c7b99e0d05b652746b5352', 'admin@mail.com', 1357962458, NULL),
(167, 'a35a581579131e4a0b7b95336ed6fced', 'admin@mail.com', 1357966182, NULL),
(168, '5f2afb808b5d5bbdf93c1c6f4018a0fc', 'admin@mail.com', 1357968128, NULL),
(169, '17a1b35e32c7097ec6ef1c7893f5b6e6', 'admin@mail.com', 1357968224, NULL),
(170, '144f343bb7af15b01d402d3077a69245', 'admin@mail.com', 1357970430, NULL),
(171, 'e82714b38bb42570a084b03c6fc4776b', 'admin@mail.com', 1357970538, NULL),
(172, '12466b6f3ed68271869b5c3b65eb88f4', 'admin@mail.com', 1357979744, NULL),
(173, 'c92487ca846a829a1cf03a034e280920', 'admin@mail.com', 1357979849, NULL),
(174, 'd61e78ed7a77082a57273676874caf56', 'admin@mail.com', 1357981050, NULL),
(175, 'a9680282cb086f11141c8b48d08566cd', 'admin@mail.com', 1357981689, NULL),
(176, '4c4e127b94f900b9e120290584877db3', 'admin@mail.com', 1357983051, NULL),
(177, '4da46ba04e1bdd180497333f1b6773df', 'admin@mail.com', 1357983355, NULL),
(178, '6d9f4aa55e5719625a18c114e57de349', 'admin@mail.com', 1357983440, NULL),
(179, '774357effa046c28d39e5b9ac1117b10', 'admin@mail.com', 1357983716, NULL),
(180, 'd2544f9260d2026d88a6a62268f4e9cf', 'admin@mail.com', 1357983828, NULL),
(181, '3751512dfafb11ad4bfd53e054f20b0b', 'admin@mail.com', 1357983955, NULL),
(182, 'a4ed414e48009953d0940dcc017a88e9', 'admin@mail.com', 1357983977, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `type_work`
--

CREATE TABLE IF NOT EXISTS `type_work` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=5 ;

--
-- Дамп данных таблицы `type_work`
--

INSERT INTO `type_work` (`id`, `name`) VALUES
(1, 'Лабораторная работа'),
(2, 'Лекция'),
(3, 'Зачёт'),
(4, 'Экзамен');

-- --------------------------------------------------------

--
-- Структура таблицы `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` varchar(50) NOT NULL,
  `active` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=cp1251 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `user`
--

INSERT INTO `user` (`id`, `email`, `password`, `role`, `active`) VALUES
(1, 'admin@mail.com', 'adminpass', 'администратор', 1),
(2, 'user@mail.ru', 'userpass', 'пользователь', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `variant_type_work`
--

CREATE TABLE IF NOT EXISTS `variant_type_work` (
  `id_discip_prepod_type_work` bigint(20) DEFAULT NULL,
  `id_history_stud` bigint(20) DEFAULT NULL,
  `variant` bigint(20) NOT NULL,
  KEY `fk_variant_type_work_discip_prepod_type_work` (`id_discip_prepod_type_work`),
  KEY `fk_variant_type_work_hystory_stud` (`id_history_stud`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `variant_type_work`
--

INSERT INTO `variant_type_work` (`id_discip_prepod_type_work`, `id_history_stud`, `variant`) VALUES
(1, 4, 1),
(2, 6, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `work_stud`
--

CREATE TABLE IF NOT EXISTS `work_stud` (
  `id_history_stud` bigint(20) DEFAULT NULL,
  `id_etap` bigint(20) DEFAULT NULL,
  `is_load` tinyint(4) NOT NULL,
  `path_doc` bigint(20) NOT NULL,
  `date_begin` date NOT NULL,
  `date_end` date NOT NULL,
  `progress` bigint(20) NOT NULL,
  `raiting` bigint(20) NOT NULL,
  `grade` bigint(20) NOT NULL,
  `comment` text NOT NULL,
  KEY `fk_work_stud_etap` (`id_etap`),
  KEY `fk_work_stud_hystory_stud` (`id_history_stud`)
) ENGINE=MyISAM DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `work_stud`
--

INSERT INTO `work_stud` (`id_history_stud`, `id_etap`, `is_load`, `path_doc`, `date_begin`, `date_end`, `progress`, `raiting`, `grade`, `comment`) VALUES
(4, 2, 1, 0, '2012-11-18', '2012-11-25', 100, 1, 5, 'молодец, возьми с полки пирожок'),
(3, 1, 1, 0, '2012-11-04', '2012-11-11', 20, 9, 2, 'позор двоеЧникам');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
