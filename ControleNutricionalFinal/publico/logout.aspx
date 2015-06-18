<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="ControleNutricionalFinal.logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
            FormsAuthentication.SignOut();
            
            Response.Redirect("login.aspx");
  }
    
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
