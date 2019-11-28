using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CadastroCliente
{
    public partial class FrmBancoDados : Form
    {
        public FrmBancoDados()
        {
            InitializeComponent();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();


            if (dgvBancoDados.Rows.Count > 0)
            {
                try
                {
                    XcelApp.Application.Workbooks.Add(Type.Missing);
                    for (int i = 1; i < dgvBancoDados.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dgvBancoDados.Columns[i - 1].HeaderText;
                    }
                    //
                    for (int i = 0; i < dgvBancoDados.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvBancoDados.Columns.Count; j++)
                        {
                            XcelApp.Cells[i + 2, j + 1] = dgvBancoDados.Rows[i].Cells[j].Value.ToString();
                            
                        }
                    }
                    //
                    XcelApp.Columns.AutoFit();
                    XcelApp.Columns["C:D"].Delete();//Deleta coluna
                    XcelApp.Columns["G:H"].Delete();//Deleta coluna
                    XcelApp.Columns["I:J"].Delete();//Deleta coluna
                    //
                    XcelApp.Visible = true;                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    XcelApp.Quit();
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Instancio o SqlConnection, passando como parâmetro a string de conexão ao banco
            string connectionString = @"Data Source=KEROLLYN\SQLEXPRESS01;Initial Catalog=Cadastro;
                        Persist Security Info=True;User ID=sa;Password=Admin123";

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Clientes", sqlCon);
                System.Data.DataTable dtbl = new System.Data.DataTable();
                sqlDa.Fill(dtbl);

                dgvBancoDados.DataSource = dtbl;

            }
        }

        private void dgvBancoDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            dgvBancoDados.Rows.RemoveAt(dgvBancoDados.CurrentRow.Index);
               
        }
    }
}
