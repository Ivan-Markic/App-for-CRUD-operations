<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserWebApp.Forms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logiranje</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
</head>
<body class="container">
    <div class="row">
        <div class="col-4"></div>
        <div class="col-4">
            <form class="row gap-2" runat="server">
                <div>
                    <asp:Label class="form-label" runat="server">Username</asp:Label>
                    <asp:TextBox ID="tbUsername" runat="server" type="text" class="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="Polje username je obavezno" Display="None" ControlToValidate="tbUsername" runat="server" />
                </div>
                <div>
                    <asp:Label runat="server" class="form-label">Password</asp:Label>
                    <asp:TextBox ID="tbPassword" runat="server" type="password" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="tbPassword" Display="None"
                        ErrorMessage="Niste upisali lozinku" />
                </div>
                <a href="Register.aspx" >Still do not have account?</a>
                <asp:Button ID="btnLogin" runat="server" type="submit" class="btn btn-primary" OnClick="btnLogin_Click" Text="Logiraj se"></asp:Button>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" Font-Bold="true"
                    Font-Size="15px" ForeColor="Red" />
            </form>
        </div>
        <div class="col-4"></div>
    </div>
</body>
</html>
