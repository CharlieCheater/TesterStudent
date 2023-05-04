using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterStudent.Client.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool HasMultipleCorrectAnswers { get; set; }
        public List<Answer> Answers { get; set; }
        public int SelectedAnswer { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
