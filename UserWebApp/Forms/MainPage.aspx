<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="UserWebApp.Forms.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prikaz kupaca</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script>
        window.addEventListener("DOMContentLoaded", () => {
            const inputs = document.querySelectorAll('input[type=text]');
            const td = document.querySelectorAll('td');

            td.forEach(cell => cell.classList.add("align-middle"));
            inputs.forEach(input => input.classList.add("form-control"));
        });
    </script>
    <style>
        body{ overflow-x:hidden; }

    </style>
</head>
<body class="w-auto">
    <form runat="server" class="gap-5">
        <div class="mb-4">
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
                <div class="container-fluid row">
                    <a class="navbar-brand col-9" href="#">Aplikacija za pregled kupaca</a>
                    <div class="collapse navbar-collapse col-3" id="navbarNavAltMarkup">
                        <div class="navbar-nav gap-3 ">
                            <asp:Button ID="btnProizvodi" runat="server" OnClick="btnProizvodi_Click"  CssClass="btn btn-outline-success" Text="Proizvodi" />
                            <asp:Button ID="btnPotkategorije" runat="server" OnClick="btnPotkategorije_Click" CssClass="btn btn-outline-warning" Text="Potkategorije" />
                            <asp:Button ID="btnKategorije" runat="server" OnClick="btnKategorije_Click" CssClass="btn btn-outline-danger" Text="Kategorije" />
                        </div>
                    </div>
                </div>
            </nav>
        </div>
        <div class="row">
            <div class="col-2 container text-center ">
                <asp:Label CssClass="col-form-label-sm w-75 mb-2 m-2" runat="server">Broj kupaca po stranici</asp:Label>
                <div class="container align-content-center mb-2 m-2">
                    <asp:TextBox
                        ID="tbNumberOfCustomers" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ValidationGroup="NumberValidation"
                        ControlToValidate="tbNumberOfCustomers"
                        ErrorMessage="Unos ne smije biti prazan"
                        Display="None" />
                    <asp:RegularExpressionValidator runat="server"
                        ValidationGroup="NumberValidation"
                        ID="RegularExpressionValidator"
                        ControlToValidate="tbNumberOfCustomers"
                        ErrorMessage="Morate upisati pozitivan broj"
                        Display="None" ToolTip="Upisati broj do 1 do 50" ValidationExpression="^\d+$" />
                    <asp:RangeValidator runat="server"
                        ValidationGroup="NumberValidation"
                        ControlToValidate="tbNumberOfCustomers"
                        ErrorMessage="Morate upisati broj od 1 do 50"
                        Display="None" MaximumValue="50" MinimumValue="1" />
                </div>

                <asp:Button ValidationGroup="NumberValidation" ID="btnConfirmNumber" OnClick="btnConfirmNumber_Click" runat="server" CssClass="btn btn-primary mb-2 m-2" Text="Spremi" />
                <asp:ValidationSummary ValidationGroup="NumberValidation" ID="ValidationSummary1" runat="server" Font-Bold="true"
                    Font-Size="15px" ForeColor="Red" ShowSummary="true" />
            </div>
            <div class="col-8">
                <asp:GridView
                    ID="gvKupci"
                    runat="server"
                    CssClass="table table-striped table-dark"
                    AutoGenerateColumns="False"
                    CellSpacing="2"
                    HorizontalAlign="Center"
                    OnRowEditing="GridView_RowEditing"
                    OnRowCancelingEdit="gvKupci_RowCancelingEdit" DataKeyNames="IDKupac"
                    OnRowUpdating="gvKupci_RowUpdating" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvKupci_PageIndexChanging" OnSorting="gvKupci_Sorting" OnSelectedIndexChanging="gvKupci_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Ime" SortExpression="Ime">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbIme" runat="server" Text='<%# Bind("Ime") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="tbIme"
                                    ErrorMessage="Polje Ime ne smije biti prazno"
                                    Display="None" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Ime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prezime" SortExpression="Prezime">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbPrezime" runat="server" Text='<%# Bind("Prezime") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="tbPrezime"
                                    ErrorMessage="Polje Prezime ne smije biti prazno"
                                    Display="None" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Prezime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="TextBox1" ID="RegularExpressionValidator" runat="server" Display="None" ErrorMessage="Krivi format E-maila" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Email", "<a href=mailto:{0}>{0}</a>") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telefon">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbTelefon" runat="server" Text='<%# Bind("Telefon") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="tbTelefon"
                                    ErrorMessage="Polje Telefon ne smije biti prazno"
                                    Display="None" />
                                <asp:RegularExpressionValidator runat="server"
                                    ID="revTelefon"
                                    ControlToValidate="tbTelefon"
                                    ErrorMessage="Morate upisati  broj"
                                    Display="None" ValidationExpression="^[0-9-]*$" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Telefon") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grad">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbGrad" runat="server" Text='<%# Bind("Grad.Naziv") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="tbGrad"
                                    ErrorMessage="Polje grad ne smije biti prazno"
                                    Display="None" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Grad.Naziv") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Država">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlDrzave" OnDataBinding="ddlDrzave_DataBinding" runat="server" Text='<%# Bind("Grad.Drzava.Naziv") %>'></asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Grad.Drzava.Naziv") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:Button CssClass="btn btn-danger" runat="server" CausesValidation="True" CommandName="Update" Text="Ažuriraj"></asp:Button>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Edit" Text="Uredi"></asp:Button>
                                <asp:Button ID="bntSelect" CssClass="btn btn-primary" runat="server" CausesValidation="False" CommandName="Select" Text="Odaberi"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:ValidationSummary ShowMessageBox="true" DisplayMode="List" ID="ValidationSummary" runat="server" Font-Bold="true"
                    Font-Size="15px" ForeColor="Red" ShowSummary="false" />
            </div>
            <div class="col-2 container text-center">
                <asp:Label CssClass="col-form-label-sm w-75 mb-2 m-2" runat="server">Upišite grad</asp:Label>
                <div class="container align-content-center mb-2 m-2">
                    <asp:TextBox
                        ID="tbCity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                        ValidationGroup="CitiesValidation"
                        ControlToValidate="tbCity"
                        ErrorMessage="Unos ne smije biti prazan"
                        Display="None" />
                </div>

                <asp:Button ValidationGroup="CitiesValidation" ID="btnSearchCities" OnClick="btnSearchCities_Click" runat="server" CssClass="btn btn-primary mb-2 m-2" Text="Filtriraj" />
                <asp:Button ID="btnClearFilter" OnClick="btnClearFilter_Click" runat="server" CssClass="btn btn-primary mb-2 m-2" Text="Očisti filter" />
                <asp:ValidationSummary ValidationGroup="CitiesValidation" ID="vsCities" runat="server" Font-Bold="true"
                    Font-Size="15px" ForeColor="Red" ShowSummary="true" />
            </div>
        </div>
    </form>
</body>
</html>
