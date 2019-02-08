using System;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        [Key]
        public int Id { get; set; }

        public int InvaitedUserId { get; set; }
        public User InvaitedUser { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public bool IsActive { get; set; }
    }
}