<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductInfoWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h3 class="display-4">Product Information</h3>
    </div>
    <div style="border: 1px solid black">

        <table style="margin: 50px">
            <tr>
                <td style="margin: 10px; width: 121px;">Product Id:</td>
                <td style="margin: 10px">
                    <asp:TextBox ID="ProductId" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="margin: 10px; width: 121px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="margin: 10px; width: 121px;">Product Name:</td>
                <td style="margin: 10px">
                    <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="margin: 10px; width: 121px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="margin: 10px; width: 121px;">
                    <asp:Button ID="AddProduct" runat="server" Text="Add Product" OnClick="AddProduct_Click" />
                <td style="margin: 10px">
                    <asp:Button ID="GetProduct" runat="server" Text="Get Product" OnClick="GetProduct_Click" />
            </tr>
        </table>
        <div style="margin: 50px;">
            <asp:Label ID="ErrorMsg" runat="server" Text="Label" ForeColor="#FF3300" Visible="False"></asp:Label>
        </div>
        

    </div>
    <div></div>

    <div style="border: 1px solid black">
        <asp:GridView ID="ProductsGridView" runat="server" AutoGenerateColumns="False" Width="366px">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Product Id" />
                <asp:BoundField DataField="Name" HeaderText="Product Name" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
