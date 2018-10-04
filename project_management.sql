-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 04, 2018 at 05:20 PM
-- Server version: 5.7.23
-- PHP Version: 7.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `project_management`
--

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
  `status` varchar(255) NOT NULL DEFAULT 'Undone'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `projects`
--

INSERT INTO `projects` (`id`, `name`, `content`, `duedate`, `status`) VALUES
(377, 'Project Management Website', '<p>This project will create a project management website. Examples of what we\'re aiming for would be:</p><ul><li>Kaggle</li><li>Trello</li></ul><p><br></p><p class=\"ql-align-justify\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p><p><br></p>', '2018-10-04 00:00:00', 'In Progress'),
(378, 'Unity Game', '<p>This project will create a <em>real time strategy</em> game using the Unity gaming engine.</p><p><br></p><p class=\"ql-align-justify\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. </p><ol><li class=\"ql-align-justify\">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. </li><li class=\"ql-align-justify\">Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</li></ol><p><br></p><p><br></p>', '2018-10-04 00:00:00', 'In Progress'),
(379, 'Clock Management', '<p>This project will create and online clock in and out system for companies to track employees time.</p><p><br></p><ul><li>Clock In</li><li>Clock Out</li><li>Shake It All About</li><li>Do the Hokey Pokey</li><li>Turn Yourself Around</li></ul><p><br></p><p>That\'s what it\'s all about.</p>', '2018-10-05 00:00:00', 'In Progress');

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
-- Table structure for table `projects_owners`
--

CREATE TABLE `projects_owners` (
  `id` int(11) NOT NULL,
  `project_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `projects_owners`
--

INSERT INTO `projects_owners` (`id`, `project_id`, `user_id`) VALUES
(2, 377, 194),
(3, 378, 194),
(4, 379, 194);

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

--
-- Dumping data for table `projects_todos`
--

INSERT INTO `projects_todos` (`id`, `project_id`, `todo_id`) VALUES
(1, 375, 1),
(3, 377, 3),
(4, 377, 4),
(5, 377, 5),
(6, 377, 6),
(9, 377, 9),
(10, 377, 10),
(11, 377, 11),
(12, 377, 12),
(13, 378, 13),
(14, 378, 14),
(15, 378, 15),
(16, 378, 16),
(17, 378, 17),
(18, 378, 18),
(19, 378, 19),
(20, 378, 20),
(21, 378, 21),
(22, 379, 22),
(23, 379, 23),
(24, 379, 24),
(25, 379, 25),
(26, 379, 26),
(27, 379, 27),
(28, 379, 28),
(29, 379, 29);

-- --------------------------------------------------------

--
-- Table structure for table `projects_users`
--

CREATE TABLE `projects_users` (
  `id` int(32) NOT NULL,
  `project_id` int(32) NOT NULL,
  `user_id` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `projects_users`
--

INSERT INTO `projects_users` (`id`, `project_id`, `user_id`) VALUES
(112, 377, 194),
(113, 378, 194),
(114, 379, 194);

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

--
-- Dumping data for table `todos`
--

INSERT INTO `todos` (`id`, `name`, `status`) VALUES
(1, 'Something to do', 'In progress'),
(3, 'Create Models', 'Done'),
(4, 'Create Controllers', 'Done'),
(5, 'Create Views', 'Done'),
(6, 'Create Animation', 'Stuck'),
(9, 'Style Home Page', 'In Progress'),
(10, 'Style MyProjects Page', 'In Progress'),
(11, 'Create Name & Logo', 'Todo'),
(12, 'Create README', 'Todo'),
(13, 'Find Character Models', 'Done'),
(14, 'Design World', 'Done'),
(15, 'Design Turn Counter', 'Done'),
(16, 'Smooth Movement', 'Stuck'),
(17, 'Weapons', 'In Progress'),
(18, 'Range Attacks', 'In Progress'),
(19, 'Jumping', 'In Progress'),
(20, 'Dance Parties', 'Stuck'),
(21, 'Create README', 'Todo'),
(22, 'Create Models', 'Done'),
(23, 'Create Views', 'Done'),
(24, 'Create Controllers', 'In Progress'),
(25, 'Style Pages', 'Todo'),
(26, 'Add Javascript', 'Todo'),
(27, 'Add Korean language', 'Done'),
(28, 'Add Japanese language', 'In Progress'),
(29, 'Add Russian language', 'Stuck');

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
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `username`, `email`) VALUES
(191, 'Meria Thomas', 'MeriaT', 'josemeria93@gmail.com'),
(193, 'Hyewon Cho', 'jhng2525', 'jhng2525@gmail.com'),
(194, 'Chris Crow', 'ChrisMCrow', 'chrismcrow@gmail.com');

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
-- Indexes for table `projects_owners`
--
ALTER TABLE `projects_owners`
  ADD PRIMARY KEY (`id`),
  ADD KEY `projects_owners project_id foreign key` (`project_id`),
  ADD KEY `projects_owners user_id foreign key` (`user_id`);

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
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=380;

--
-- AUTO_INCREMENT for table `projects_forums`
--
ALTER TABLE `projects_forums`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `projects_owners`
--
ALTER TABLE `projects_owners`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `projects_tags`
--
ALTER TABLE `projects_tags`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `projects_todos`
--
ALTER TABLE `projects_todos`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `projects_users`
--
ALTER TABLE `projects_users`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=115;

--
-- AUTO_INCREMENT for table `tags`
--
ALTER TABLE `tags`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `todos`
--
ALTER TABLE `todos`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=195;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `projects_owners`
--
ALTER TABLE `projects_owners`
  ADD CONSTRAINT `projects_owners project_id foreign key` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`),
  ADD CONSTRAINT `projects_owners user_id foreign key` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

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
