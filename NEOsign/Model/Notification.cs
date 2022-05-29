using System.ComponentModel.DataAnnotations;

namespace NEOsign.Model
{
    public class Notification
    {
       
        
          
            public int Id { get; set; }
            public string Message { get; set; } = string.Empty;
            public User User { get; set; }
        public int UserId { get; set; }



    }
}
