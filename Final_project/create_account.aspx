<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="create_account.aspx.cs" Inherits="Final_project.create_account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="LabelTitulo" runat="server" Text="Registo"></asp:Label>
        <br /> <br />
        Nome <asp:TextBox runat="server" ID="TextBoxNome" class="form-control me-2" Style="width: 100px; display: inline;" /> 
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxNome" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <br />

        Nome de Utilizador<asp:TextBox runat="server" ID="TextBoxUsername" class="form-control me-2" Style="width: 100px; display: inline;" /> 
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxUsername" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <br />

        Email <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxEmail" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Insira um formato de email valido" ControlToValidate="TextBoxEmail" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" />
        <br />


        Data de nascimento<asp:TextBox ID="TextBoxDate" runat="server" TextMode="Date"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxDate" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <asp:CompareValidator ID="validarDataNascimento" runat="server" ControlToValidate="TextBoxDate"/>
        <br />

        Password<asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxPassword" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="A password tem de ter pelo menos 6 charaters" ControlToValidate="TextBoxPassword" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="^\d{6,}$" />
        <br />

        Confirme a Password<asp:TextBox ID="TextBoxPasswordConfirmation" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxPasswordConfirmation" runat="server" Display="Dynamic" ForeColor="#CC0000" />
        <asp:CompareValidator ID="CompareValidatorPasswords" runat="server" ErrorMessage="As passwords não são iguais" ForeColor="#CC0000"
            ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxPasswordConfirmation" />
        <br />

        <asp:Button ID="ButtonSubmit" runat="server" Text="Criar conta" Onclick="buttonCriarConta_Click"/>

    </div>
</asp:Content>
