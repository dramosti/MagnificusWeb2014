using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Comum.Infrastructure.Static
{
    public class UserData
    {
        /// <summary>
        /// Usuário que esta logado no sistema.
        /// </summary>
        public static int idUser = 2; //Provisório enquanto não eé implementada tela de login
        public static string xLogin;
        /// <summary>
        /// Usuário padrão da HLP
        /// </summary>
        public static int idUserHLP = 0;
        /// <summary>
        /// Usuário configurado para ser DEFAULT para a configuração de telas do sistema.
        /// </summary>
        public static int idUserDEFAULT = 0;
        public static int idGrupoUsuario;
        public static string xNome;
    }
}
