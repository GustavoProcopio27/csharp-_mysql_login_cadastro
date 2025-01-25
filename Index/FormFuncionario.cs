using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Index
{
    public partial class FormFuncionario : Form
    {
        public MySqlConnection Conexao = null;
        public int SelecaoLinha = 0;
        public int Indice = 0;

        public void Limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "Escolha uma opção";
        }

        public void Conectar()
        {
            try
            {
                Conexao = new MySqlConnection("server=localhost;database=banco_cadastro;uid=root;pwd=Cefet123");
                Conexao.Open();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Conexao nao estabelecida"); }
        }

        public FormFuncionario()
        {
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Poor Richard", 12);
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(212, 161, 228);
            this.dataGridView1.BackgroundColor = Color.FromArgb(212, 161, 228);

        }

        public void Gravar()
        {
            try
            {
                Conectar();
                 
                string linha;
                if (SelecaoLinha == 0)
                {
                    linha = " insert into Tbl_Funcionario(Nome,Endereco,Celular,Email,Funcao_Funcionario) values (@Nome,@Endereco,@Celular,@Email,@Funcao_Funcionario);";
                }
                else
                {
                    linha = "update Tbl_Funcionario set Nome=@Nome,Celular=@Celular,Endereco=@Endereco,Email=@Email,Funcao_Funcionario=@Funcao_Funcionario where codigo=@Codigo;";
                    int Codigo = Convert.ToInt32(textBox5.Text);
                }
                MySqlCommand query = new MySqlCommand(linha, Conexao);

                query.CommandType = CommandType.Text;
                if (SelecaoLinha != 0)
                {
                    query.Parameters.Add("@Codigo", MySqlDbType.Int32).Value = textBox5.Text;
                }
                query.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = textBox1.Text;
                query.Parameters.Add("@Celular", MySqlDbType.VarChar).Value = textBox2.Text;
                query.Parameters.Add("@Endereco", MySqlDbType.VarChar).Value = textBox3.Text;
                query.Parameters.Add("@Email", MySqlDbType.VarChar).Value = textBox4.Text;
                query.Parameters.Add("@Funcao_Funcionario", MySqlDbType.VarChar).Value = comboBox1.Text;

                query.ExecuteNonQuery();
                SelecaoLinha = 0;

                MessageBox.Show("Dados inseridos");
                Limpar();
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }

        }
        public void Excluir()
        {
            try
            {
                Conectar();


                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index; //indice da linha selecionada,acho, não sei oq to fazendo
                    int celula1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                    string deletar = " delete from Tbl_Funcionario where codigo = @codigo ;";
                    MySqlCommand query = new MySqlCommand(deletar, Conexao);

                    query.CommandType = CommandType.Text;

                    query.Parameters.Add("@codigo", MySqlDbType.Int32).Value = celula1;

                    query.ExecuteNonQuery();




                    dataGridView1.Refresh();
                    dataGridView1.Update();
                    MessageBox.Show("linha excluida");
                }
                else
                {
                    MessageBox.Show("selecione uma linha para apagar");
                }
            }//fim do try
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }
        }

        public void Carregar()
        {
            try
            {
                Conectar();
                string selecionar = "Select * from Tbl_Funcionario";
                MySqlDataAdapter da = new MySqlDataAdapter(selecionar, Conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }
        }
        public void CarregarCombo()
        {
            try
            {
                Conectar();
                string sql = "select Funcao from Tbl_Funcao";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, Conexao);

                DataTable dt = new DataTable();

                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dt.Rows[i]["funcao"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally
            {
                Conexao.Close();
            }
        }



        public void Sair()
        {
            DialogResult result = MessageBox.Show("Realmente deseja sair", "Titulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) { Close(); }

        }





        private void button1_Click(object sender, EventArgs e)
        { //limpar
            Limpar();
        }


        private void button2_Click(object sender, EventArgs e)
        { //fechar
            Sair();

        }

        private void button3_Click(object sender, EventArgs e)
        { //Cadastro
            Gravar();
            Carregar();
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void FormFuncionario_Load_1(object sender, EventArgs e)
        {
            Carregar();
            CarregarCombo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Excluir();
            Carregar();
            dataGridView1.Update();
            dataGridView1.Refresh();

        }



        private void button5_Click(object sender, EventArgs e)
        {
            //Alterar
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Index);
                    //Variaveis
                    int codigo = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);
                    var nome = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[1].Value);
                    var endereco = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[2].Value);
                    var celular = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[3].Value);
                    var email = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[4].Value);
                    var funcao = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[5].Value);

                    textBox1.Text = nome;
                    textBox2.Text = celular;
                    textBox3.Text = endereco;
                    textBox4.Text = email;
                    textBox5.Text = codigo.ToString();
                    comboBox1.Text = funcao;
                    SelecaoLinha = 1;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string search = textBox6.Text;
            try
            {
                Conectar();
                if (search == " " || search == null) { Carregar(); }
                else
                {
                    string selecionar = "Select * from tbl_Funcionario where Nome like'" + search + "%';";
                    MySqlDataAdapter da = new MySqlDataAdapter(selecionar, Conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }
        }
    }
}