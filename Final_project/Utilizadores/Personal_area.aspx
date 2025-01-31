<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Personal_area.aspx.cs" Inherits="Final_project.Utilizadores.Personal_area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DataList runat="server" ID="listLocais" RepeatColumns="6" RepeatDirection="Horizontal">
         <ItemTemplate>
             <table class="table table-borderless">
                 <tr style="height: 220px; vertical-align: middle;">
                     <td style="width: 300px; text-align: center;">
                         <img src='../<%# Eval("PrimeiraFoto") %>' alt='<%# Eval("NomeLocal") %>'
                        style="width: 250px;" onerror="this.onerror=null; this.src='../Imagens/default.jpeg';" />
                     </td>
                 </tr>
                 <tr>
                     <td>
                        <asp:Label Text='<%# Eval("NomeLocal") %>' runat="server" CssClass="fs-3" />
                     </td>
                 </tr>

                 <tr>
                     <td>
                        <asp:Button ID="edit_local_button" runat="server" CssClass="btn buttons_default" Text="Editar local" Style="padding: 5px 15px; margin-right: 15px" 
                            OnClick="redirect_local" CommandArgument='<%# Eval("LocalID") %>' />
                         
                        <asp:Button ID="eliminate_local_button" runat="server" CssClass="btn buttons_default" Text="Eliminar local" Style="padding: 5px 15px;" 
                            OnClick="eliminate_local" CommandArgument='<%# Eval("LocalID") %>' />
                     </td>
                 </tr>

             </table>
         </ItemTemplate>
    </asp:DataList>
</asp:Content>
