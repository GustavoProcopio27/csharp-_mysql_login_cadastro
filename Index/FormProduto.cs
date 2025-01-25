using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Index
{
    public partial class FormProduto : Form
    {

        public FormProduto()
        {
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Poor Richard", 12);
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(212, 161, 228);
            this.dataGridView1.BackgroundColor = Color.FromArgb(212, 161, 228);

           



        }
        public MySqlConnection Conexao = null;
        public int SelecaoLinha = 0;
        private void FormProduto_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-M-dd";
            Carregar();
            CarregarCombo();
        }
        public void Limpar()
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            textBox2.Clear();
            textBox1.Clear();
            comboBox1.Text = "Escolha uma opção";
        }
        public void Fechar()
        {
            DialogResult result = MessageBox.Show("Realmente deseja sair", "Titulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) { Close(); }
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

        public void Cadastrar()
        {
            try
            {
                if (textBox2.Text == "" || comboBox1.Text == "Escolha uma opção")
                { MessageBox.Show("Campo vazio", "Vazio"); }
                else { 
                string linha;
                Conectar();
                if (SelecaoLinha == 0)
                {
                    linha = " insert into Tbl_venda(data,nome,quantidade,precoUnitario,precoTotal,formaPagamento) values (@data,@nome,@quantidade,@precoUnitario,@precoTotal,@formaPagamento);";
                }
                else
                {
                    linha = "update tbl_venda set data=@data,nome=@nome,quantidade=@quantidade,precoUnitario=@precoUnitario,precoTotal=@precoTotal,formaPagamento=@formaPagamento where codigo=@Codigo;";

                }
                MySqlCommand query = new MySqlCommand(linha, Conexao);

                query.CommandType = CommandType.Text;
                if (SelecaoLinha != 0)
                {
                    int codigueta = Convert.ToInt32(textBox1.Text);
                    query.Parameters.Add("@Codigo", MySqlDbType.Int32).Value = codigueta;
                    SelecaoLinha = 0;
                }
                query.Parameters.Add("@data", MySqlDbType.Date).Value = dateTimePicker1.Value;
                query.Parameters.Add("@nome", MySqlDbType.VarChar).Value = textBox2.Text;
                query.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = numericUpDown1.Value;
                query.Parameters.Add("@precoUnitario", MySqlDbType.Decimal).Value = Convert.ToDecimal(numericUpDown2.Value);
                decimal PrecoTotal = Convert.ToDecimal(numericUpDown1.Value) * Convert.ToDecimal(numericUpDown2.Value);



                query.Parameters.Add("@precoTotal", MySqlDbType.Decimal).Value = PrecoTotal;
                query.Parameters.Add("@formaPagamento", MySqlDbType.VarChar).Value = comboBox1.Text;

                query.ExecuteNonQuery();

                MessageBox.Show("Dados inseridos");
                Limpar();
                }
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
                    int celula1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);

                    string deletar = " delete from tbl_venda where codigo = @codigo ;";
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
                string selecionar = "Select * from tbl_venda";
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
                string sql = "select forma from FormaPagar";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, Conexao);

                DataTable dt = new DataTable();

                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dt.Rows[i]["forma"]);
                }
            }


            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }
            finally
            {
                Conexao.Close();
            }
        }

     

        private void button3_Click(object sender, EventArgs e)
        {
            Cadastrar();
            Carregar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fechar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Excluir();
            Carregar();
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
                    DateTime data = Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells[0].Value);
                    int codigo = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[1].Value);
                    textBox2.Text = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[2].Value);
                    int quantidade = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[3].Value);
                    decimal precoUni = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells[4].Value);
                    decimal preoTotal = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells[5].Value);
                    string pagamento = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[6].Value);

                    textBox1.Text = codigo.ToString();

                    numericUpDown1.Value = quantidade;
                    numericUpDown2.Value = precoUni;
                    dateTimePicker1.Value = data;
                    comboBox1.Text = pagamento;
                    SelecaoLinha = 1;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string search = textBox3.Text;
            try
            {
                Conectar();
                if (search == " " || search == null) { Carregar(); }
                else
                {
                    string selecionar = "Select * from tbl_venda where nome like'" + search + "%';";
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

