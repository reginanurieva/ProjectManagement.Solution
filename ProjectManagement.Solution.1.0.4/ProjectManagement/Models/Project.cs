using System;
using System.Collections.Generic;
using ProjectManagement;

namespace ProjectManagement.Models
{
  public class Project 
  {
    public int Id;
    public string Name;
    public string Content;
    public DateTime DueDate;
    public string Status;

    public Project(string name, string content, DateTime duedate, string status, int id = 0)
    {
      Id = id;
      Name = name;
      Content = content;
      DueDate = duedate;
      Status = status;
    }
  }
}