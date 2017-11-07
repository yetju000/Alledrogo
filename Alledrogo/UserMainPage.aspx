<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMainPage.aspx.cs" Inherits="UserMainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 69px;
        }
        .auto-style4 {
            text-align: right;
            width: 364px;
        }
        .auto-style5 {
            width: 304px;
        }
        .auto-style6 {
            width: 201px;
        }
        .auto-style7 {
            width: 174px;
        }
        .auto-style8 {
            width: 69px;
            text-align: right;
        }
        .auto-style9 {
            width: 364px;
        }
        .auto-style10 {
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="AddAuction" runat="server" OnClick="AddAuction_Click" Text="Dodaj aukcje" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style4">Balans:<asp:TextBox ID="Balance" runat="server" OnTextChanged="Balance_TextChanged" ReadOnly="True"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="LogoutButton" runat="server" Text="Wyloguj" OnClick="LogoutButton_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="SearchButton" runat="server" Text="Szukaj" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="BoughtButton" runat="server" Text="Kupione" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">
                    <asp:TextBox ID="AmountToCharge" runat="server" CssClass="auto-style10"></asp:TextBox>
                    <asp:Button ID="BuyCoinsButton" runat="server" OnClick="BuyCoinsButton_Click" Text="Doładuj" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="SoldButton" runat="server" Text="Sprzedane" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="BidButton" runat="server" Text="Licytowane" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="SoldingButton" runat="server" Text="Sprzedawane" Width="90px" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
