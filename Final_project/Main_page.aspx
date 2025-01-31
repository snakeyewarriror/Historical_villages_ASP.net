<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Main_page.aspx.cs" Inherits="Final_project.Main_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="row">

        <div class="col-9">
            <asp:DataList ID="listLocais" runat="server" DataKeyField="LocalID" RepeatDirection="Horizontal"
                RepeatColumns="3" CssClass="m-5">
                <ItemTemplate>
                    <a href='<%# "Local.aspx?id=" + Eval("LocalID") %>' class="link" style="text-decoration: none;">
                        <div class="imagem-card-local-wrap mb-3 mt-5">
                            <asp:Image CssClass="imagem-card-local" ID="Image1" runat="server" ImageUrl='<%# string.IsNullOrEmpty(Eval("PrimeiraFoto") as string) ? "~/Imagens/default.jpeg" : Eval("PrimeiraFoto") %>' />

                        </div>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("NomeLocal") %>' />
                        <div>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Concelho") + "," %>' />
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Distrito") %>' />
                        </div>
                    </a>
                </ItemTemplate>
            </asp:DataList>

            <div class="mt-1 text-center">
                <asp:LinkButton Text="Primeira" runat="server" ID="linkFirst" CssClass="text-decoration-none fs-5" OnClick="linkFirst_click" />

                <asp:LinkButton Text="Anterior" runat="server" ID="linkPrevious" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkPrevious_click" />

                <asp:LinkButton Text="Seguinte" runat="server" ID="linkNext" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkNext_click" />

                <asp:LinkButton Text="Última" runat="server" ID="linkLast" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkLast_click" />
            </div>
        </div>

        <div class="col-3" style="text-align:center">
            
            <label class="mt-4 h4"> Melhores locais para ir</label>

            <asp:DataList ID="listLocaisTop" runat="server" RepeatDirection="Vertical" CellPadding="10">
                <ItemTemplate>
                    <div class="mb-2">

                        <a href='local.aspx?id=<%# Eval("LocalId") %>' style="text-decoration: none;">
                            <div class="mb-2" style="font-size: 16px; font-weight: bold;">
                                <span style="color: #555;">#<%# Eval("NumeroOrdem") %>:</span>
                                <span style="color: #333;"><%# Eval("LocalNome") %> </span>
                            </div>
                            <img src='<%# Eval("PrimeiraImagem") %>' alt='<%# Eval("LocalNome") %>' style="display:block ; width:100%; border: 1px solid #ccc;" />

                            <div class="mt-2" style="font-size: 14px; color: #666;">
                                Classificação: <%# Eval("MediaClassificacao") %>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </section>
</asp:Content>
