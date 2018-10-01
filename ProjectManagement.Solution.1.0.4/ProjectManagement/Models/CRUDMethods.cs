using System;
using System.Collections.Generic;

namespace ProjectManagement.Models{
  public interface ICRUDMethods<T> {
    // Save
    void Save();
    
    // Update
    void Update(T newObject);

    // Delete
    void Delete(T toBeDeleted);
  }
}
