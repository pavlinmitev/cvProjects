namespace VaporStore.DataProcessor
{
	using System;
    using System.Linq;
    using Data;
    using VaporStore.Data.Models;

    public static class Bonus
	{
		public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
		{
            User user = context.Users.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                if (context.Users.Any(x=>x.Email==newEmail))
                {
                    return $"Email {newEmail} is already taken";
                }
                else
                {
                    user.Email = newEmail;
                    
                    context.SaveChanges();
                  
                    return $"Changed {username}'s email successfully";

                }
            }
            else
            {
                return $"User {username} not found";
            }
        }
	}
}
