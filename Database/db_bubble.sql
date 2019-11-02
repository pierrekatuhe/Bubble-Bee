-- phpMyAdmin SQL Dump
-- version 4.8.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 02 Nov 2019 pada 09.54
-- Versi server: 10.1.32-MariaDB
-- Versi PHP: 5.6.36

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_bubble`
--
CREATE DATABASE IF NOT EXISTS `db_bubble` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `db_bubble`;

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_bet`
--

CREATE TABLE `tb_bet` (
  `username` varchar(20) NOT NULL,
  `id_permainan` int(6) NOT NULL,
  `id_bet` int(6) NOT NULL,
  `binatang` varchar(20) DEFAULT NULL,
  `angka` int(3) DEFAULT NULL,
  `warna` varchar(20) DEFAULT NULL,
  `keterangan` varchar(20) DEFAULT NULL,
  `bet` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_koin`
--

CREATE TABLE `tb_koin` (
  `id_koin` int(6) NOT NULL,
  `in_koin` bigint(20) DEFAULT NULL,
  `out_koin` bigint(20) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `time` time NOT NULL,
  `username_giver` varchar(20) NOT NULL,
  `username_receiver` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tb_koin`
--

INSERT INTO `tb_koin` (`id_koin`, `in_koin`, `out_koin`, `date`, `time`, `username_giver`, `username_receiver`) VALUES
(1, 100, NULL, '2019-09-25', '01:13:31', 'pierre', 'test'),
(2, NULL, 500, '2019-09-25', '01:13:36', 'pierre', ''),
(3, NULL, 100, '2019-09-25', '01:13:41', 'pierre', ''),
(4, NULL, 100, '2019-09-25', '01:13:45', 'pierre', ''),
(5, NULL, 100, '2019-09-25', '01:14:16', 'pierre', 'test'),
(6, NULL, 500, '2019-09-25', '01:14:22', 'pierre', ''),
(7, NULL, 100, '2019-09-25', '01:14:29', 'pierre', 'test'),
(8, 100, NULL, '2019-09-25', '01:14:36', 'pierre', 'test'),
(9, 100, NULL, '2019-09-25', '01:17:20', 'pierre', 'test'),
(10, NULL, 500, '2019-09-25', '01:17:29', 'pierre', 'test');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_log_activity`
--

CREATE TABLE `tb_log_activity` (
  `id_log` int(11) NOT NULL,
  `activity` varchar(50) NOT NULL,
  `username` varchar(20) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tb_log_activity`
--

INSERT INTO `tb_log_activity` (`id_log`, `activity`, `username`, `date`) VALUES
(1, 'Tambah Koin User', 'pierre', '2019-09-25 01:13:31'),
(2, 'Kurang Koin User', 'pierre', '2019-09-25 01:13:36'),
(3, 'Kurang Koin User', 'pierre', '2019-09-25 01:13:41'),
(4, 'Kurang Koin User', 'pierre', '2019-09-25 01:13:45'),
(5, 'Kurang Koin User', 'pierre', '2019-09-25 01:14:16'),
(6, 'Kurang Koin User', 'pierre', '2019-09-25 01:14:22'),
(7, 'Kurang Koin User', 'pierre', '2019-09-25 01:14:29'),
(8, 'Tambah Koin User', 'pierre', '2019-09-25 01:14:36'),
(9, 'Tambah Koin User', 'pierre', '2019-09-25 01:17:20'),
(10, 'Kurang Koin User', 'pierre', '2019-09-25 01:17:29'),
(11, 'Tambah User', 'pierre', '2019-10-07 15:36:06'),
(12, 'Update User', 'pierre', '2019-10-07 15:36:44'),
(13, 'Update User', 'pierre', '2019-10-07 15:38:28'),
(14, 'Tambah User', 'pierre', '2019-10-07 15:38:34'),
(15, 'Ubah Status User', 'pierre', '2019-10-07 15:38:37'),
(16, 'Ubah Status User', 'pierre', '2019-10-07 15:38:45'),
(17, 'Delete User', 'pierre', '2019-10-07 15:38:55');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_permainan`
--

CREATE TABLE `tb_permainan` (
  `id_permainan` int(6) NOT NULL,
  `status` int(1) NOT NULL,
  `angka` int(3) DEFAULT NULL,
  `keterangan` varchar(20) DEFAULT NULL,
  `time` datetime DEFAULT NULL,
  `bonus` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_setting`
--

CREATE TABLE `tb_setting` (
  `hadiah_2d` float NOT NULL,
  `maxbet_2d` int(11) NOT NULL,
  `hadiah_hewan` float NOT NULL,
  `maxbet_hewan` int(11) NOT NULL,
  `hadiah_bk` float NOT NULL,
  `maxbet_bk` int(11) NOT NULL,
  `hadiah_gg` float NOT NULL,
  `maxbet_gg` int(11) NOT NULL,
  `hadiah_warna` float NOT NULL,
  `maxbet_warna` int(11) NOT NULL,
  `hadiah_4warna` float NOT NULL,
  `maxbet_4warna` int(11) NOT NULL,
  `hadiah_12item` float NOT NULL,
  `maxbet_12item` int(11) NOT NULL,
  `time_limit` int(11) NOT NULL,
  `bonus` float NOT NULL,
  `bonus_angka` float NOT NULL,
  `bonus_hewan` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tb_setting`
--

INSERT INTO `tb_setting` (`hadiah_2d`, `maxbet_2d`, `hadiah_hewan`, `maxbet_hewan`, `hadiah_bk`, `maxbet_bk`, `hadiah_gg`, `maxbet_gg`, `hadiah_warna`, `maxbet_warna`, `hadiah_4warna`, `maxbet_4warna`, `hadiah_12item`, `maxbet_12item`, `time_limit`, `bonus`, `bonus_angka`, `bonus_hewan`) VALUES
(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_user`
--

CREATE TABLE `tb_user` (
  `username` varchar(20) NOT NULL,
  `nama` varchar(30) DEFAULT NULL,
  `password` varchar(20) NOT NULL,
  `koin` bigint(20) DEFAULT NULL,
  `hak_akses` varchar(20) NOT NULL,
  `last_login` datetime DEFAULT NULL,
  `created_by` varchar(20) DEFAULT NULL,
  `status` varchar(20) DEFAULT NULL,
  `koin_update` datetime NOT NULL,
  `koin_update_by` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tb_user`
--

INSERT INTO `tb_user` (`username`, `nama`, `password`, `koin`, `hak_akses`, `last_login`, `created_by`, `status`, `koin_update`, `koin_update_by`) VALUES
('a', 'a', 'a', 0, 'Staff Koin', NULL, 'pierre', 'Available', '0000-00-00 00:00:00', ''),
('admin', 'admin', '121117', 0, 'Owner', '2019-08-31 18:03:42', 'admin', 'Available', '0000-00-00 00:00:00', ''),
('pierre', 'Pierre', 'bwyugzym', 1001600, 'Kasir', '2019-10-07 15:38:16', 'admin', 'Available', '0000-00-00 00:00:00', ''),
('test', 'test', 'test', 99600, 'Customer', '2019-11-02 16:49:16', 'admin', 'Suspend', '0000-00-00 00:00:00', '');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tb_bet`
--
ALTER TABLE `tb_bet`
  ADD PRIMARY KEY (`id_bet`);

--
-- Indeks untuk tabel `tb_koin`
--
ALTER TABLE `tb_koin`
  ADD PRIMARY KEY (`id_koin`);

--
-- Indeks untuk tabel `tb_log_activity`
--
ALTER TABLE `tb_log_activity`
  ADD PRIMARY KEY (`id_log`);

--
-- Indeks untuk tabel `tb_permainan`
--
ALTER TABLE `tb_permainan`
  ADD PRIMARY KEY (`id_permainan`);

--
-- Indeks untuk tabel `tb_user`
--
ALTER TABLE `tb_user`
  ADD PRIMARY KEY (`username`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `tb_bet`
--
ALTER TABLE `tb_bet`
  MODIFY `id_bet` int(6) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT untuk tabel `tb_koin`
--
ALTER TABLE `tb_koin`
  MODIFY `id_koin` int(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT untuk tabel `tb_log_activity`
--
ALTER TABLE `tb_log_activity`
  MODIFY `id_log` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT untuk tabel `tb_permainan`
--
ALTER TABLE `tb_permainan`
  MODIFY `id_permainan` int(6) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
