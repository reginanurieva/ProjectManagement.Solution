-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 03, 2018 at 10:02 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `project_management_test`
--
CREATE DATABASE IF NOT EXISTS `project_management_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `project_management_test`;

-- --------------------------------------------------------

--
-- Table structure for table `forums`
--

CREATE TABLE `forums` (
  `id` int(32) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects`
--

CREATE TABLE `projects` (
  `id` int(32) NOT NULL,
  `name` varchar(255) NOT NULL,
  `content` text NOT NULL,
  `duedate` datetime NOT NULL,
  `status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects_forums`
--

CREATE TABLE `projects_forums` (
  `id` int(32) NOT NULL,
  `project_id` int(32) NOT NULL,
  `forum_id` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects_tags`
--

CREATE TABLE `projects_tags` (
  `id` int(32) NOT NULL,
  `project_id` int(32) NOT NULL,
  `tag_id` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects_todos`
--

CREATE TABLE `projects_todos` (
  `id` int(32) NOT NULL,
  `project_id` int(32) NOT NULL,
  `todo_id` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `projects_users`
--

CREATE TABLE `projects_users` (
  `id` int(32) NOT NULL,
  `project_id` int(32) NOT NULL,
  `user_id` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tags`
--

CREATE TABLE `tags` (
  `id` int(32) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `todos`
--

CREATE TABLE `todos` (
  `id` int(32) NOT NULL,
  `name` varchar(255) NOT NULL,
  `status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(32) NOT NULL,
  `name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `forums`
--
ALTER TABLE `forums`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `projects_forums`
--
ALTER TABLE `projects_forums`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `projects_tags`
--
ALTER TABLE `projects_tags`
  ADD PRIMARY KEY (`id`),
  ADD KEY `projects_tags project_id foreign key` (`project_id`),
  ADD KEY `projects_tags tag_id foreign key` (`tag_id`);

--
-- Indexes for table `projects_todos`
--
ALTER TABLE `projects_todos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `projects_todos project_id foreign key` (`project_id`),
  ADD KEY `projects_todos todo_id` (`todo_id`);

--
-- Indexes for table `projects_users`
--
ALTER TABLE `projects_users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `project_users project_id foreign key` (`project_id`),
  ADD KEY `project_users user_id foreign key` (`user_id`);

--
-- Indexes for table `tags`
--
ALTER TABLE `tags`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `todos`
--
ALTER TABLE `todos`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `forums`
--
ALTER TABLE `forums`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `projects`
--
ALTER TABLE `projects`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `projects_forums`
--
ALTER TABLE `projects_forums`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `projects_tags`
--
ALTER TABLE `projects_tags`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `projects_todos`
--
ALTER TABLE `projects_todos`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `projects_users`
--
ALTER TABLE `projects_users`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `tags`
--
ALTER TABLE `tags`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `todos`
--
ALTER TABLE `todos`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `projects_tags`
--
ALTER TABLE `projects_tags`
  ADD CONSTRAINT `projects_tags project_id foreign key` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `projects_tags tag_id foreign key` FOREIGN KEY (`tag_id`) REFERENCES `tags` (`id`);

--
-- Constraints for table `projects_todos`
--
ALTER TABLE `projects_todos`
  ADD CONSTRAINT `projects_todos project_id foreign key` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `projects_todos todo_id` FOREIGN KEY (`todo_id`) REFERENCES `todos` (`id`);

--
-- Constraints for table `projects_users`
--
ALTER TABLE `projects_users`
  ADD CONSTRAINT `project_users project_id foreign key` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `project_users user_id foreign key` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
