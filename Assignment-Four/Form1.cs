using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Assignment_Four
{
    public partial class StudentManage : Form
    {

        private string connectionString = "SERVER=LAB35\\SQL2012;DataBase=C0501L_WinForm;uid=lab;pwd=";
        private ErrorProvider nameErrorProvider;
        private ErrorProvider ageErrorProvider;
        private ErrorProvider emailErrorProvider;
        private ErrorProvider phoneErrorProvider;
        private ErrorProvider pictureErrorProvider;
        private Bitmap myImage;
        private DataTable dataTableAvailable;
        private DataTable dataTableSelected;
        
        public StudentManage()
        {
            InitializeComponent();

            // Create and set the ErrorProvider for each data entry control.
            nameErrorProvider = new ErrorProvider();
            nameErrorProvider.SetIconAlignment(txtName, ErrorIconAlignment.MiddleRight);
            nameErrorProvider.SetIconPadding(txtName, 2);
            nameErrorProvider.BlinkRate = 200;
            nameErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;


            ageErrorProvider = new ErrorProvider();
            ageErrorProvider.SetIconAlignment(nudAge, ErrorIconAlignment.MiddleRight);
            ageErrorProvider.SetIconPadding(nudAge, 2);
            ageErrorProvider.BlinkRate = 200;
            ageErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            emailErrorProvider = new ErrorProvider();
            emailErrorProvider.SetIconAlignment(txtEmail, ErrorIconAlignment.MiddleRight);
            emailErrorProvider.SetIconPadding(txtEmail, 2);
            emailErrorProvider.BlinkRate = 200;
            emailErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            phoneErrorProvider = new ErrorProvider();
            phoneErrorProvider.SetIconAlignment(txtPhone, ErrorIconAlignment.MiddleRight);
            phoneErrorProvider.SetIconPadding(txtPhone, 2);
            phoneErrorProvider.BlinkRate = 200;
            phoneErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            pictureErrorProvider = new ErrorProvider();
            pictureErrorProvider.SetIconAlignment(pictAvatar, ErrorIconAlignment.MiddleRight);
            pictureErrorProvider.SetIconPadding(pictAvatar, 2);
            pictureErrorProvider.BlinkRate = 200;
            pictureErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
        }

        private void StudentManage_Load(object sender, EventArgs e)
        {
            loadStudents();
            loadLanguage();
            loadSelected();
        }

        private void loadStudents()
        {
            string queryString = "SELECT StudentID, StudentName FROM Student";
            SqlConnection conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, conn);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            cboStudents.DataSource = dataTable;
            cboStudents.DisplayMember = "StudentName";
            cboStudents.ValueMember = "StudentID";
            conn.Close();
        }

        private void loadSelected()
        {
            // to do chua lam
        }

        private void loadLanguage()
        {
            string queryString = "SELECT * FROM Language";
            SqlConnection conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, conn);
            dataTableAvailable = new DataTable();
            dataAdapter.Fill(dataTableAvailable);
            lstAvailabel.DataSource = dataTableAvailable;
            lstAvailabel.DisplayMember = "LanguageName";
            lstAvailabel.ValueMember = "LanguageID";
            conn.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (cboStudents.Enabled)
            {
                cboStudents.Enabled = false;
                txtName.Focus();
                btnAddNew.Text = "Cancel";
                btnUpdate.Text = "Add";
                btnDelete.Enabled = false;
            }
            else
            {
                resetForm();
            }
        }

        private void resetForm()
        {
            cboStudents.Enabled = true;
            btnAddNew.Text = "Add New";
            btnUpdate.Text = "Update";
            btnDelete.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to exit?",
                "Student Manager",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboStudents.Enabled)
                {
                    // todo
                }
                else
                {
                    string queryString = "INSERT INTO Student (StudentName, Age, Email, Phone, ImagePath) VALUES (@StudentName, @Age, @Email, @Phone, @ImagePath);";
                    SqlConnection conn = new SqlConnection(connectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.Add("@StudentName", SqlDbType.NVarChar);
                    command.Parameters["@StudentName"].Value = txtName.Text;
                    command.Parameters.Add("@Age", SqlDbType.Int);
                    command.Parameters["@Age"].Value = nudAge.Value;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar);
                    command.Parameters["@Email"].Value = txtEmail.Text;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar);
                    command.Parameters["@Phone"].Value = txtPhone.Text;
                    command.Parameters.Add("@ImagePath", SqlDbType.NVarChar);
                    command.Parameters["@ImagePath"].Value = openFileDialog1.FileName;
                    command.Connection = conn;
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("RowsAffected {" + rowsAffected + "}");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "Pictures";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filePath = openFileDialog1.FileName;
                ShowMyImage(filePath, 100, 100);
            }
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            if (isNameValid())
            {
                nameErrorProvider.SetError(this.txtName, String.Empty);
            }
            else
            {
                nameErrorProvider.SetError(this.txtName, "Name is required.");
            }
        }

        private bool isAgeInValid()
        {
            return (this.nudAge.Value == 0);
        }

        private bool isNameValid()
        {
            return (this.txtName.Text.Length > 0);
        }

        private bool isAgeTooYoung()
        {
            return (this.nudAge.Value < 18);
        }

        private bool isAgeTooOld()
        {
            return (this.nudAge.Value > 30);
        }

        private void nudAge_Validated(object sender, EventArgs e)
        {
            
            if (isAgeInValid())
            {
                ageErrorProvider.SetError(this.nudAge, "Age is required.");
            } else if (isAgeTooYoung())
            {
                ageErrorProvider.SetError(this.nudAge, "Age not old enough. Age range come from 18 - 30");
            } else if (isAgeTooOld()) {
                ageErrorProvider.SetError(this.nudAge, "Age is too old. Age range come from 18 - 30");
            }
            else
            {
                ageErrorProvider.SetError(this.nudAge, String.Empty);
            }
        }

        private bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            if (emailAddress.Length == 0)
            {
                errorMessage = "Email address is required.";
                return false;
            }

            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "Email address must be valid email address format.\n" + 
                "For example 'someone@example.com'";
            return false;

        }

        private bool isPhoneValid()
        {
            return (this.txtPhone.Text.Length > 0);
        }

        private bool phoneIsNumeric()
        {
            double num;
            return double.TryParse(this.txtPhone.Text.Trim(), out num);
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(this.txtEmail.Text, out errorMsg)) {
                emailErrorProvider.SetError(this.txtEmail, errorMsg);
            } else 
            {
                emailErrorProvider.SetError(this.txtEmail, String.Empty);
            }
        }

        private void txtPhone_Validated(object sender, EventArgs e)
        {
            if (!isPhoneValid())
            {

                phoneErrorProvider.SetError(this.txtPhone, "Phone is required.");
            } else if (!phoneIsNumeric())
            {
                phoneErrorProvider.SetError(this.txtPhone, "Invalid phone number.");
            }
            else
            {
                phoneErrorProvider.SetError(this.txtPhone, String.Empty);
            }
        }

        private void pictAvatar_Validated(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName.Equals(""))
            {
                pictureErrorProvider.SetError(this.pictAvatar, "Avatar is required.");
            }
            else
            {
                pictureErrorProvider.SetError(this.pictAvatar, String.Empty);
            }
        }

        private void ShowMyImage(String fileToDisplay, int xSize, int ySize)
        {
            if (myImage != null)
            {
                myImage.Dispose();
            }
            pictAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            myImage = new Bitmap(fileToDisplay);
            pictAvatar.ClientSize = new Size(xSize, ySize);
            pictAvatar.Image = (Image)myImage;
        }

        private void lstAvailabel_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataTableAvailable.Rows.Count > 0)
            {
                //DataRow row = dataTableAvailable
            }
        }

    }
}
