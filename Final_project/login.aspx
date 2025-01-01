<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Final_project.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="loginUser" runat="server"
        CreateUserText="Criar conta"
        CreateUserUrl="~/create_account.aspx" 
        DestinationPageUrl="~/Main_page.aspx" OnLoggedIn="loginUtilizador_LoggedIn"></asp:Login>
</asp:Content>
