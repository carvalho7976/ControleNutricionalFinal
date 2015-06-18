using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleNutricionalFinal {
    public partial class login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void LogIn(object sender, EventArgs e) {
            if (IsValid) {
                String usuario = Email.Text;
                String senha = Password.Text;

                if (usuario.Equals("rogerio@mail.com") && senha.Equals("12345")) {
                    //pra lembrar do usario
                    FormsAuthentication.RedirectFromLoginPage(usuario, false);
                }
                else {
                    FailureText.Text = "Login ou senha invalido";
                    ErrorMessage.Visible = true;
                }

            }
            
        }
    }
}