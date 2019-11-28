using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CadastroCliente
{
    public partial class FrmCadastro : Form
    {
        public FrmCadastro()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomeCompleto.Text != string.Empty && txtCPF.Text != string.Empty
                    && txtData.Text != string.Empty && txtSexo.Text != string.Empty &&
                    txtTelefone.Text != string.Empty && txtEmail.Text != string.Empty
                    && txtStatus.Text != string.Empty)
                {

                    //Instancio o SqlConnection, passando como parâmetro a string de conexão ao banco
                    SqlConnection conn = new SqlConnection(@"Data Source=KEROLLYN\SQLEXPRESS01;Database=Cadastro;
                        Persist Security Info=True;User ID=sa;Password=Admin123");

                    //Instancio o SqlCommand, responsável pelas instruções SQL e
                    //Passo ao SqlCommand que a conexão que ele usará é o SqlConnection
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;

                    //No CommandText do SqlCommand, passo a instrução SQL referente a inserção dos dados
                    comm.CommandText = "INSERT INTO Clientes (Nome_Completo, Cpf, " +
                                       " Data_Nasc, Sexo, Telefone, Email,Endereco,Numero,Status,Nome_Mae,Nome_Pai) " +
                                       //Nos Values, passo os valores referentes aos controles digitados pelo usuário
                                       " VALUES (@Nome_Completo, @Cpf, @Data_Nasc, @Sexo, " +
                                       "         @Telefone, @Email, @Endereco,@Numero,@Status,@Nome_Mae,@Nome_Pai) ";

                    comm.Parameters.AddWithValue("@Nome_Completo", txtNomeCompleto.Text);
                    comm.Parameters.AddWithValue("@Cpf", txtCPF.Text);
                    comm.Parameters.AddWithValue("@Data_Nasc", txtData.Text);
                    comm.Parameters.AddWithValue("@Sexo", txtSexo.Text);
                    comm.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    comm.Parameters.AddWithValue("@Email", txtEmail.Text);
                    comm.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
                    comm.Parameters.AddWithValue("@Numero", txtNum.Text);
                    comm.Parameters.AddWithValue("@Nome_Mae", txtNomeMae.Text);
                    comm.Parameters.AddWithValue("@Nome_Pai", txtNomePai.Text);
                    comm.Parameters.AddWithValue("@Status", txtStatus.Text);

                    //Abro a conexão, uso o método ExecuteNonQuery e fecho a conexão
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();

                    //Exibo ao usuário a mensagem de inserção efetuada com sucesso
                    MessageBox.Show("Cadastro Realizado!", "Mensagem",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);                                      
                  
                }
                else
                {
                    MessageBox.Show("Informe os valores corretamente para completar o cadastro. ",
                     "Erro do Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeCompleto.Clear();
            txtCPF.Clear();
            txtData.Clear();
            txtEmail.Clear();
            txtEndereco.Clear();
            txtSexo.Clear();
            txtTelefone.Clear();
            txtNum.Clear();
        }
          

        //private void btnExportar_Click(object sender, EventArgs e)
        //{
        //    Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();

        //    if (dgvBancoDados.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            XcelApp.Application.Workbooks.Add(Type.Missing);
        //            for (int i = 1; i < dgvBancoDados.Columns.Count + 1; i++)
        //            {
        //                XcelApp.Cells[1, i] = dgvBancoDados.Columns[i - 1].HeaderText;
        //            }
        //            //
        //            for (int i = 0; i < dgvBancoDados.Rows.Count - 1; i++)
        //            {
        //                for (int j = 0; j < dgvBancoDados.Columns.Count; j++)
        //                {
        //                    XcelApp.Cells[i + 2, j + 1] = dgvBancoDados.Rows[i].Cells[j].Value.ToString();
        //                }
        //            }
        //            //
        //            XcelApp.Columns.AutoFit();
        //            //
        //            XcelApp.Visible = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Erro : " + ex.Message);
        //            XcelApp.Quit();
        //        }
        //    }
        //}

        private void btnBanco_Click(object sender, EventArgs e)
        {
            FrmBancoDados frmBanco = new FrmBancoDados();
            frmBanco.Show();           
        }
        
        private void FrmCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            var message = "Tem certeza que deseja encerrar a aplicação?";
            var result = MessageBox.Show(message, "Encerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}