using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Team
    {
        public Team()
        {
            this.Members = new List<UserTeam>();
            this.Events = new List<TeamEvent>();
            this.Invitations = new List<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }


        public ICollection<UserTeam> Members { get; set; }
        public ICollection<TeamEvent> Events { get; set; }
        public ICollection<Invitation> Invitations { get; set; }

    }
}