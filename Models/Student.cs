using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // for DataAnnotations
using System.ComponentModel.DataAnnotations.Schema; // for DataAnnotations


namespace CoreMVCWebApp4API5.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        //[Index]
        //[Index( "INDEX_STDNAME", IsClustered=true, IsUnique=true )]
        //[MaxLength(Int32.MaxValue, ErrorMessage = "More than max value")]
        //[StringLength(500, MinimumLength = 3, ErrorMessage = "Invalid Title Lenght")]
        public string StudentName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public decimal Height { get; set; }
        
        //[Range(10, 15)]
        public float Weight { get; set; }
        
//        [ForeignKey("GradeId")]
        //[ForeignKey]
        //[Column(Order =5)]
        //[Column("MyGrade", Order =5)]
        public int GradeId { get; set; }        // Foreign Key
        public Grade grade { get; set; }        // Foreign Key Class

        //[Required]
        //[EmailAddress]
        [NotMapped]
        public string MailingAddress { get; set; }

        // Any one only
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        public string Age;

    }

    public class Grade
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        // One - Many relationship
        public ICollection<Student> Students { get; set; }
    }


}