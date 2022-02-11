<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UserWebApp.Forms.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registracija</title>
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
                    <asp:CustomValidator ID="CustomValidator" runat="server"
                        ControlToValidate="tbUsername" Display="None"
                        ErrorMessage="Korisničko ime vec postoji" />
                </div>
                <div>
                    <asp:Label runat="server" class="form-label">Password</asp:Label>
                    <asp:TextBox ID="tbPassword" runat="server" type="password" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="tbPassword" Display="None"
                        ErrorMessage="Niste upisali lozinku" />
                </div>
                <div>
                    <asp:Label runat="server" class="form-label">Repeat password</asp:Label>
                    <asp:TextBox ID="tbRepeatPassword" runat="server" type="password" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="tbRepeatPassword" Display="None"
                        ErrorMessage="Niste upisali potvrdu lozinke" />
                    <asp:CompareValidator runat="server"
                        ControlToCompare="tbPassword" ControlToValidate="tbRepeatPassword" Display="None"
                        ErrorMessage="Lozinke u oba polja moraju odgovarati" />
                </div>
                <asp:Button ID="btnRegister" runat="server" type="submit" class="btn btn-primary" OnClick="btnRegister_Click" Text="Registriraj se"></asp:Button>
                <asp:ValidationSummary runat="server" Font-Bold="true"
                    Font-Size="15px" ForeColor="Red" />
            </form>
        </div>
        <div class="col-4"></div>
    </div>
</body>
</html>
