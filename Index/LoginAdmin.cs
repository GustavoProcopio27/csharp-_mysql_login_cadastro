using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
namespace Index
{
    public partial class LoginAdmin : Form
    {
        public LoginAdmin()
        {
            InitializeComponent();
        }
        public MySqlConnection Conexao = null;
        public void Conectar()
        {
            try
            {
                Conexao = new MySqlConnection("server=localhost;database=banco_cadastro;uid=root;pwd=Cefet123");
                Conexao.Open();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Conexao nao estabelecida"); }
        }
        public void Logar()
        {
            try
            {
                Conectar();

                string buscar = "SELECT * from tbl_admin where cpf='" + textBox8.Text + "'";
                MySqlCommand query = new MySqlCommand(buscar, Conexao);

                query.CommandType = CommandType.Text;
                MySqlDataReader sqlReader = query.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        if ("207.706.107-30" == textBox8.Text && textBox7.Text=="gustavo" && BCrypt.Net.BCrypt.Verify(textBox5.Text, sqlReader.GetString(3).ToString()))
                        {
                            var FormCadastrarAdmin = new FormUsuarios();
                            FormCadastrarAdmin.Show();
                            this.Hide();


                        }
                        else
                        {
                            if (BCrypt.Net.BCrypt.Verify(textBox5.Text, sqlReader.GetString(3).ToString()) && textBox7.Text==sqlReader.GetString(2).ToString() )
                            {
                                var FormPrinc = new FormPrincipal();
                                FormPrinc.Show();
                                this.Hide();

                            }
                            else { MessageBox.Show("Nome ou senha incorretos","Erro"); }
                        }

                    }
                }
                else { MessageBox.Show("Sem linhas encontradas", "no lines"); }




            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro"); }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            Logar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
