using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hangman.Domain
{
    public class UserLeave
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
