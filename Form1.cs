using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testsInHistory
{
    public struct Question
    {
        public string question { get; set; }
        public List<string> answers;
        public string t;

        public Question(string _question, List<string> _answers)
        {
            question = _question;
            t = "0";
            answers = new List<string>();

            for (int i = 0; i < _answers.Count; i++)
            {
                if (_answers[i][0] == '\\')
                {
                    _answers[i] = _answers[i].TrimStart('\\');
                    t = _answers[i];
                }
                answers.Add(_answers[i]);
            }
            if (t == "0") throw new Exception("t не определено");
            Shuffle(answers);
        }

        public static void Shuffle(List<string> arr)
        {
            Random rand = new Random();

            for (int i = arr.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                string tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }
    }
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        string[] lines;
        List<int> newQuestion = new List<int>();
        List<Question> question = new List<Question>();
        Test Test;
        private void StartForm_Load(object sender, EventArgs e)
        {}

        public static void Shuffle(List<int> arr)
        {
            Random rand = new Random();

            for (int i = arr.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            if(!System.IO.File.Exists(textBox1.Text))
            {
                MessageBox.Show("Файл не найден");
                Random r = new Random();
                if(r.Next()%2==1)
                    MessageBox.Show("Возможно, файл с тестами не находится в одной папке с этим приложением.");
                this.Close();
            }
            if (textBox1.Text.Substring(textBox1.Text.Length - 4) != ".txt")
            {
                MessageBox.Show("Формат файла не соответствует требуемому");
                this.Close();
            }
            lines = System.IO.File.ReadAllLines(textBox1.Text);
            if (lines.Length == 0|| lines[0] != "NewQuestion" || lines[lines.Length - 1] != "EndQuestions")
            {
                MessageBox.Show("Файл с тестами некорректен");
                this.Close();
            }
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "NewQuestion")
                    newQuestion.Add(i + 1);
            }
            Shuffle(newQuestion);

            for (int i = 0; i < newQuestion.Count; i++)
            {
                Question quest;
                string questquest = lines[newQuestion[i]];
                List<string> answers = new List<string>();
                int j = newQuestion[i] + 1;
                while (lines[j] != "NewQuestion" && lines[j] != "EndQuestions")
                {
                    answers.Add(lines[j]);
                    j++;
                }
                quest = new Question(questquest, answers);
                question.Add(quest);
            }

            Test = new Test(question);

            Test.Show();
        }
    }
}
