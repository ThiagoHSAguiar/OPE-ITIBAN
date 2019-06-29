using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace controleestoque.Web.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string login, string senha)
        {
            var ret = false;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    string criptSenha = CriptoHelper.HashMD5(senha);
                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select count(*) from usuario where login = '{0}' and senha='{1}'",
                        login, criptSenha);
                    ret = ((int)comando.ExecuteScalar() > 0);
                }
            }
            return ret;

        }
    }
}