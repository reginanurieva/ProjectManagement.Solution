using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;

namespace ProjectManagement.Tests
{
  [TestClass]
  public class ProjectTests : IDisposable
  {
    public void Dispose()
    {
      Project.DeleteAll();
      User.DeleteAll();
      Todo.DeleteAll();
      Tag.DeleteAll();
    }

    public ProjectTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
    }

    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Project.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }


    [TestMethod]
    public void Equals_ReturnsTrueIfProjectsAreTheSame_Project()
    {
      // Arrange, Act
      DateTime time = new DateTime(1234, 12, 23);
      Project firstProject = new Project("Planner", "content", time, "Done", 1);
      Project secondProject = new Project("Planner", "content", time, "Done", 1);

      // Assert
      Assert.AreEqual(firstProject, secondProject);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ProjectsList()
    {
      //Arrange
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done", 1);

      //Act
      testProject.Save();
      List<Project> result = Project.GetAll();
      List<Project> testList = new List<Project>{testProject};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnAllProjects_ProjectsList()
    {
      //Arrange
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();
      Project newProject = new Project("Wedding Planner", "content", newDateTime, "Done");
      newProject.Save();
      List <Project> expectedProjects = new List <Project> {testProject, newProject};

      //Act
      List <Project> allProjects = Project.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedProjects, allProjects);
    }

    [TestMethod]
    public void Find_FindsProjectInDB_Project()
    {
      //Arrange
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done", 1);
      testProject.Save();

      //Act
      Project foundProject = Project.Find(testProject.Id);

      //Assert
      Assert.AreEqual(testProject, foundProject);
    }

    [TestMethod]
    public void Update_UpdatesProjectInDB_String()
    {
      //Arrange
      DateTime testDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", testDateTime, "Done");
      testProject.Save();
      DateTime newDateTime = new DateTime(2018, 6, 7);
      Project newProject = new Project("Wedding Planner", "new content", newDateTime, "In Progress");
      newProject.Save();
      newProject.Id = testProject.Id;

      //Act
      testProject.Update(newProject);

      //Assert
      Assert.AreEqual(newProject, testProject);
    }

    [TestMethod]
    public void Delete_DeleteProjectFromDB()
    {
      //Arrange
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();
      Project newProject = new Project("Wedding Planner", "content", newDateTime, "Done");
      newProject.Save();
      List <Project> expectedProjects = new List <Project> {newProject};

      //Act
      testProject.Delete();
      List <Project> actualProjects = Project.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedProjects, actualProjects);
    }

    [TestMethod]
    public void DeleteAll_DeleteAllProjectsFromDB()
    {
      //Arrange
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();
      Project newProject = new Project("Wedding Planner", "content", newDateTime, "Done");
      newProject.Save();
      List <Project> expectedProjects = new List <Project> {};

      //Act
      Project.DeleteAll();
      List <Project> actualProjects = Project.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedProjects, actualProjects);
    }

    [TestMethod]
    public void GetUsers_GetAllAssignedUsers_List()
    {
      //Arrange
      User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
      user1.Save();
      User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
      user2.Save();
      List <User> expectedUsers = new List<User>{user1, user2};
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      currentProject.AddUser(user1);
      currentProject.AddUser(user2);

      //Act
      List <User> users = currentProject.GetUsers();

      //Assert
      CollectionAssert.AreEqual(expectedUsers, users);
    }

    [TestMethod]
    public void AddUser_AssignUserToProject()
    {
      //Arrange
      User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
      user1.Save();
      User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
      user2.Save();
      List <User> expectedUsers = new List<User>{user1, user2};
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();

      //Act
      currentProject.AddUser(user1);
      currentProject.AddUser(user2);
      List <User> users = currentProject.GetUsers();

      //Assert
      CollectionAssert.AreEqual(expectedUsers, users);
    }

    [TestMethod]
    public void AddTodo_MakeNewTodo()
    {
      //Arrange
      Todo todo1 = new Todo("todo1", "Todo");
      todo1.Save();
      Todo todo2 = new Todo("todo2", "Done");
      todo2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "In Progress");
      currentProject.Save();
      List <Todo> expectedTodos = new List<Todo>{todo1, todo2};

      //Act
      currentProject.AddTodo(todo1);
      currentProject.AddTodo(todo2);
      List <Todo> todos = currentProject.GetTodos();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void GetTodos_GetAllTodosInProject_List()
    {
      //Arrange
      Todo todo1 = new Todo("todo1", "Todo");
      todo1.Save();
      Todo todo2 = new Todo("todo2", "Done");
      todo2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      List <Todo> expectedTodos = new List<Todo>{todo1, todo2};
      currentProject.AddTodo(todo1);
      currentProject.AddTodo(todo2);

      //Act
      List <Todo> todos = currentProject.GetTodos();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void GetTags_GetAllTagsOfProject_List()
    {
      //Arrange
      Tag tag1 = new Tag("#Hyewon");
      tag1.Save();
      Tag tag2 = new Tag("#Cho");
      tag2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      currentProject.AddTag(tag1);
      currentProject.AddTag(tag2);
      List <Tag> expectedTags = new List <Tag> {tag1, tag2};

      //Act
      List<Tag> tags = currentProject.GetTags();

      //Assert
      CollectionAssert.AreEqual(expectedTags, tags);
    }

    [TestMethod]
    public void AddTag_AddNewTag()
    {
      //Arrange
      Tag tag1 = new Tag("#Hyewon");
      tag1.Save();
      Tag tag2 = new Tag("#Cho");
      tag2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      List <Tag> expectedTags = new List <Tag> {tag1, tag2};

      //Act
      currentProject.AddTag(tag1);
      currentProject.AddTag(tag2);
      List<Tag> tags = currentProject.GetTags();

      //Assert
      CollectionAssert.AreEqual(expectedTags, tags);
    }

    [TestMethod]
    public void DeleteTags_DeleteAllTags()
    {
      //Arrange
      Tag tag1 = new Tag("#Hyewon");
      tag1.Save();
      Tag tag2 = new Tag("#Cho");
      tag2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      currentProject.AddTag(tag1);
      currentProject.AddTag(tag2);
      List <Tag> expectedTags = new List <Tag> {};
      
      //Act
      currentProject.DeleteTags();
      List<Tag> tags = currentProject.GetTags();

      //Assert
      CollectionAssert.AreEqual(expectedTags, tags);
    }

    [TestMethod]
    public void UpdateTags_UpdateTagsWithNewTags()
    {
      //Arrange
      Tag tag1 = new Tag("#tag1");
      tag1.Save();
      Tag tag2 = new Tag("#tag2");
      tag2.Save();
      Tag tag3 = new Tag("#tag3");
      tag3.Save();
      Tag tag4 = new Tag("#tag4");
      tag4.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      currentProject.AddTag(tag1);
      currentProject.AddTag(tag2);
      List <Tag> expectedTags = new List <Tag> {tag3, tag4};
      
      //Act
      currentProject.UpdateTags(expectedTags);
      List<Tag> tags = currentProject.GetTags();

      //Assert
      CollectionAssert.AreEqual(expectedTags, tags);
    }

    [TestMethod]
    public void AssignOwner_SetOwnerOfProject()
    {
      //Arrange
      User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
      user1.Save();
      User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
      user2.Save();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project currentProject = new Project("Current Project", "Project content", newDateTime, "Done");
      currentProject.Save();
      currentProject.AddUser(user1);
      currentProject.AddUser(user2);

      //Act
      currentProject.AssignOwner(user1);
      User user = currentProject.GetOwner();

      //Assert
      Assert.AreEqual(user1, user);
    }

    [TestMethod]
    public void GetOwner_GetOwnerOfProject_User()
    {
      //Arrange
      User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
      user1.Save();
      User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
      user2.Save();
      Project currentProject = new Project("Current Project", "Project content", DateTime.Now, "Done");
      currentProject.Save();
      currentProject.AddUser(user1);
      currentProject.AddUser(user2);
      currentProject.AssignOwner(user1);

      //Act
      User user = currentProject.GetOwner();

      //Assert
      Assert.AreEqual(user1, user);
    }
  }
}
