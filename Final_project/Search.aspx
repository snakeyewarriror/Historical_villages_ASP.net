<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Final_project.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="display: flex; flex-direction: column; align-items: flex-start; margin: 10px;">


         <div style="display: flex; align-items: center; gap: 10px;">

            <label for="listDistrito" style="min-width: 60px;">Distrito</label>
            <asp:DropDownList ID="listDistrito" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDistrito_SelectedIndexChanged"
                CssClass="dropdown-default" style="width: 200px; height: 35px;"></asp:DropDownList>

            <label for="listConcelho" style="min-width: 70px;  margin-left: 10px"">Concelho</label>
            <asp:DropDownList ID="listConcelho" runat="server" CssClass="dropdown-default" style="width: 250px; height: 35px;"></asp:DropDownList>

             
            <label for="textNome" style="min-width: 60px; margin-left: 35px;">Nome</label>
            <asp:TextBox ID="textNome" runat="server" CssClass="textbox-default" style="width: 400px; height: 35px;"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn buttons_default" Text="Pesquisar"  style="padding: 5px 15px;"
                OnClick="buttonPesquisar_Click"/>

        </div>
    </div>

    <section class="row">

        <div>
            <asp:DataList ID="listLocais" runat="server" DataKeyField="LocalID" RepeatDirection="Horizontal"
                RepeatColumns="4" CssClass="m-5">
                <ItemTemplate>
                    <div class="mb-3 mt-5">
                        <a href='<%# "Local.aspx?id=" + Eval("LocalID") %>' class="link">
                            <div class="imagem-card-local-wrap">
                                <asp:Image CssClass="imagem-card-local" ID="Image1" runat="server" ImageUrl='<%# string.IsNullOrEmpty(Eval("PrimeiraFoto") as string) ? "~/Imagens/default.jpeg" : Eval("PrimeiraFoto") %>' />

                            </div>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("NomeLocal") %>' />
                            <div>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Concelho") + "," %>' />
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Distrito") %>' />
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:DataList>

            <div class="mt-1 text-center">
                <asp:LinkButton Text="Primeira" runat="server" ID="linkFirst" CssClass="text-decoration-none fs-5" OnClick="linkFirst_click" />

                <asp:LinkButton Text="Anterior" runat="server" ID="linkPrevious" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkPrevious_click" />

                <asp:LinkButton Text="Seguinte" runat="server" ID="linkNext" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkNext_click" />

                <asp:LinkButton Text="Última" runat="server" ID="linkLast" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkLast_click" />
            </div>
        </div>
    </section>
</asp:Content>
