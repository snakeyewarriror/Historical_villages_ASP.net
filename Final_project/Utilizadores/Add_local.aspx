<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add_local.aspx.cs" Inherits="Final_project.Utilizadores.Add_local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Criar Local"></asp:Label>

    Nome<asp:TextBox ID="text_name" runat="server"></asp:TextBox>

    Descrição<asp:TextBox ID="text_description" runat="server"></asp:TextBox>

    Morada<asp:TextBox ID="text_address" runat="server"></asp:TextBox>

    Localidade<asp:TextBox ID="text_town" runat="server"></asp:TextBox>

    Distrito<asp:DropDownList ID="list_district" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDistrito_SelectedIndexChanged" style="width: 200px;"></asp:DropDownList>

    Concelho<asp:DropDownList ID="list_council" runat="server" style="width: 250px;"></asp:DropDownList>

    <br />
    <br />

    <asp:Button ID="save_button" runat="server" class="btn-dark" Text="Guardar"  style="padding: 5px 15px;" OnClick="button_save_local"/>

    <asp:Button ID="cancel_button" runat="server" class="btn-dark" Text="Cancelar"  style="padding: 5px 15px;" />


    <br /><br /><br />
    
    <asp:Label ID="Label2" runat="server" Text="Fotos do Local"></asp:Label>

    <asp:DataList runat="server" ID="list_photos" RepeatColumns="2" RepeatDirection="Horizontal">
        <ItemTemplate>
            <table class="table table-borderless">
                <tr style="height: 220px; vertical-align: middle;">
                    <td style="width: 450px; text-align: center;">
                        <img src='../<%# Eval("Ficheiro") %>' alt='<%# Eval("Legenda") %>' style="width: 350px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text='<%# Eval("Legenda") %>' runat="server" CssClass="fs-3" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%-- controlo que permite selecionar uma foto para
                        editar a legenda ou para remover--%>
                        <asp:LinkButton ID="lnkDetalhes" runat="server"
                        CommandArgument='<%# Eval("ID") %>' OnCommand="link_details_Command" CssClass="btn mt-4" BackColor="#D7D3BF">Selecionar</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>

    <br /><br /><br /><br />

    Selecionar foto<asp:FileUpload ID="photo_upload" runat="server" />

    

    Legenda<asp:TextBox ID="text_legend" runat="server"></asp:TextBox>

    <br /><br /><br />
    
    <asp:Button ID="save_photo_button" runat="server" class="btn-dark" Text="Guardar foto"  style="padding: 5px 15px;" OnClick="button_save_photo"/>

    <asp:Button ID="edit_photo_button" runat="server" class="btn-dark" Text="Editar legenda"  style="padding: 5px 15px;" OnClick="button_edit_legend"/>

    <asp:Button ID="eliminate_photo_button" runat="server" class="btn-dark" Text="Eliminar foto"  style="padding: 5px 15px;" OnClick="button_eliminate_photo"/>

    <asp:Button ID="cancel_everything_button" runat="server" class="btn-dark" Text="Cancelar tudo"  style="padding: 5px 15px;" OnClick="button_cancel"/>

</asp:Content>
