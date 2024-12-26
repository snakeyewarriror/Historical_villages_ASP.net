<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Final_project.Main1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="DataList1" runat="server"></asp:DataList>
    <a href="Utilizadores/Personal_area.aspx">click here</a>
    <asp:LoginStatus ID="LoginStatus1" runat="server" />
    <asp:LoginView ID="LoginView1" runat="server"></asp:LoginView>
</asp:Content>
