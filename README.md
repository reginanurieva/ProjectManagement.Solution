# Project Management version 1.0.
_by Hyewon Cho, Chris Crow, Meria Thomas, Regina Nurieva, and David Mortkowitz_

*Description*

_Project Management is a multitasking and job management database, created with a focus in collaboration and productivity._


### Project Management Specifications and Expected Behaviors

* _The user may browse the website and available projects without joining._
* _The user must register an account with Project Management in order to create a new project or join an existing one._
* _Once registered, the user may create new projects, assign directives and to-do lists, organize needed tasks, and keep track of what's done with click-and-drag to-do list functionality._
* _Created projects may be organized with tags, to organize your own projects and find other like-minded creators._


### Known bugs
* _There are no known bugs in this version._


### Directions for Installation
#### *Note*: This application was built using a Mac running OS X 10.11. If you are running Windows or a different version of OS X, these instructions may vary based on your operating system.

* _To launch this application, please have MAMP, .NET core version 1.0.4, and MySql installed and configured on your Mac._
* _Clone or download code from the Git Rep, located at:_
* https://github.com/hyewon92cho/ProjectManagement.Solution.git
* _Use a Database software such as MAMP, and turn on your server using your username and password._ 
* _Open Terminal, and launch MySql._
* _Once in MySql, run the following commands to create your database:_
```
  CREATE DATABASE project_management;
  USE project_management;
  CREATE TABLE forums (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE projects (id serial PRIMARY KEY, name VARCHAR(255), content VARCHAR(255), duedate DATETIME, status VARCHAR(255));
  CREATE TABLE projects_forums (id serial PRIMARY KEY, project_id INT(11), forum_id INT(11));
  CREATE TABLE projects_owners (id serial PRIMARY KEY, project_id INT(11), user_id INT(11));
  CREATE TABLE projects_tags (id serial PRIMARY KEY, project_id INT(11), tag_id INT(11));
  CREATE TABLE projects_todos (id serial PRIMARY KEY, project_id INT(11), todo_id INT(11));
  CREATE TABLE projects_users (id serial PRIMARY KEY, project_id INT(11), user_id INT(11));
  CREATE TABLE tags (id serial PRIMARY KEY, name VARCHAR(255));  
  CREATE TABLE todos (id serial PRIMARY KEY, name VARCHAR(255), status VARCHAR(255));
  CREATE TABLE users (id serial PRIMARY KEY, name VARCHAR(255), username VARCHAR(255), email VARCHAR (255));
```
* Next, in MySql, and run the following commands to create your test database:_
```
  CREATE DATABASE project_management_test;
  USE project_management_test;
  CREATE TABLE forums (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE projects (id serial PRIMARY KEY, name VARCHAR(255), content VARCHAR(255), duedate DATETIME, status VARCHAR(255));
  CREATE TABLE projects_forums (id serial PRIMARY KEY, project_id INT(11), forum_id INT(11));
  CREATE TABLE projects_owners (id serial PRIMARY KEY, project_id INT(11), user_id INT(11));
  CREATE TABLE projects_tags (id serial PRIMARY KEY, project_id INT(11), tag_id INT(11));
  CREATE TABLE projects_todos (id serial PRIMARY KEY, project_id INT(11), todo_id INT(11));
  CREATE TABLE projects_users (id serial PRIMARY KEY, project_id INT(11), user_id INT(11));
  CREATE TABLE tags (id serial PRIMARY KEY, name VARCHAR(255));  
  CREATE TABLE todos (id serial PRIMARY KEY, name VARCHAR(255), status VARCHAR(255));
  CREATE TABLE users (id serial PRIMARY KEY, name VARCHAR(255), username VARCHAR(255), email VARCHAR (255));
```
* _In terminal, navigate to the project folder, and run:_

```
dotnet restore
```
* _Then in terminal, run:_

```
dotnet run
```
* _Once the file is running, signaled by the notification in Terminal, you may locate the site at:_
* Localhost:5000 
* _In your browser of choice (Chrome is recommended) and navigate the Project  database._

### Technologies used
* C# 
* .NET Core App 1.0.4
* Mono
* Atom
* Visual Studio Code
* Git
* Github



*Copyright* _Hyewon Cho, Chris Crow, Meria Thomas, Regina Nurieva, and David Mortkowitz. 2018._