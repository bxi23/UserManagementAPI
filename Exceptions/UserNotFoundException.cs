using System;

namespace UserManagementAPI_TechHiveSolutionsLab.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userId)
            : base($"User with ID {userId} not found.")
        {
        }
    }
}