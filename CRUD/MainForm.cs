using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class MainForm : Form
    {
        private StudentsContext context;

        public MainForm()
        {
            InitializeComponent();
            context = new StudentsContext();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MainForm_Load(this, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Pobieramy dane z bazy danych i przypisujemy je do widoku
            dataGridView1.DataSource = context.Students.OrderBy(s => s.Name).ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Pobramy dane z formularza
            string name = textBoxName.Text;
            DateTime date = dateTimePickerDate.Value;
            string attitude = comboBoxAttitude.SelectedItem?.ToString();

            // Nowy obiekt Student
            Student student = new Student
            {
                Name = name,
                Date = date,
                Attitude = attitude
            };

            // Dodanie studenta do bazy danych
            context.Students.Add(student);
            context.SaveChanges();

            // Nazwiska alfabetycznie
            dataGridView1.DataSource = context.Students.OrderBy(s => s.Name).ToList();

            // Wyczyszczenie kontrolek formularza
            textBoxName.Text = string.Empty;
            dateTimePickerDate.Value = DateTime.Now;
            comboBoxAttitude.SelectedIndex = -1;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                // Pobranie identyfikatora zaznaczonego studenta
                int studentId = (int)dataGridView1.Rows[selectedRowIndex].Cells["ColumnID"].Value;
                Student student = context.Students.Find(studentId);

                if (student != null)
                {
                    // Pobranie zaktualizowanych danych z formularza
                    string name = textBoxName.Text;
                    DateTime date = dateTimePickerDate.Value;
                    string attitude = comboBoxAttitude.SelectedItem?.ToString();

                    // Aktualizacja właściwości studenta
                    student.Name = name;
                    student.Date = date;
                    student.Attitude = attitude;

                    // Zapis zmian do bazy danych
                    context.SaveChanges();

                    
                    dataGridView1.DataSource = context.Students.OrderBy(s => s.Name).ToList();

                   
                    textBoxName.Text = string.Empty;
                    dateTimePickerDate.Value = DateTime.Now;
                    comboBoxAttitude.SelectedIndex = -1;
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

               
                int studentId = (int)dataGridView1.Rows[selectedRowIndex].Cells["ColumnID"].Value;
                Student student = context.Students.Find(studentId);

                if (student != null)
                {
                    // Usunięcie studenta z bazy danych
                    context.Students.Remove(student);
                    context.SaveChanges();

                    // Odświeżenie danych w DataGridView
                    dataGridView1.DataSource = context.Students.OrderBy(s => s.Name).ToList();



                }
            }
        }
    }
}