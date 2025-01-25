using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Index
{
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Poor Richard", 12);
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(212, 161, 228);
            this.dataGridView1.BackgroundColor = Color.FromArgb(212, 161, 228);
        }
        public MySqlConnection Conexao = null;
        public int selecaolinha = 0;
        public void Limpar()
        {
            textBox5.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
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
        public void Excluir()
        {
            try
            {
                Conectar();


                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index; //indice da linha selecionada,acho, não sei oq to fazendo
                    int celula1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                    string deletar = " delete from tbl_admin where id = @codigo ;";
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
        public void Gravar()
        {
            try
            {
                Conectar();

                if(textBox1.Text =="" || textBox2.Text == "") 
                { MessageBox.Show("Campos vazios"); }
                else { 
                string linha;
                if (selecaolinha == 0) { 
                linha = " insert into tbl_admin(cpf,nome,senha,salt) values (@cpf,@nome,@senha,@salt);";
                }
                else{                
                    linha = "update tbl_admin set cpf=@cpf,nome=@nome,master=null where id=@id;";
                }
                MySqlCommand query = new MySqlCommand(linha, Conexao);
               
                string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                string Hash = BCrypt.Net.BCrypt.HashPassword(textBox3.Text, salt);
                query.CommandType = CommandType.Text;
                if (selecaolinha != 0)
                {
                    int id = Convert.ToInt32(textBox5.Text);
                    query.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                }
                query.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = textBox1.Text;
                query.Parameters.Add("@nome", MySqlDbType.VarChar).Value = textBox2.Text;
                if(selecaolinha == 0) { 
                query.Parameters.Add("@senha", MySqlDbType.VarChar).Value = Hash;
                query.Parameters.Add("@salt", MySqlDbType.VarChar).Value = salt;
                }

                query.ExecuteNonQuery();
                selecaolinha = 0;

                MessageBox.Show("Dados inseridos");
                Limpar();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }

        }
        public void Carregar()
        {
            try
            {
                Conectar();
                string selecionar = "Select * from tbl_admin where cpf!= \"207.706.107-30\"; ";
                MySqlDataAdapter da = new MySqlDataAdapter(selecionar, Conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Senhas diferentes");
            }
            else
            {
                Gravar();
                Carregar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formFuncProd = new FormPrincipal();
            formFuncProd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formLogin = new LoginAdmin();
            formLogin.Show();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            Carregar();
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
                    string selecionar = "Select * from tbl_admin where nome like'" + search + "%' and cpf!= \"207.706.107-30\";;";
                    MySqlDataAdapter da = new MySqlDataAdapter(selecionar, Conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally { Conexao.Close(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Excluir();
            Carregar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try          //ALTERAR
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Index);
                    //Variaveis
                    int codigo = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);
                    var cpf = dataGridView1.Rows[rowIndex].Cells[1].Value;
                    var nome = dataGridView1.Rows[rowIndex].Cells[2].Value;
                    textBox5.Text=codigo.ToString();
                    textBox1.Text = cpf.ToString();
                    textBox2.Text = nome.ToString();
                    selecaolinha = 1;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
