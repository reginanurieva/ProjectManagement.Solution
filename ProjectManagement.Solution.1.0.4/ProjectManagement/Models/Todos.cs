using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
  public class Item
  {
    private string _name;
    private string _status; 
    private int _id;

    public Item(string name, string status, int id = 0)
    {
      _name = name;
      _status = status; 
      _id = id;
    }

    public override bool Equals(System.Object otherStatus)
    {
      if(!(otherStatus is Item))
      {
        return false;
      }
      else
      {
        Item newStatus = (Item) otherStatus;
        bool idEquality = this.GetId() == newStatus.GetId();
        bool statusEquality = this.GetStatus() == newStatus.GetStatus();
        bool nameEquality = this.GetStatus() == newStatus.GetStatus();
        return (idEquality && statusEquality && statusEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetStatus().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }
    public string GetStatus()
    {
      return _status;
    }
    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
     MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (status) VALUES (@status);";

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@status";
      status.Value = this._name;
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public void AddCategory(Category newCategory)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"INSERT INTO todo (status_id, name_id) VALUES (@statusId, @nameId);";

    //   MySqlParameter status_id = new MySqlParameter();
    //   status_id.ParameterName = "@statusId";
    //   status_id.Value = newCategory.GetId();
    //   cmd.Parameters.Add(status_id);

    //   MySqlParameter name_id = new MySqlParameter();
    //   name_id.ParameterName = "@nameId";
    //   name_id.Value = _id;
    //   cmd.Parameters.Add(name_id);

    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }

    // public List<Status> GetCategories()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT status_id FROM categories_items WHERE item_id = @itemId;";

    //   MySqlParameter itemIdParameter = new MySqlParameter();
    //   itemIdParameter.ParameterName = "@itemId";
    //   itemIdParameter.Value = _id;
    //   cmd.Parameters.Add(itemIdParameter);

    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;

    //   List<int> categoryIds = new List<int> {};
    //   while(rdr.Read())
    //   {
    //     int categoryId = rdr.GetInt32(0);
    //     categoryIds.Add(categoryId);
    //   }

    //   List<Category> categories = new List<Category> {};
    //   foreach (int categoryId in categoryIds)
    //   {
    //     var categoryQuery = conn.CreateCommand() as MySqlCommand;
    //     categoryQuery.CommandText = @"Select * FROM categories WHERE id = @categoryId;";

    //     MySqlParameter categoryIdParameter = new MySqlParameter();
    //     categoryIdParameter.ParameterName = "@categoryId";
    //     categoryIdParameter.Value = categoryId;
    //     categoryQuery.Parameters.Add(categoryIdParameter);

    //     var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
    //     while(categoryQueryRdr.Read())
    //     {
    //       int thisCategoryId = categoryQueryRdr.GetInt32(0);
    //       string categoryName = categoryQueryRdr.Getstring(1);
    //       Category foundCategory = new Category(categoryName, thisCategoryId);
    //       categories.Add(foundCategory);
    //     }
    //     categoryQueryRdr.Dispose();
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return categories;


    // }
    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int nameId = rdr.GetInt32(0);
        string nameDescription = rdr.GetString(1);
        string statusDescription = rdr.GetString(2);

        Item newName = new Item(nameDescription, statusDescription, nameId);
        allItems.Add(newName);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    
    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemName = "";
      string itemStatus = "";

      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemName = rdr.GetString(1);
        itemStatus = rdr.GetString(2);
        
      }
      Item newItem = new Item(itemName, itemStatus, itemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newItem;
    }
    public void UpdateDescription (string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET status = @newDescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _name = newDescription;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @itemId;";

      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@itemId";
      itemIdParameter.Value = this.GetId();
      cmd.Parameters.Add(itemIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
