<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Personal_area.aspx.cs" Inherits="Final_project.Utilizadores.Personal_area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DataList runat="server" ID="listLocais" RepeatColumns="3" RepeatDirection="Horizontal">
         <ItemTemplate>
             <table class="table table-borderless">

                 <tr style="height: 220px; vertical-align: middle;">
                     <td style="width: 300px; text-align: center;">
                         <img src='../<%# Eval("PrimeiraFoto") %>' alt='<%# Eval("NomeLocal") %>'
                        style="width: 250px;" />
                     </td>
                 </tr>
                 <tr>
                     <td>
                        <asp:Label Text='<%# Eval("NomeLocal") %>' runat="server" CssClass="fs-3" />
                     </td>
                 </tr>

                 <tr>
                     <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Utilizadores/Edit_local.aspx?id=" + Eval("LocalID") %>'>asfasf
                        </asp:HyperLink>
                     </td>
                 </tr>

             </table>
         </ItemTemplate>
    </asp:DataList>

    <asp:Button ID="Button1" runat="server" Text="Criar local" OnClick="Button_create_local" />
</asp:Content>
