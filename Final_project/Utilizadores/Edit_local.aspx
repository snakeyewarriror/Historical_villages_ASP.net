<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Edit_local.aspx.cs" Inherits="Final_project.Utilizadores.Edit_local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css"
        integrity="sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY="
        crossorigin="" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Criar Local"></asp:Label>

    Nome<asp:TextBox ID="text_name" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="text_name" runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />

    Descrição<asp:TextBox ID="text_description" runat="server"></asp:TextBox>

    Morada<asp:TextBox ID="text_address" runat="server"></asp:TextBox>

    Localidade<asp:TextBox ID="text_town" runat="server"></asp:TextBox>

    Distrito<asp:DropDownList ID="list_district" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listDistrito_SelectedIndexChanged" style="width: 200px;"></asp:DropDownList>
    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="list_district" InitialValue="Selecione um Distrito"
        runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />
    <br />

    Concelho<asp:DropDownList ID="list_council" runat="server" style="width: 250px;"></asp:DropDownList>
    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="list_council" InitialValue="Selecione um Concelho"
        runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />

    <br />
    <br />

    

    <h3 class="mt-2">Clique no mapa onde o local esta localizado</h3>

    <div id="map" style="width: 40%; height: 640px"></div>
    <asp:HiddenField ID="latitude" runat="server" />
    <asp:HiddenField ID="longitude" runat="server" />

    
    <br />
    <br />

    <asp:Button ID="save_button" runat="server" class="btn-dark" Text="Guardar"  style="padding: 5px 15px;" OnClick="button_save_local"/>


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

    <asp:Button ID="cancel_everything_button" runat="server" class="btn-dark" Text="Cancelar foto"  style="padding: 5px 15px;" OnClick="button_cancel_photo"/>

    <asp:Button ID="Button1" runat="server" class="btn-dark" Text="Eliminar local"  style="padding: 5px 15px;" OnClick="eliminate_local"/>

    
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"
        integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo="
        crossorigin=""></script>
    <script>
        // Get the values of the definined properties from the Load event with the values from the table
        var lat = parseFloat('<%= Latitude %>') || 39.69484; // Default value if there is no value on the database
        var lng = parseFloat('<%= Longitude %>') || -8.13031;
        var name = '<%= Nome %>';
        

        let mapOptions = {
            center: [lat, lng],
            zoom: 11
        };

        let map = new L.map('map', mapOptions);
        let layer = new L.TileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 15 });
        map.addLayer(layer);

        // Create popup
        let marker = L.marker([lat, lng]).addTo(map).bindPopup(name).openPopup();


        // Identify the hidden fields in the form
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
