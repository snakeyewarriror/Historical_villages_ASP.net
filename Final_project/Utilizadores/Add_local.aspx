<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add_local.aspx.cs" Inherits="Final_project.Utilizadores.Add_local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css"
     integrity="sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY="
     crossorigin=""/>

    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"
     integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo="
     crossorigin=""></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="mt-3 mb-5">
        
        <div class="form_row">
            <label for="text_name" class="form-label" style="margin-left: 0px;">Nome</label>
            <asp:TextBox ID="text_name" runat="server" CssClass="textbox_default_add"></asp:TextBox>
            <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="text_name" runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true"/>

        
            <label for="text_description" class="form-label">Descrição</label>
            <asp:TextBox ID="text_description" runat="server" CssClass="textbox_default_add"></asp:TextBox>

        
            <label for="text_address" class="form-label">Morada</label>
            <asp:TextBox ID="text_address" runat="server" CssClass="textbox_default_add"></asp:TextBox>

        
            <label for="text_town" class="form-label">Localidade</label>
            <asp:TextBox ID="text_town" runat="server" CssClass="textbox_default_add"></asp:TextBox>
        </div>

        <div class="mt-4 mb-3 form_row">
            
            <label for="list_district" class="form-label" style="margin-left:0px;">Distrito</label>
            <asp:DropDownList ID="list_district" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDistrito_SelectedIndexChanged" 
                CssClass="dropdown-default" style="height: 35px;"></asp:DropDownList>
            <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="list_district" InitialValue="Selecione um Distrito"
            runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />

            
            <label for="list_council" class="form-label">Concelho</label>
            <asp:DropDownList ID="list_council" runat="server" CssClass="dropdown-default" style="width: 250px; height: 35px;"></asp:DropDownList>
            <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="list_council" InitialValue="Selecione um Concelho"
                runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />
        </div>

    </div>


    <h3 class="mt-2">Clique no mapa onde o local esta localizado</h3>

    <div id="map" style="width: 40%; height: 640px"></div>
    <asp:HiddenField ID="latitude" runat="server" />
    <asp:HiddenField ID="longitude" runat="server" />

    
    <div class="mt-4">

        <asp:Button ID="save_button" runat="server" class="btn buttons_default" Text="Guardar" style="margin-right: 15px;" OnClick="button_save_local"/>

        <asp:Button ID="cancel_button" runat="server" class="btn buttons_default" Text="Cancelar" OnClick="clear_local_fields"/>
    </div>


    <div class="mt-5">
        <asp:Label ID="Label_photos" runat="server" Text="Fotos do Local" CssClass="h2"></asp:Label>

        <div class="mt-5"></div>

        <asp:DataList runat="server" ID="list_photos" RepeatColumns="4" RepeatDirection="Horizontal">
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
                            <%-- Select photo --%>
                            <asp:LinkButton ID="lnkDetalhes" runat="server"
                            CommandArgument='<%# Eval("ID") %>' OnCommand="link_details_Command" CssClass="btn mt-4" BackColor="#D7D3BF">Selecionar</asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>

    <div class="mt-5 form_row">

        <label for="photo_upload" class="form-label" style="margin-left:0px;">Selecionar foto</label>
        <asp:FileUpload ID="photo_upload" runat="server"/>
    
        
        <div class="mt-4 mb-3 form_row">

            <label for="text_legend" class="form-label" style="margin-left:0;">Legenda</label>
            <asp:TextBox ID="text_legend" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="mt-4">
    
        <asp:Button ID="save_photo_button" runat="server" class="btn buttons_default" Text="Guardar foto" OnClick="button_save_photo"
            style="margin-right: 15px; margin-left: 0px;"/>

        <asp:Button ID="edit_photo_button" runat="server" class="btn buttons_default" Text="Editar legenda" OnClick="button_edit_legend"
            style="margin-right: 15px;"/>

        <asp:Button ID="eliminate_photo_button" runat="server" class="btn buttons_default" Text="Eliminar foto" OnClick="button_eliminate_photo"
            style="margin-right: 15px;"/>

        <asp:Button ID="cancel_everything_button" runat="server" class="btn buttons_default" Text="Cancelar foto" OnClick="button_cancel_photo"
            style="margin-right: 15px;"/>

        <asp:Button ID="Button1" runat="server" class="btn buttons_default" Text="Cancelar tudo" OnClick="button_cancel_all" />

    </div>



    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>    <script>
        let mapOptions = {
            center: [39.69484, - 8.13031],
            zoom: 7
        }
        let map = new L.map('map', mapOptions);
        let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
        map.addLayer(layer);
        let marker = null;


        const latitudeField = '<%= latitude.ClientID %>';
        const longitudeField = '<%= longitude.ClientID %>';

        map.on('click', (event) => {
            if (marker !== null) {
                map.removeLayer(marker);
            }
            marker = L.marker([event.latlng.lat, event.latlng.lng]).addTo(map);

            // Put the values of latitude/longitude on the hiddenfields
            document.getElementById(latitudeField).value = event.latlng.lat;
            document.getElementById(longitudeField).value = event.latlng.lng;
        })
    </script>
</asp:Content>
