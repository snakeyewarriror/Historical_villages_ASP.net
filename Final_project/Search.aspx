﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Final_project.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="display: flex; flex-direction: column; align-items: flex-start; margin: 10px;">


         <div style="display: flex; align-items: center; gap: 10px;">

            <label for="listDistrito" style="min-width: 60px;">Distrito</label>
            <asp:DropDownList ID="listDistrito" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDistrito_SelectedIndexChanged" style="width: 200px;"></asp:DropDownList>

            <label for="listConcelho" style="min-width: 70px;  margin-left: 10px"">Concelho</label>
            <asp:DropDownList ID="listConcelho" runat="server" style="width: 200px;"></asp:DropDownList>

             
            <label for="textNome" style="min-width: 60px; margin-left: 40px">Nome</label>
            <asp:TextBox ID="textNome" runat="server" style="width: 400px;"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" class="btn-dark" Text="Pesquisar"  style="padding: 5px 15px;" OnClick="buttonPesquisar_Click"/>

        </div>
    </div>


    <asp:DataList ID="listLocais" runat="server" DataKeyField="LocalID" RepeatDirection="Horizontal"
        RepeatColumns="3" CssClass="m-5" Width="100%">
        <ItemTemplate>

            <table style="margin-bottom: 60px; border-collapse:collapse;">
                <tr>
                    <td style="height: 50px; width: 180px; margin-left: 10px;">
                        <a href='<%# "Local.aspx?id=" + Eval("LocalID") %>' class="link" style="text-decoration: none;">
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("PrimeiraFoto") %>' Height="260" />
                        </a>
                    </td>
                </tr>

                <tr>
                    <td style="padding-top: 30px">
                        <a href='<%# "Local.aspx?id=" + Eval("LocalID") %>' class="link" style="text-decoration: none;">
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("NomeLocal") %>' />
                        </a>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Concelho") + ",&#160;&#160;&#160;" + Eval("Distrito") %>' />
                    </td>
                </tr>

            </table>
        </ItemTemplate>
    </asp:DataList>

    <div class="mt-1 container text-center" style="margin-bottom:100px;">
        <asp:LinkButton Text="Primeira" runat="server" ID="linkFirst" CssClass="text-decoration-none fs-5" OnClick="linkFirst_click" />

        <asp:LinkButton Text="Anterior" runat="server" ID="linkPrevious" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkPrevious_click" />

        <asp:LinkButton Text="Seguinte" runat="server" ID="linkNext" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkNext_click" />

        <asp:LinkButton Text="Última" runat="server" ID="linkLast" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkLast_click" />
    </div>

</asp:Content>