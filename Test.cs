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
    public partial class Test : Form
    {
        List<Question> question = new List<Question>();
        static int number=0;
        public Test(List<Question> _question)
        {
            InitializeComponent();
            question = _question;
            number = 0;
        }

        Label errorR = new Label();
        Label name = new Label();
        Button ans = new Button();
        Label quest = new Label();
        Button next = new Button();
        CheckedListBox answers;
        private void Test_Load(object sender, EventArgs e)
        {
            errorR.Name = "errorR";
            errorR.Text = "";

            next.Name = "next";
            next.Location = new Point(450, 33);
            next.Text = "Далее";
            next.Enabled = false;
            next.Click += new System.EventHandler(this.Next);

            name.Name = "name";
            name.Location = new Point(25, 32);
            name.Font = new Font("TimesNewRoman", 16);
            name.Text = "Вопрос #"+(number+1);
            name.Size = new Size(name.PreferredWidth, name.PreferredHeight);

            ans.Name = "ans";
            ans.Location = new Point(175, 33);
            ans.Text = "Ответить";
            ans.Click += new System.EventHandler(this.validateUserEntry);

            quest.Name = "quest";
            quest.AutoSize = false;
            quest.Location = new Point(25, 70);
            quest.Font = new Font("TimesNewRoman", 12);
            string qu = question[number].question;
            qu = qu.Replace('\\', '\n');
            quest.Text = qu;
            quest.Size = new Size(quest.PreferredWidth, quest.PreferredHeight);

            int n = question[number].question.Split('\\').Length-1;

            //Ответы
            answers = new CheckedListBox();
            answers.Location = new Point(25, 100+(24*n));
            answers.Font = new Font("TimesNewRoman", 12);
            answers.Size = new System.Drawing.Size(1000, question[number].answers.Count * 25);
            for (int i=0;i<question[number].answers.Count;i++)
            {
                answers.Items.Add(question[number].answers[i]);
            }

            Controls.Add(name);
            Controls.Add(quest);
            Controls.Add(answers);
            Controls.Add(ans);
            Controls.Add(next);
        }

        private void validateUserEntry(object sender, EventArgs e)
        {
            errorR.Location = new Point(260, 35);
            errorR.Font = new Font("TimesNewRoman", 10);
            if (answers.CheckedItems.Count > 1)
            {
                errorR.Text = "Слишком много ответов";
                errorR.ForeColor = Color.FromName("Red");
            }
            else if(answers.CheckedItems.Count == 1)
            {
                if (answers.CheckedItems[0].ToString() == question[number].t)
                {
                    errorR.Text = "Правильно!";
                    errorR.ForeColor = Color.FromName("Green");
                    number++;
                    ans.Enabled = false;
                }
                else
                {
                    errorR.Text = "Неправильно";
                    errorR.ForeColor = Color.FromName("Red");
                }
            }
            errorR.Size = new Size(errorR.PreferredWidth, errorR.PreferredHeight);
            Controls.Add(errorR);

            if (errorR.Text == "Правильно!")
            {
                next.Enabled = true;
            }
        }

        private void Next(object sender, EventArgs e)
        {
            if (errorR.Text == "Правильно!")
            {
                if (number >= question.Count)
                {
                    MessageBox.Show("Тест пройден!");
                    this.Hide();
                    //Application.Exit();
                    Environment.Exit(0);
                }
                NewQuestion();
            }
            next.Enabled = false;
        }
        private void NewQuestion()
        {
            Controls.Clear();

            errorR.Text = "";

            //next.Location = new Point(450, 33);
            //next.Text = "Далее";
            next.Enabled = false;

            name.Location = new Point(25, 32);
            //name.Font = new Font("TimesNewRoman", 16);
            name.Text = "Вопрос #" + (number + 1);
            name.Size = new Size(name.PreferredWidth, name.PreferredHeight);

            //ans.Location = new Point(175, 33);
            ans.Enabled = true;
            //ans.Text = "Ответить";

            //quest.Location = new Point(25, 70);
            //quest.AutoSize = false;
            //quest.Location = new Point(25, 70);
            //quest.Font = new Font("TimesNewRoman", 12);
            string qu = question[number].question;
            qu = qu.Replace('\\', '\n');
            quest.Text = qu;
            quest.Size = new Size(quest.PreferredWidth, quest.PreferredHeight);

            int n = question[number].question.Split('\\').Length - 1;

            //Ответы
            answers = new CheckedListBox();
            answers.Location = new Point(25, 100+(24*n));
            answers.Font = new Font("TimesNewRoman", 12);
            answers.Size = new System.Drawing.Size(1000, question[number].answers.Count * 25);
            for (int i = 0; i < question[number].answers.Count; i++)
            {
                answers.Items.Add(question[number].answers[i]);
            }

            Controls.Add(name);
            Controls.Add(quest);
            Controls.Add(answers);
            Controls.Add(ans);
            Controls.Add(next);
        }
    }
}
