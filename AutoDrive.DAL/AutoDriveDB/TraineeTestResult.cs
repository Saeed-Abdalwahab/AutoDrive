using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public class TraineeTestResult
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }

        [Required]
        [ForeignKey("TraineeEvaluation")]
        public int TraineeEvaluationId { get; set; }

        [Required]
        public DateTime TestDate { get; set; }

        [Required]
        [ForeignKey("Test")]
        public int TestId { get; set; }

        [Required]
        [ForeignKey("TestResult")]
        public int TestResultId { get; set; }
       
        public Trainee Trainee { get; set; }

        public TraineeEvaluation TraineeEvaluation { get; set; }

        public Test Test { get; set; }

        public TestResult TestResult { get; set; }

    }
}
