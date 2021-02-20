using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AirTrack.Entity.Base
{
    public class AuiditEntity
    {
        public AuiditEntity()
        {
            
            IsActive = true;
            IsDeleted = false;
        }
        
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public DateTime InsertedDate { get; set; }

      
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

    }
}
