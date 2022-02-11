<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="UserWebApp.Forms.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
</head>
<body class="container">
    <form runat="server">
        <div class="alert alert-danger d-flex justify-content-center align-items-center text-center" >
            <asp:Label CssClass="fw-bolder text-center font-monospace" Text="Nesto je poslo po krivu, molimo vas pokušajte kasnije :)" runat="server" />
        </div>
        <div class="container text-center">
            <asp:Button ID="btnPovratak" runat="server" CssClass="btn btn-outline-success" Text="Povratak na naslovnicu" OnClick="btnPovratak_Click"/>
        </div>
        <div class="container text-center">
            <img src="../Images/ProblemPicture.jpg" class="img-fluid"/>
        </div>
    </form>
</body>
</html>
