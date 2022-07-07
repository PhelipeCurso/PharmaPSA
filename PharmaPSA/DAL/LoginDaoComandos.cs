using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaPSA.DAL
{
    class LoginDaoComandos
    {
        public bool tem = false;
        public string mensagem = "";       
        SqlCommand cmd = new SqlCommand();
        Conexao con = new Conexao();
        SqlDataReader dr;

        public bool verificarLogin (String login, String senha)
        {
            //comando sql para verificar se tem no banco de dados os dados.

            cmd.CommandText = "select * from Usuarios where email = @login and senha = @senha";
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha", senha);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    tem = true;
                }
                con.desconectar();
                dr.Close();
            }
            catch (SqlException)
            {

                this.mensagem = "Erro com o Banco de dados.";
            }

            
            return tem;
        }
        public string cadastrar(string email, string senha, string confSenha)
        {
            tem =false;
            // comandos para inserir os dados no banco de dados.
            if (senha.Equals(confSenha))
            {
                cmd.CommandText = "insert into Usuarios values(@e,@s); ";
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@s", senha);
                try
                {
                    cmd.Connection = con.Conectar();
                    cmd.ExecuteNonQuery();
                    con.desconectar();
                    this.mensagem = "Casdastrado com sucesso!";
                    tem=true;
                }
                catch (SqlException)
                {
                    this.mensagem = "Erro com o Banco de Dados";
                   
                }
            }
            else
            {
                this.mensagem = "Senhas não correspondem !";
            }
            
            return mensagem;
        }
    }
}
