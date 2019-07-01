using System.ComponentModel.DataAnnotations;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class User {
        [Autonumber]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Salt { get; set; }

        [Required, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }


        // EagerLoad Properties
        public virtual UserData UserData_Object { get; set; }
    }
}
