using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            this.CreatedEvents =new List<Event>();
            this.UserTeams = new List<UserTeam>();
            this.UserCreatedTeams =new List<UserTeam>();
            this.RecievedInvitations = new List<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }
        
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }


        public GenderEnum Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }


        public ICollection<UserTeam> UserCreatedTeams { get; set; }


        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Invitation> RecievedInvitations { get; set; }

    }
}
