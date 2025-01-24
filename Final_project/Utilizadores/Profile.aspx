<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Final_project.Utilizadores.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    
    <main class="row">
        <div class="mt-5 d-flex justify-content-center align-items-center">
            <div class="form-container p-5 bg-light rounded">
                <h2 class="mb-4 text-center">Perfil</h2>


                <%-- Name--%>
                <div class="mb-3">
                    <label for="TextBoxUsername" class="form-label">Nome de Utilizador</label>
                    <asp:TextBox runat="server" ID="TextBoxUsername" class="form-control" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxUsername" runat="server" Display="Dynamic" ForeColor="#CC0000"
                        SetFocusOnError="true" />

                    <!-- Label for error message -->
                    <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>


                <%-- Email --%>
                <div class="mb-3">
                    <label for="TextBoxEmail" class="form-label">Email</label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" class="form-control" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxEmail" runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Insira um formato de email valido"
                        ControlToValidate="TextBoxEmail" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic" />
                </div>


                <%-- Alter button --%>
                <asp:Button ID="Button1" runat="server"  type="submit" class="btn" style="background-color: rgb(24,54,81); color: white;"
                    Text="Guardar mudanças" Onclick="ChangeProfileButton_Click"/>
            </div>
        </div>
    </main>
</asp:Content>
